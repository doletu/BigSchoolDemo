using BigSchoolDemo.DTOs;
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
    public class FollowingController : ApiController
    {
        private ApplicationDbContext dbContext;


        public FollowingController()
        {
            dbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollwingDto dto)
        {

            var userId = User.Identity.GetUserId();
            if (dbContext.Followings.Any(a => a.FollowerId == userId && a.FolloweeId == dto.FollweeId))
            {
                return BadRequest("The Attendance already exist");
            }


            var following = new Following
            {
                FolloweeId = dto.FollweeId,
                FollowerId = userId
            };

            dbContext.Followings.Add(following);
            dbContext.SaveChanges();



            return Ok();
        }
    }
}
