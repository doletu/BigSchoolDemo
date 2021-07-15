using BigSchoolDemo.Models;
using BigSchoolDemo.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BigSchoolDemo.Controllers
{

    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _dbContext;
        
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            var upcoming = _dbContext.Courses.Include(c => c.Lecturer).Include(c => c.Category).Where(cm => cm.DateTime > DateTime.Now).ToList();


            foreach (var item in upcoming)
            {
                Attendance find = _dbContext.Attendances.FirstOrDefault(p => p.CourseId == item.Id && p.AttendeeId == item.Lecturer.Id);
                if (find == null)
                    item.isGoing = true;

                Following findFollow = _dbContext.Followings.FirstOrDefault(p =>p.FollowerId == item.Lecturer.Id && p.FolloweeId == item.LecturerId);
                if (findFollow == null)
                    item.isFollowing = true;

            }


            var viewmodel = new CoursesViewModel
            {
                UpcomingCourses = upcoming,
                ShowAction = User.Identity.IsAuthenticated,

            };

            return View(viewmodel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}