using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.Models;
using System.Net;
using System.Data;
namespace Customers.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        private CRUD db = new CRUD();

        public ActionResult Index()
        {
            List<Employee> employeeList = new List<Employee>();
            DataTable dtResult = db.GetAllEmployee();
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                Employee employee = new Employee(); //model
                employee.Id = Convert.ToInt32(dtResult.Rows[i]["Id"]);  
                employee.FirstName = dtResult.Rows[i]["FirstName"].ToString();
                employee.LastName = dtResult.Rows[i]["LastName"].ToString();
                employee.Contact = dtResult.Rows[i]["Contact"].ToString();
                employee.Email = dtResult.Rows[i]["Email"].ToString();
                employeeList.Add(employee);
            }
            return View(employeeList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,Contact,Email")] Employee employee)
        {
           
            if (ModelState.IsValid)
            {
                int status = db.CreateEmployee(employee.FirstName, employee.LastName, employee.Contact, employee.Email);
                //int status = db.UpdateEmployee(employee.Id, employee.FirstName, employee.LastName, employee.Contact, employee.Email);
                ViewBag.StatusMessage = "Employee created successfully";
            }
            return RedirectToAction("Index");

        }
        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {

            DataTable dt = db.GetEmployeeById(id);
            Employee employee = new Employee();
            employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            employee.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
            employee.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
            employee.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
            employee.Email = Convert.ToString(dt.Rows[0]["Email"]);
            // employee.Id = Convert.ToInt32(employee.Id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (employee.Id == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
            
       
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DataTable dt = db.GetEmployeeById(id);
            Employee employee = new Employee();
            employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
            employee.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
            employee.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
            employee.Contact = Convert.ToString(dt.Rows[0]["Contact"]);
            employee.Email = Convert.ToString(dt.Rows[0]["Email"]);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            } 
           
            if (employee.Id == null)
            { 
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Contact,Email")] Employee employee)
        {

            if (ModelState.IsValid)
            {
                int status = db.UpdateEmployee(employee);
               
                ViewBag.Status = "Updated Employee details successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            // CRUDModel model = new CRUDModel();
            db.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
