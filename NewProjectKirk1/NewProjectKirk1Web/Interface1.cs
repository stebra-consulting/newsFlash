using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProjectKirk1Web
{
    interface iTest
    {
       
        int equals(int number1, int number2);

    }
    interface item
    {
        string title { get; set; }
        string body { get; set; }
        string article { get; set; }
        DateTime date { get; set; }
    }
}
