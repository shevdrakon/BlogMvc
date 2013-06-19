using Blog.Core.Domain.Articles;
using Blog.Core.Infrastructure;
using Blog.Services.Articles;
using BlogMVC.BLL;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogMVC.ViewModel
{
    public abstract class ArticleViewModel
    {
         #region Constructors

        protected ArticleViewModel(Article article, ControllerContext controllerContext)
        {
            Article = article;
            ControllerContext = controllerContext;
        }

        #endregion

        protected Article Article
        {
            get; 
            set;
        }

        protected ControllerContext ControllerContext
        {
            get; 
            set;
        }

        public string Date
        {
            get
            {
                return Article.PostedDate.ToLongDateString();
            }
        }

        public MvcHtmlString TagsHtml
        {
            get
            {
                StringBuilder result = null;

                if (Article.Tags != null)
                {
                    result = new StringBuilder();

                    foreach (var tagName in Article.Tags)
                    {
                        var id = TagHelper.GetTagNameForOutput(tagName);
                        var routeValues = new RouteValueDictionary(new {tagName = id});
                        var link = HtmlHelper.GenerateLink(ControllerContext.RequestContext, RouteTable.Routes, tagName, null, "Tag", "Blog", routeValues, null);

                        if (result.Length > 0)
                            result.Append(", ");

                        result.Append(link);
                    }
                }

                return result == null ? null : MvcHtmlString.Create(result.ToString());
            }
        }

        public MvcHtmlString CommentsCountHtml
        {
            get
            {
                var count = EngineContext.Current.Container.Resolve<IArticleRepository>().GetTotalRepliesCount(Article.Id);

                return GetPostUrl(string.Format(Resources.Global.Post_Comments, count), "comment-form");
            }
        }

        public abstract string Html
        {
            get;
        }

        public string Title
        {
            get
            {
                return Article.Title;
            }
        }

        public MvcHtmlString TitleHtml
        {
            get
            {
                return GetPostUrl(Article.Title, null);
            }
        }

        private MvcHtmlString GetPostUrl(string linkText, string detail)
        {
            var routeValues = new RouteValueDictionary(new
            {
                id = Article.SeoTitle
            });

            var url = UrlHelper.GenerateUrl(null, "Show", "Blog", routeValues, RouteTable.Routes, ControllerContext.RequestContext, false);

            if (!string.IsNullOrEmpty(detail))
                url += string.Format("#{0}", detail);

            var a = new TagBuilder("a");
            a.SetInnerText(linkText);
            
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(new {href = url});
            a.MergeAttributes(attributes);

            return MvcHtmlString.Create(a.ToString());
        }
    }
}