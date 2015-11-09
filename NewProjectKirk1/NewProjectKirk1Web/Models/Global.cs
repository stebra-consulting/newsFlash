using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewProjectKirk1Web.Models
{
    public static class Global
    {
        public static int globalX { get; set; }
        public static int globalY { get; set; }
        public static string globalError { get; set; }
        public static string globalError1 { get; set; }
    }

    public class mytest : iTest
    {
        public int equals(int number1, int number2)
        {
            return number1 + number2;
        }
    }
}