using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models
{
    public class ArticleCreateModel
    {
        [Required]
        [RegularExpression(@"[a-zA-Z\d\+\-]+")]
        public string SeoTitle
        {
            get; 
            set;
        }

        [Required]
        public string Title
        {
            get; 
            set;
        }

        public bool IsTemp
        {
            get; 
            set;
        }

        public DateTime PostedDate
        {
            get; 
            set;
        }

        public List<string> Tags
        {
            get; 
            set;
        }

        public string Html
        {
            get; 
            set;
        }
    }
}