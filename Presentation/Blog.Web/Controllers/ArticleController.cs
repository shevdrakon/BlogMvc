using Blog.Core.Domain.Articles;
using Blog.Services.Articles;
using Blog.Web.Framework.Mappers;
using BlogMVC.Models;
using BlogMVC.Models.Binders;

using System.Web.Mvc;

namespace BlogMVC.Controllers
{
    public class ArticleController : Controller
    {
        #region Constructors

        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            ArticleRepository = articleRepository;
            Mapper = mapper;
        }

        #endregion

        #region Properties

        private IArticleRepository ArticleRepository
        {
            get;
            set;
        }

        private IMapper Mapper
        {
            get; 
            set;
        }

        #endregion

        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Article/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([ModelBinder(typeof(ArticleBinder))] ArticleCreateModel newArticle)
        {
            if (ModelState.IsValid)
            {
                var result = Mapper.Map<Article>(newArticle);

                ArticleRepository.Insert(result);
            }

            return View();
        }

        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Article/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Article/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}