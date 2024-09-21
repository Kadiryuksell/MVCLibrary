using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        LibraryEntities db = new LibraryEntities();
        public ActionResult BookList()
        {
            var books = db.Books.ToList();
            return View(books);
        }

        private List<SelectListItem> CategoryValues()
        {
            List<SelectListItem> categoryValues = db.Categories
                       .Select(category => new SelectListItem
                       {
                           Text = category.Name,
                           Value = category.Id.ToString()
                       }).ToList();

            return categoryValues;
        }

        private List<SelectListItem> AuthorValues()
        {
            List<SelectListItem> authorValues = db.Authors
                    .Select(author => new SelectListItem
                    {
                        Text = author.Name + " " + author.LastName,
                        Value = author.Id.ToString()
                    }).ToList();

            return authorValues;
        }

        [HttpGet]
        public ActionResult BookAdd()
        {
            var categoryValues = CategoryValues();

            ViewBag.categories = categoryValues;

            var authorValues = AuthorValues();
            ViewBag.authors = authorValues;
            
            return View();
        }

        [HttpPost]
        public ActionResult BookAdd(Books book)
        {
            var category = db.Categories.Where(c => c.Id == book.Categories.Id).FirstOrDefault();
            var author = db.Authors.Where(a => a.Id == book.Authors.Id).FirstOrDefault();

            book.Categories = category;
            book.Authors = author;

            db.Books.Add(book);
            db.SaveChanges();

            return RedirectToAction("BookList");
        }
        public ActionResult BookSearch(string searchterm)
        {
            if (string.IsNullOrEmpty(searchterm))
            {
                var allBooks = db.Books.ToList();
                return View("BookList", allBooks);
            }

            var authorId = db.Authors
                      .Where(p => (p.Name + " " + p.LastName).Contains(searchterm))
                      .Select(a => a.Id)
                      .ToList();

            if (authorId.Any())
            {
                var booksByAuthors = db.Books.Where(b => authorId.Contains(b.Authors.Id)).ToList();
                return View("BookList", booksByAuthors);
            }

            var books = db.Books.Where(p => p.Name.Contains(searchterm)).ToList();
            return View("BookList", books);
        }
        public ActionResult BookDelete(int id) 
        {
            var bookId = db.Books.Find(id);
            db.Books.Remove(bookId);
            db.SaveChanges();
            return RedirectToAction("BookList");
        }

        public ActionResult BookBring(int id) 
        {
            var authorValues = AuthorValues();
            var categoryValues = CategoryValues();

            ViewBag.authors = authorValues;
            ViewBag.categories = categoryValues;

            var bookId = db.Books.Find(id);
            return View("BookBring", bookId);
        }

        public ActionResult BookUpdate(Books book)
        {
            var categoryId = db.Categories.Where(p => p.Id == book.Categories.Id).FirstOrDefault();
            var authorId = db.Authors.Where(p => p.Id == book.Authors.Id).FirstOrDefault();
            var bookId = db.Books.Find(book.Id);
            bookId.Name = book.Name;
            bookId.Author = authorId.Id;
            bookId.Category = categoryId.Id;
            bookId.PageCount = book.PageCount;
            bookId.State = book.State;
            bookId.PublicationYear = book.PublicationYear;
            bookId.PublisherCompany = book.PublisherCompany;
            db.SaveChanges();
            return RedirectToAction("BookList");
        }
    }
}