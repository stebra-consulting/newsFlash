using ExternalWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ExternalWebsite2
{
    public static class LinkManager
    {
        public static List<StebraEntity> globalNews { get; set; }

        public static string LinkText(string mm, int intYear)
        {
            //string mm = intMonth.ToString();
            //if (mm.Length == 1) { mm = "0" + mm; }

            string yyyy = intYear.ToString();

            int count = 0;

            foreach (var o in globalNews)
            {
                string strDate = o.IntDate.ToString();      //"20150909"
                string mmDate = strDate.Substring(4, 2);    //"09"
                string yyyyDate = strDate.Substring(0, 4);  //"2015"
                if (mmDate == mm && yyyyDate == yyyy)
                {
                    count++;
                }
            }

            if (count == 0) { return ""; } //cancel execution, there is no news to fetch

            string month = "";

            switch (mm)
            {
                case "01": month = "January"; break;
                case "02": month = "February"; break;
                case "03": month = "March"; break;
                case "04": month = "April"; break;
                case "05": month = "May"; break;
                case "06": month = "June"; break;
                case "07": month = "July"; break;
                case "08": month = "August"; break;
                case "09": month = "September"; break;
                case "10": month = "October"; break;
                case "11": month = "November"; break;
                case "12": month = "December"; break;
                default: break;
            }

            return month + " (" + count + ")";
        }
    }
}

//CALL THIS CLASS FROM VIEW
//< ul id = "linkTree" >
//      @if(LinkManager.LinkExists("11")) //November
//      < li > @Html.ActionLink(LinkManager.LinkText("11"), "Month", new { yyyymm = "201301" }) </ li >


// </ ul >