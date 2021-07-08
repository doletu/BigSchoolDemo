using BigSchoolDemo.DTOs;
using BigSchoolDemo.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace BigSchoolDemo.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {


        private ApplicationDbContext dbContext;


        public AttendancesController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto  dto)
        {

            var userId = User.Identity.GetUserId();
            if (dbContext.Attendances.Any(a=>a.AttendeeId==userId && a.CourseId==dto.CourseId))
            {
                return BadRequest("The Attendance already exist");
            }

            var attendance = new Attendance
            {
                CourseId = dto.CourseId,
                AttendeeId = userId
            };

            dbContext.Attendances.Add(attendance);
            dbContext.SaveChanges();



            return Ok();
        }


    }
}
