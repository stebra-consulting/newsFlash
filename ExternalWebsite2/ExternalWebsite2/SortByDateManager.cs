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
        public static List<StebraEntity> NewsYoungerThanMonths(List<StebraEntity> newsToSort, int months) //specify how fresh the news must be in measurements of months
        {
            months = 1;

            //today
            string yyyymmdd = DateTime.Now.ToString("yyyy-MM-dd");

            //todays Month
            string mmToday = yyyymmdd.Split('-')[1];

            //Last Month                          //- months, in order words, subtract 1 month from time. if month is 1
            string mmExpired = (int.Parse(mmToday) - months).ToString();

            //Date Last Month
            string expired = yyyymmdd.Replace(mmToday, mmExpired);

            //integer (Date One Month Ago)
            int intExpired = int.Parse(expired.Replace("-", ""));


            List<StebraEntity> listType = new List<StebraEntity>();

            IEnumerable<StebraEntity> iEnumType = listType;

            iEnumType = (from o in newsToSort where o.IntDate >= intExpired select o);

            listType = iEnumType.ToList();

            return listType;
        }


        public static List<StebraEntity> ByMonth(List<StebraEntity> newsToSort, string yyyymm)
        {
          
            int dateStart = int.Parse(yyyymm + "01");

            int dateStop = int.Parse(yyyymm + "31");


            List<StebraEntity> listType = new List<StebraEntity>();

            IEnumerable<StebraEntity> iEnumType = listType;


            iEnumType = (from o in newsToSort where o.IntDate >= dateStart     //yymmdd
                                                    && o.IntDate <= dateStop  //yymmdd
                         select o);

            listType = iEnumType.ToList();

            return listType;


        }
    }
}