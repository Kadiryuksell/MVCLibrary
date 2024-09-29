using MVCLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace MVCLibrary.Controllers
{
    public class LendBookController : Controller
    {


        // GET: LendBook
        LibraryEntities Db = new LibraryEntities();

        private List<SelectListItem> UserValues()
        {
            List<SelectListItem> userValues = Db.Users.Select(user => new SelectListItem
            {
                Text = user.Name + " " + user.LastName,
                Value = user.Id.ToString()
            }).ToList();

            return userValues;
        }

        private List<SelectListItem> EmployeeValues()
        {
            List<SelectListItem> employeeValues = Db.Employee.Select(employee => new SelectListItem
            {
              Text = employee.Name +" " + employee.LastName,
              Value = employee.Id.ToString()
            }).ToList();

            return employeeValues;
        }
        private List<SelectListItem> BookValues()
        {
            List<SelectListItem> bookValues = Db.Books.Select(book => new SelectListItem
            {
                Text = book.Name,
                Value = book.Id.ToString()
            }).ToList();

            return bookValues;
        }
        public ActionResult BorrowedBookList()
        {
            var operations = Db.LibraryOperations.Where(p => p.OperationState == false).ToList();

            return View(operations);
        }

        [HttpGet]
        public ActionResult LendBookAdd()
        {
            ViewBag.userValues = UserValues();
            ViewBag.employeeValues = EmployeeValues();
            ViewBag.bookValues = BookValues();

            return View();
        }

        [HttpPost]
        public ActionResult LendBookAdd(LibraryOperations libraryOperation) 
        {
                var userId = Db.Users.Where(p => p.Id == libraryOperation.Users.Id).FirstOrDefault();
                var employeeId = Db.Employee.Where(p => p.Id == libraryOperation.Employee.Id).FirstOrDefault();
                var bookId = Db.Books.Where(p => p.Id == libraryOperation.Books.Id).FirstOrDefault();

            libraryOperation.Books = bookId;
            libraryOperation.Employee = employeeId;
            libraryOperation.Users = userId;

            Db.LibraryOperations.Add(libraryOperation);
            Db.SaveChanges();

            return RedirectToAction("BorrowedBookList");
        }

        public ActionResult BorrowedLendBookBring(LibraryOperations operation) 
        {
            ViewBag.userValues = UserValues();
            ViewBag.employeeValues = EmployeeValues();
            ViewBag.bookValues = BookValues();

            var operationsId = Db.LibraryOperations.Find(operation.Id);

            DateTime dateStart = DateTime.Parse(operationsId.ReturnDate.ToString());
            DateTime dateEnd = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            TimeSpan dateDifference =  dateEnd - dateStart;
            
            ViewBag.dateValue = dateDifference.TotalDays;
            return View("BorrowedLendBookBring", operationsId);
        }

        public ActionResult BorrowedLendBookUpdate(LibraryOperations operation)
        {
            var userId = Db.Users.Where(p => p.Id == operation.Users.Id).FirstOrDefault();
            var bookId = Db.Books.Where(p => p.Id == operation.Books.Id).FirstOrDefault();
            var employeeId = Db.Employee.Where(p => p.Id == operation.Employee.Id).FirstOrDefault();
            var operationId = Db.LibraryOperations.Find(operation.Id);

            operationId.BookId = bookId.Id;
            operationId.EmployeeId = employeeId.Id;
            operationId.UserId = userId.Id;
            operationId.OperationState = true;
            operationId.DateMemberBrought = operation.DateMemberBrought;
            Db.SaveChanges();
            return RedirectToAction("BorrowedBookList");
        }

    }
}