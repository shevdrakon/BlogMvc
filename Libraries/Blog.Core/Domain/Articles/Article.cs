using System;
using System.Collections.Generic;

namespace Blog.Core.Domain.Articles
{
    public class Article : IEntity
    {
        public int Id
        {
            get; 
            set; 
        }

        public string Title
        {
            get;
            set;
        }

        public string Html
        {
            get;
            set;
        }

        public DateTime PostedDate
        {
            get; 
            set;
        }

        public List<Reply> Replies
        {
            get; 
            set;
        }

        public List<string> Tags
        {
            get; 
            set;
        }

        public string SeoTitle
        {
            get; 
            set;
        }

        public bool IsTemp
        {
            get; 
            set;
        }
    }
}