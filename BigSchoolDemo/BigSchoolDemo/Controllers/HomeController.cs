using BigSchoolDemo.Models;
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
            var upcoming = _dbContext.Courses.Include(c => c.Lecturer).Include(c => c.Category).Where(cm => cm.DateTime > DateTime.Now);
            return View(upcoming);
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