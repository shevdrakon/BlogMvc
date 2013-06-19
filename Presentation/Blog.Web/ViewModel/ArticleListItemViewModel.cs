using System.Web.Mvc;
using Blog.Core.Domain.Articles;

namespace BlogMVC.ViewModel
{
    public class ArticleListItemViewModel : ArticleViewModel
    {
        #region Constructors
        
        public ArticleListItemViewModel(Article article, ControllerContext controllerContext) 
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
    }
}