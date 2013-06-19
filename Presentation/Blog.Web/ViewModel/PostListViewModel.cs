using System.Collections.Generic;
using BlogMVC.BLL;

namespace BlogMVC.ViewModel
{
    public class PostListViewModel
    {
        public PostListViewModel(List<ArticleListItemViewModel> posts, int totalPostsCount)
        {
            var pagesCount = totalPostsCount / Settings.PageSize;
            if (totalPostsCount % Settings.PageSize != 0)
                pagesCount++;

            PagesCount = pagesCount;
            Posts = posts;
        }

        public int PagesCount
        {
            get; 
            private set;
        }

        public List<ArticleListItemViewModel> Posts
        {
            get; 
            private set;
        }
    }
}