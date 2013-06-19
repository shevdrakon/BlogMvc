using System;
using System.Collections.Generic;

namespace Blog.Core.Domain.Articles
{
    public class Reply : IEntity
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime Date
        {
            get; 
            set;
        }

        public string HtmlText
        {
            get; 
            set;
        }

        public List<Reply> Replies
        {
            get; 
            set;
        }
    }
}