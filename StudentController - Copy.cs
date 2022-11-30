using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MvcCoreWeb.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace MvcCoreWeb.controller
{
    public class StudentController : Controller

    {
        private readonly IConfiguration _config;

        public StudentController(IConfiguration config)
        {
            _config = config;
        }
        //public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        //Query
        public ActionResult GetAllStudent()
        {
           
            List<Student> students = new List<Student>();
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            {
                connection.Open();
                students = connection.Query<Student>("Select * from Student").ToList();
                connection.Close();
            }
            return View(students);
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Hi reshmi";
            return View();
        }
        public ActionResult InsertStudent(Student student)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            connection.Open();
            var affectedRows = connection.Execute("Insert into Student (Name, Marks) values (@Name, @Marks)", new { Name = student.Name, Marks = student.Marks });
            connection.Close();
            ModelState.Clear();
            return View();
            
        }
        public ActionResult UpdateStudent(Student student)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            connection.Open();
            var affectedRows = connection.Execute("Update Student set Name = @Name, Marks = @Marks Where Id = @Id", new { Id = student.Id, Name = student.Name, Marks = student.Marks });
            connection.Close();
            ModelState.Clear();
            return View(student);
            

        }

        public ActionResult DeleteStudent(Student student)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            {
                connection.Open();
                var affectedRows = connection.Execute("Delete from Student Where Id = @Id", new { Id = student.Id });
                connection.Close();
                ModelState.Clear();
                return View(student);
            }
        }
    }
}
