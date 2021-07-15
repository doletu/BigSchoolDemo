using BigSchoolDemo.Models;
using BigSchoolDemo.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            dbContext.Courses.AddOrUpdate(course);
            dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = dbContext.Attendances
                        .Where(a => a.AttendeeId == userId)
                        .Select(a => a.Course)
                        .Include(l => l.Category)
                        .Include(l => l.Lecturer)
                        .ToList();

            var viewModel = new CoursesViewModel
            {
                UpcomingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }


        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();

            var courses = dbContext.Courses
                        .Where(a => a.LecturerId == userId && a.DateTime>DateTime.Now)                        
                        .Include(l => l.Category)
                        .Include(l => l.Lecturer)
                        .ToList();

            return View(courses);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {

            var userId = User.Identity.GetUserId();

            var courses = dbContext.Courses
                        .Single(a => a.CategoryId == id && a.LecturerId == userId);

            var viewModel = new CourseViewModel
            {
                Categories = dbContext.Categories.ToList(),
                Date = courses.DateTime.ToString("dd/M/yyyy"),
                Time = courses.DateTime.ToString("HH:mm"),
                Category = (byte)courses.CategoryId,
                Place = courses.Place,
                Heading = "Edit Course",
                Id=courses.Id
            };
            return View("Create",viewModel);
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = dbContext.Categories.ToList();
                return View("Create", model);
            }

            var userId = User.Identity.GetUserId();

            var courses = dbContext.Courses
                        .Single(a => a.CategoryId == model.Id && a.LecturerId == userId);

            courses.Place = model.Place;
            courses.DateTime = model.GetDateTime();
            courses.CategoryId = model.Category;

            dbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }




        public ActionResult LectureIamGoing()
        {
            ApplicationUser currentUser =
            System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
            .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
           
            //danh sách giảng viên được theo dõi bởi người dùng (đăng nhập) hiện tại
            var listFollwee =dbContext.Followings.Where(p => p.FollowerId ==
            currentUser.Id).ToList();
            //danh sách các khóa học mà người dùng đã đăng ký
            var listAttendances = dbContext.Attendances.Where(p => p.AttendeeId ==
            currentUser.Id).ToList();
            var courses = new List<Course>();
            foreach (var course in listAttendances)
            {
                foreach (var item in listFollwee)
                {
                    if (item.FolloweeId == course.AttendeeId)
                    {
                        Course objCourse = dbContext.Courses.Include("Category").FirstOrDefault(p=>p.Id==course.CourseId);
                        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                        .FindById(objCourse.LecturerId);
                        objCourse.Lecturer = user;

                        courses.Add(objCourse);
                    }
                }
            }
            return View(courses);
        }


    }
}