using ExternalWebsite2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExternalWebsite2
{
    public class SortByDateManager
    {
        public static List<StebraEntity> LatestFirst(List<StebraEntity> newsToSort)
        {

            newsToSort = newsToSort.OrderByDescending(o => o.IntDate).ToList();

            return newsToSort;

        }


        public static List<StebraEntity> ByMonth(List<StebraEntity> newsToSort)
        {
            //today
            string yyyymmdd = DateTime.Now.ToString("yyyy-MM-dd");

            //todays Month
            string mmToday = yyyymmdd.Split('-')[1];

            //Last Month                          //1 month old
            string mmExpired = (int.Parse(mmToday) - 1).ToString();

            //Date Last Month
            string expired = yyyymmdd.Replace(mmToday, mmExpired);

            //integer (Date One Month Ago)
            int intExpired = int.Parse(expired.Replace("-", ""));


            List<StebraEntity> listType = new List<StebraEntity>();

            IEnumerable<StebraEntity> iEnumType = listType;


            iEnumType = (from o in newsToSort where o.IntDate <= intExpired select o);

            listType = iEnumType.ToList();

            return listType;

        }
    }
}