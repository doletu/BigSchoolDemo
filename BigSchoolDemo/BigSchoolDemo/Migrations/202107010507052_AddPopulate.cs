namespace BigSchoolDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPopulate : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO CATEGORIES (NAME) VALUES ('Devi')");
            Sql("INSERT INTO CATEGORIES (NAME) VALUES ('Busa')");
            Sql("INSERT INTO CATEGORIES (NAME) VALUES ('Mark')");
        }
        
        public override void Down()
        {
        }
    }
}
