using LMS.Models;
using LMS.Models.BookModel;
using LMS.Models.HoldModel;
using LMS.Models.MemberModel;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LMS.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IMembersRepository _membersRepository;
		private readonly IBooksRepository _booksRepository;
		[Obsolete]
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IHoldRepository _holdRepository;

		[Obsolete]
		public HomeController(IMembersRepository membersRepository, 
			IBooksRepository booksRepository, 
			IHostingEnvironment hostingEnvironment, 
			IHoldRepository holdRepository)
		{
			_membersRepository = membersRepository;
			_booksRepository = booksRepository;
			_hostingEnvironment = hostingEnvironment;
			_holdRepository = holdRepository;
		}

		[AllowAnonymous]
		public ViewResult Index()
		{
			var model = _booksRepository.GetAll().OrderByDescending(b => b.Status);
			return View(model);
		}
		[HttpGet]
		[AllowAnonymous]
		public ActionResult SearchBook(string name)
		{
			var model = _booksRepository.GetBooksByName(name);
			return View("Index",model);
		}
		[AllowAnonymous]
		public ViewResult Details(int? id)
		{
			Book book = _booksRepository.GetBook(id.Value);

			if(book == null)
			{
				Response.StatusCode = 404;
				return View("BookNotFound", id.Value);
			}

			HomeDetailViewModel homeDetailViewModel = new HomeDetailViewModel()
			{
				Book = book,
				PageTitle = "Book details"

			};

			return View(homeDetailViewModel); ;
		}

		[HttpGet]
		public IActionResult BooksOnHold(string email)
		{
			var member = _membersRepository.UserIsMember(email);

			if (member == null)
			{
				return RedirectToAction("Membership", "home", new { email = email });
			}

			var holds = _holdRepository.GetAll().Where(h => h.MemberId == member.Id);

			var books = _booksRepository.GetAll();

			var bookInHold = books.Where(b => holds.Any(h => h.Id == b.HoldId));



			return View(bookInHold);
		}


		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ViewResult Add()
		{
			return View();
		}



		[HttpPost]
		[Obsolete]
		[Authorize(Roles = "Admin")]
		public IActionResult Add(BookCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				string uniqueFileName = ProccessUpoadedFile(model);
				Book book = new Book
				{
					Title = model.Title,
					Author = model.Author,
					Year = model.Year,
					ISBN = model.ISBN,
					Location = model.Location,
					NumberOfCopies = model.NumberOfCopies,
					ImageUrl = uniqueFileName
				};

				_booksRepository.Add(book);

				//Book newbook = _booksRepository.Add(model);
				return RedirectToAction("details", new { id = book.Id });
			}
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin,User")]
		public ViewResult Membership(string email)
		{
			Member membership = _membersRepository.GetMemberByEmail(email);
			MemberEditViewModel model = new MemberEditViewModel
			{
				Id = membership.Id,
				FirstName = membership.FirstName,
				LastName = membership.LastName,
				DateOfBirth = membership.DateOfBirth,
				PhoneNUmber = membership.PhoneNUmber,
				Address = membership.Address,
				User = membership.User,
				UserMembershipId = membership.UserMembershipId
			};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Admin,User")]
		public IActionResult Membership(MemberEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				Member member = _membersRepository.GetMember(model.Id);

				if(member == null)
				{
					var newMember = new Member
					{
						Id = model.Id,
						FirstName = model.FirstName,
						LastName = model.LastName,
						Address = model.Address,
						DateOfBirth = model.DateOfBirth,
						User = model.User,
						UserMembershipId = model.UserMembershipId,
						PhoneNUmber = model.PhoneNUmber
						
					};
					_membersRepository.Add(newMember);

					return RedirectToAction("index");
				}

				member.FirstName = model.FirstName;
				member.LastName = model.LastName;
				member.Address = model.Address;
				member.DateOfBirth = model.DateOfBirth;
				member.User = model.User;
				member.UserMembershipId = model.UserMembershipId;
				member.PhoneNUmber = model.PhoneNUmber;



				_membersRepository.Update(member);

				return RedirectToAction("index");
			}
			return View();
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]
		public ViewResult Edit(int id)
		{
			Book book = _booksRepository.GetBook(id);
			BookEditViewModel bookEditViewModel = new BookEditViewModel
			{
				Id = book.Id,
				Title = book.Title,
				Author = book.Author,
				Year = book.Year,
				ISBN = book.ISBN,
				Location = book.Location,
				NumberOfCopies = book.NumberOfCopies,
				ExistingImagePath = book.ImageUrl
			};
			return View(bookEditViewModel);
		}

		[HttpPost]
		[Obsolete]
		[Authorize(Roles = "Admin")]
		public IActionResult Edit(BookEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				Book newBook = _booksRepository.GetBook(model.Id);
				newBook.Title = model.Title;
				newBook.Author = model.Author;
				newBook.Year = model.Year;
				newBook.ISBN = model.ISBN;
				newBook.Location = model.Location;
				newBook.NumberOfCopies = model.NumberOfCopies;

				if (model.Image != null)
				{
					if (model.ExistingImagePath != null)
					{
						string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingImagePath);
						System.IO.File.Delete(filePath);
					}
					newBook.ImageUrl = ProccessUpoadedFile(model);
				}


				_booksRepository.Update(newBook);

				return RedirectToAction("index");
			}
			return View();
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult DeleteBook(int id)
		{
			var book = _booksRepository.GetBook(id);

			if (book == null)
			{
				ViewBag.ErrorMessage = $"Book with Id = {id} cannot be found";

				return View("NotFound");
			}
			else
			{
				_booksRepository.Delete(id);

				return RedirectToAction("index");
			}
		}

		[HttpPost]
		[Authorize(Roles ="Admin,User")]
		public IActionResult HoldBook(int id, string email)
		{
			var book = _booksRepository.GetBook(id);

			var member = _membersRepository.UserIsMember(email);

			if(member == null)
			{
				return RedirectToAction("Membership", "home",new { email = email});
			}

			if (book == null)
			{
				ViewBag.ErrorMessage = $"Book with Id = {id} cannot be found";

				return View("NotFound");
			}
			else
			{
				book.Status = Status.OnHold;
				var time = DateTime.Now;
				var holdtime = time.AddDays(10);
				var hold = new Hold
				{
					HoldTime = holdtime,
					BookId = book.Id,
					Member = member,
					Book = book
				};

				_holdRepository.Add(hold);

				return RedirectToAction("index");
			}
		}


		[Authorize(Roles = "Admin,User")]
		public IActionResult ReturnBook(int id, string email)
		{
			var hold = _holdRepository.GetAll().FirstOrDefault(h => h.BookId == id);

			_holdRepository.Delete(hold.Id);

			var book = _booksRepository.GetBook(id);

			book.Status = Status.Available;

			_booksRepository.Update(book);

			return RedirectToAction("BooksOnHold", new { email = email });
		}

		[Authorize(Roles = "Admin,User")]
		public IActionResult LostBook(int id, string email)
		{
			var hold = _holdRepository.GetAll().FirstOrDefault(h => h.BookId == id);

			var book = _booksRepository.GetBook(id);

			book.Status = Status.Lost;

			_booksRepository.Update(book);

			return RedirectToAction("BooksOnHold", new { email = email });
		}


		[Authorize(Roles = "Admin,User")]
		public IActionResult FoundBook(int id, string email)
		{
			var hold = _holdRepository.GetAll().FirstOrDefault(h => h.BookId == id);

			var book = _booksRepository.GetBook(id);

			book.Status = Status.OnHold;

			_booksRepository.Update(book);

			return RedirectToAction("BooksOnHold", new { email = email });
		}


		[Obsolete]
		private string ProccessUpoadedFile(BookCreateViewModel model)
		{
			string uniqueFileName = null;
			if (model.Image != null)
			{
				string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;

				string filePath = Path.Combine(uploadFolder, uniqueFileName);
				using var fileStream = new FileStream(filePath, FileMode.Create);
				model.Image.CopyTo(fileStream);
			}

			return uniqueFileName;
		}
	}
}
