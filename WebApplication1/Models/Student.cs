using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentBook { get; set; }
        public string DateofIssued  { get; set; }
        public string PhotoFileName { get; set; } 
    }
}