namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mmark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Course_name = c.String(nullable: false),
                        Course_description = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentInCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        studentID = c.Int(nullable: false),
                        Mark = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.studentID, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.studentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StdId = c.Int(nullable: false, identity: true),
                        StdName = c.String(nullable: false),
                        Grade_id = c.Int(nullable: false),
                        Std_address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StdId);
            
            CreateTable(
                "dbo.StudentInGrades",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        GradeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Grades", t => t.GradeID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.GradeID);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        GradeName = c.String(nullable: false),
                        Salary = c.Int(nullable: false),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_name = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.User_name)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.StudentInCourses", "studentID", "dbo.Students");
            DropForeignKey("dbo.StudentInGrades", "StudentId", "dbo.Students");
            DropForeignKey("dbo.StudentInGrades", "GradeID", "dbo.Grades");
            DropForeignKey("dbo.StudentInCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.StudentInGrades", new[] { "GradeID" });
            DropIndex("dbo.StudentInGrades", new[] { "StudentId" });
            DropIndex("dbo.StudentInCourses", new[] { "studentID" });
            DropIndex("dbo.StudentInCourses", new[] { "CourseId" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Grades");
            DropTable("dbo.StudentInGrades");
            DropTable("dbo.Students");
            DropTable("dbo.StudentInCourses");
            DropTable("dbo.Courses");
        }
    }
}
