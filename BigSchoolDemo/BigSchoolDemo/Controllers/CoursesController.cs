using BigSchoolDemo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchoolDemo.Controllers
{
    public class CoursesController : ApiController
    {

        public ApplicationDbContext  dbContext { get; set; }

        public CoursesController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var courses = dbContext.Courses
                        .First(a => a.CategoryId == id && a.LecturerId == userId);

            if (courses.isCancelled)
            {

                return NotFound();
            }
            courses.isCancelled = true;
            dbContext.SaveChanges();

            return Ok();

        }

    }
}
