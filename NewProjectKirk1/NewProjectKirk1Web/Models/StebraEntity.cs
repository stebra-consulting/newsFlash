﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;


namespace NewProjectKirk1Web
{
    public class StebraEntity : TableEntity
    {

        public StebraEntity()
        { }
        public StebraEntity(string StebraType, string NewsEntry,
            string NewsDescription, string NewsArticle, string NewsDate)
        {
            this.PartitionKey = StebraType;
            this.RowKey = NewsEntry;
            this.Title = NewsEntry;
            this.Description = NewsDescription;
            this.Article = NewsArticle;
            this.Date = NewsDate;
        }
 
        public string Description { get; set; }
        public string Article { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
    }
}