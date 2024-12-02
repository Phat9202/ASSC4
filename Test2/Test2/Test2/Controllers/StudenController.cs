using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Test2.Context;
using Test2.Models;

namespace Test2.Controllers
{
    public class StudenController : Controller
    {
        public MyDb Db;

        public StudenController(MyDb db)
        {
            Db = db;
        }

        public IActionResult Index()
        {
            return View(Db.Students.ToList());
        }
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                Db.Add(student);
                Db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        public IActionResult Details(int? id)
        {
            var student = Db.Students.FirstOrDefault(x => x.Id == id);
            if (student == null || id == null)
            {
                return NotFound();
            }
            return View(student);
        }
    }
}
