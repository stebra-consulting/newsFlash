using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace ExternalWebsite2.Models
{
    public class StebraEntity : TableEntity
    {
        public StebraEntity()
        { }
        public StebraEntity(string StebraType, string NewsEntry,
            string NewsDescription, string NewsArticle, string NewsDate, string NewsBody)
        {
            //hold properties that mirrors listitem-columns
            this.PartitionKey = StebraType;
            this.RowKey = NewsEntry;
            this.Title = NewsEntry;
            this.Description = NewsDescription;
            this.Article = NewsArticle;
            this.Body = NewsBody;
            this.Date = NewsDate; //"dd/mm/yyyy"

            string ddmmyyyy_hhmmss = NewsDate;
            string ddmmyyyy = ddmmyyyy_hhmmss.Split(' ')[0];
            string dd = ddmmyyyy.Split('/')[0];
            string mm = ddmmyyyy.Split('/')[1];
            string yyyy = ddmmyyyy.Split('/')[2];
            int yyyymmdd = int.Parse(yyyy + mm + dd);
            this.IntDate = yyyymmdd;//int Date property as yyyymmdd for sort/query against list of this object

        }

        public string Description { get; set; }
        public string Article { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }
        public int IntDate { get; set; }
    }
}