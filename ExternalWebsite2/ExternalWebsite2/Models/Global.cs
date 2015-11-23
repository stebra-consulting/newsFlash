using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalWebsite2.Models
{
    public static class Global
    {
        public static List<StebraEntity> globalList {get; set;}


        public static string sidebar(string month, int year)
        {

            //List to hold news
            //List<StebraEntity> freshNews = new List<StebraEntity>();
            ////Get news from AzureTable
            //freshNews = AzureManager.LoadNews();
            int count = 0;
            string monthInText = "";
            switch (month)
            {
                case "01": monthInText = "January"; break;
                case "02": monthInText = "February"; break;
                case "03": monthInText = "March"; break;
                case "04": monthInText = "April"; break;
                case "05": monthInText = "May"; break;
                case "06": monthInText = "June"; break;
                case "07": monthInText = "July"; break;
                case "08": monthInText = "August"; break;
                case "09": monthInText = "September"; break;
                case "10": monthInText = "October"; break;
                case "11": monthInText = "November"; break;
                case "12": monthInText = "December"; break;
                default: break;
            }
            string actionLink = "";
            count = SortByDateManager.filterArchive(month, Global.globalList, year);
            if (count != 0)
            {
                actionLink = monthInText + " (" + count + ")";

            }
            else
            {
                actionLink = null;
            }
           
            return actionLink;
        }



        //public string January { get; set; }
        //public string February { get; set; }
        //public string March { get; set; }
        //public string April { get; set; }
        //public string May { get; set; }
        //public string June { get; set; }
        //public string July { get; set; }
        //public string August { get; set; }
        //public string September { get; set; }
        //public string October { get; set; }
        //public string November { get; set; }
        //public string December { get; set; }
        //public int yyyy { get; set; }
    }
}