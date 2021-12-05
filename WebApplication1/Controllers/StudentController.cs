using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace WebApplication1.Controllers
{
    public class StudentController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" select StudentID, StudentName, StudentBook,
            convert(varchar(10),DateofIssued,120) as DateofIssued,PhotoFileName from dbo.Student ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BooksAppDB"].ConnectionString))

            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
        public string Post(Student stu)
        {
            try
            {
                string query = @"
                    insert into dbo.Student values 
                        (
                            '" + stu.StudentName + @"'
                            ,'" + stu.StudentBook + @"'
                            ,'" + stu.DateofIssued + @"'
                            ,'" + stu.PhotoFileName + @"'
                       
                        )
                        ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BooksAppDB"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Failed to Add!!";
            }
        }

        public string Put(Student stu)
        {
            try
            {
                string query = @"
                    update dbo.Student set

                    StudentName ='" + stu.StudentName + @"'
                    ,StudentBook ='" + stu.StudentBook + @"'
                    ,DateofIssued ='" +stu.DateofIssued + @"'
                    ,PhotoFileName ='" +stu.PhotoFileName +@"'
                    where StudentID=" + stu.StudentID + @"
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BooksAppDB"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Failed to Update!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
                    delete from dbo.Student where StudentID=" + id + @"
                ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BooksAppDB"].ConnectionString))

                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Failed to Delete!!";
            }
        }

        [Route("api/Student/GetAllBooksCategoryNames")]
        [HttpGet]

        public HttpResponseMessage GetAllBooksCategoryNames()
        {
            string query = @"
                select BookName from dbo.BooksCategory
                ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BooksAppDB"].ConnectionString))

            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/Student/saveFile")]
        public string saveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                postedFile.SaveAs(physicalPath);
                return filename;

            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }

    }
}
