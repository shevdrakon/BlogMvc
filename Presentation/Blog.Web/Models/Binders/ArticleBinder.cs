using System;
using System.Linq;
using System.Web.Mvc;

namespace BlogMVC.Models.Binders
{
    public class ArticleBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.RequestContext.HttpContext.Request;

            var result = new ArticleCreateModel
                {
                    SeoTitle = request["txtSEOTitle"],
                    Title = request["txtTitle"],
                    IsTemp = request["cbIsTemp"] != null,
                    Html = request["newArticleText"]
                };

            if (request["txtPostDate"] != null)
            {
                DateTime postDate;

                if (DateTime.TryParse(request["txtPostDate"], out postDate))
                {
                    result.PostedDate = postDate;

                    if (postDate < DateTime.Now)
                        bindingContext.ModelState.AddModelError("PostDate", "PostDate cannot be less then today.");
                }
                else
                    bindingContext.ModelState.AddModelError("PostDate", "Invalid date.");
            }

            if (request["txtTags"] != null)
                result.Tags = request["txtTags"].Split(',').ToList();

            if (bindingContext.Model != null)
                return bindingContext.Model;

            var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => result, typeof(ArticleCreateModel));
            var modelValidator = ModelValidator.GetModelValidator(modelMetadata, controllerContext);

            foreach (var validationResult in modelValidator.Validate(null))
            {
                bindingContext.ModelState.AddModelError(validationResult.MemberName, validationResult.Message);
            }

            return result;
        }
    }
}