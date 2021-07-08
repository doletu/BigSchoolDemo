namespace BigSchoolDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addattendance : DbMigration
    {
        public override void Up()
        {
            CreateTable(
             "dbo.Attendances",
             c => new
             {
                 CourseId = c.Int(nullable: false),
                 AttendeeId = c.String(nullable: false, maxLength: 128),
             })
                .PrimaryKey(t => new { t.CourseId, t.AttendeeId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: false)
                 .Index(t => t.AttendeeId)
                .Index(t => t.CourseId); 
//            AddPrimaryKey("dbo.Attendances", new[] { "CourseId", "AttendeeId" });
  //          AddForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
    //        AddForeignKey("dbo.Attendances", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "CoursesId", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendances", new[] { "CourseId" });
            DropIndex("dbo.Attendances", new[] { "AttendeeId" });
            DropTable("dbo.Attendances");
        }
    }
}
