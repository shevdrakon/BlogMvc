using System.Collections.Generic;
using System.Web.Mvc;

using Blog.Core.Domain.Articles;
using Blog.Core.Infrastructure;
using Blog.Services.Articles;

namespace BlogMVC.ViewModel
{
    public class ArticleDetailsViewModel : ArticleViewModel
    {
        #region Constructors
        
        public ArticleDetailsViewModel(Article article, ControllerContext controllerContext)
            : base(article, controllerContext)
        { } 

        #endregion

        public override string Html
        {
            get
            {
                return Article.Html;
            }
        }

        public string RepliesCount
        {
            get
            {
                var count = EngineContext.Current.Container.Resolve<IArticleRepository>().GetTotalRepliesCount(Article.Id);

                return string.Format(Resources.Global.Post_Comments, count);
            }
        }

        public List<Reply> Replies
        {
            get
            {
                return Article.Replies;
            }
        }
    }
}