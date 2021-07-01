using BigSchoolDemo.Models;
using BigSchoolDemo.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchoolDemo.Controllers
{
    public class CourseController : Controller
    {
        public readonly ApplicationDbContext dbContext;

        public CourseController()
        {
            dbContext = new ApplicationDbContext();

        }
        // GET: Course
        [Authorize]
        public ActionResult Create()
        {
            var viewmodel = new CourseViewModel
            {
                Categories = dbContext.Categories.ToList()
            };
            return View(viewmodel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = dbContext.Categories.ToList();
                return View("Create", viewModel);
            }

            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place

            };
            dbContext.Courses.Add(course);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");

        }
    }
}