using Blog.Services.Articles;
using Blog.Web.Framework.Controllers;
using BlogMVC.BLL;
using BlogMVC.Filters;
using BlogMVC.ViewModel;

using System.Linq;
using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    public class BlogController : BaseController
    {
        #region Constructors
        
        public BlogController(IArticleRepository articleRepository)
        {
            ArticleRepository = articleRepository;
        } 

        #endregion

        #region Properties

        private IArticleRepository ArticleRepository
        {
            get;
            set;
        } 

        #endregion

        public ActionResult Index(int pageIndex)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            var posts = ArticleRepository.GetPage(pageIndex, Settings.PageSize, null);
            var postsCount = ArticleRepository.GetTotalCount(null);

            var postsViewModels = posts.Select(o => new ArticleListItemViewModel(o, ControllerContext)).ToList();
            var viewModel = new PostListViewModel(postsViewModels, postsCount);

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Show(string id)
        {
            var post = ArticleRepository.GetBySeoTitle(id);

            return View(new ArticleDetailsViewModel(post, ControllerContext));
        }

        [HttpGet]
        public ActionResult Tag(string tagName, int pageIndex)
        {
            var originalTagname = TagHelper.GetOriginalTagName(tagName);

            var filter = new ArticleFilterData
            {
                TagName = originalTagname
            };

            var posts = ArticleRepository.GetPage(pageIndex, Settings.PageSize, filter);
            var postsCount = ArticleRepository.GetTotalCount(filter);

            var postsViewModels = posts.Select(o => new ArticleListItemViewModel(o, ControllerContext)).ToList();
            var viewModel = new PostListViewModel(postsViewModels, postsCount);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Search(int pageIndex, string q)
        {
            var filter = new ArticleFilterData
            {
                Text = q
            };

            var posts = ArticleRepository.GetPage(pageIndex, Settings.PageSize, filter);
            var postsViewModels = posts.Select(o => new ArticleListItemViewModel(o, ControllerContext)).ToList();
            var postsCount = ArticleRepository.GetTotalCount(filter);
            var viewModel = new PostListViewModel(postsViewModels, postsCount);

            return View("Tag", viewModel);
        }

        [HttpGet]
        [AsyncRequestOnly]
        public ActionResult ReplyTemplate(int id)
        {
            ViewBag.ReplyToId = id;

            return PartialView("~\\Views\\Shared\\_ReplyLayout.cshtml");
        }
    }
}