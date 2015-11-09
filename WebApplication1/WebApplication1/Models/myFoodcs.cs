using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Burger : iFood
    {
        public string Prepare()
        {
            return "myBurger";
        }
        public int calcPrice(int t1, int t2) { return t1 + t2; }
    }
    public class Pizza : iFood
    {
        public string Prepare()
        {
            return "myPizza";
        }
        public int calcPrice(int t1, int t2) { return t1 + t2; }
    }
    public class Pasta : iFood
    {
        public string Prepare()
        {
            return "myPasta";
        }
        public int calcPrice(int t1, int t2) { return t1 + t2; }
    }
}