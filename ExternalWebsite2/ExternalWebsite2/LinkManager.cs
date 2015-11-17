using ExternalWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ExternalWebsite2
{
    public class LinkManager
    {
        public static bool LinkExists(string mm)
        {



            //List to hold news
            List<StebraEntity> news = new List<StebraEntity>();

            //Get ALL news from AzureTable
            news = AzureManager.LoadNews();

            //sortlist by latestFirst
            news = SortByDateManager.LatestFirst(news);

            int count = 0;

            foreach (var o in news)
            {
                string strDate = o.IntDate.ToString();
                string mmDate = strDate.Substring(3, 5);
                if (mmDate == mm)
                {
                    count++;
                }

                //intDate example 20150909

            }

            return false;
        }

        public static string LinkText(string mm)
        {
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
                case "10": month = "Octoboer"; break;
                case "11": month = "November"; break;
                case "12": month = "December"; break;
                default: break;
            }

            return "Hello";
        }
    }
}

//CALL THIS CLASS FROM VIEW
//< ul id = "linkTree" >
//      @if(LinkManager.LinkExists("11")) //November
//      < li > @Html.ActionLink(LinkManager.LinkText("11"), "Month", new { yyyymm = "201301" }) </ li >


// </ ul >