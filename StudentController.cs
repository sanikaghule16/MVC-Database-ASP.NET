using MvCDtabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvCDtabase.Controllers
{
    public class StudentController : Controller
    {
        StudentRepository repo = new StudentRepository();

        public ActionResult Index() => View(repo.GetAll());

        [HttpPost]
        public ActionResult Create(Student std)
        {
            repo.Insert(std);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var student = repo.GetAll().FirstOrDefault(s => s.ID == id);
            if (student == null) return HttpNotFound();

            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                repo.Edit(std);
                return RedirectToAction("Index");
            }
            return View(std);
        }

        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }

}
