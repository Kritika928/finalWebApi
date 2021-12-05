using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BooksCategoryController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @" select BookID, BookName from dbo.BooksCategory ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["BooksAppDB"].ConnectionString))

                using(var cmd=new SqlCommand(query,con))
                using(var da=new SqlDataAdapter(cmd))
            {   cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }
            return Request.CreateResponse(HttpStatusCode.OK,table);
        }
        public string Post(BooksCategory dep)
        {
            try
            {
                string query = @"
                    insert into dbo.BooksCategory values
                    (
                        '" + dep.BookName + @"'
                       
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

        public string Put(BooksCategory dep)
        {
            try
            {
                string query = @"
                    update dbo.BooksCategory set
                    BookName = '" + dep.BookName + @"'
                    
                    where BookID=" + dep.BookID + @"
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
                    delete from dbo.BooksCategory where BookID=" + id + @"
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


    }
}
