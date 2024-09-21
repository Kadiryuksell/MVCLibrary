using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLibrary.Models.Entity;

namespace MVCLibrary.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        LibraryEntities db = new LibraryEntities();
        public ActionResult EmployeeList()
        {
            var employees = db.Employee.ToList();
            return View(employees);
        }

        [HttpGet]
        public ActionResult EmployeeAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmployeeAdd(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View("EmployeeAdd");
            }
            db.Employee.Add(employee);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

        public ActionResult EmployeeBring(int id) 
        {
            var EmployeeId = db.Employee.Find(id);

            return View("EmployeeBring", EmployeeId);
        
        }

        public ActionResult EmployeeUpdate(Employee employee)
        {
            var employeeId = db.Employee.Find(employee.Id);
            employeeId.Name = employee.Name;
            employeeId.LastName = employee.LastName;
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }


        public ActionResult EmployeeDelete(int id) 
        {
            var employeeId = db.Employee.Find(id);
            db.Employee.Remove(employeeId);
            db.SaveChanges();
            return RedirectToAction("EmployeeList");
        }

    }
}