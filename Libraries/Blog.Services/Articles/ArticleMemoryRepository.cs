using System.Collections.Generic;
using System.Linq;

using Blog.Core;
using Blog.Core.Data;
using Blog.Core.Domain.Articles;

namespace Blog.Services.Articles
{
    public class ArticleMemoryRepository : FilteredMemoryRepository<Article, ArticleFilterData>, IArticleRepository
    {
        protected override IEnumerable<Article> GetFilteredItems(IEnumerable<Article> source, ArticleFilterData filterData)
        {
            var result = source;

            if (filterData != null)
            {
                if (!string.IsNullOrEmpty(filterData.TagName))
                    result = result.Where(o => o.Tags != null && o.Tags.Contains(filterData.TagName));

                if (!string.IsNullOrEmpty(filterData.Text))
                    result = result.Where(o => o.Title.Contains(filterData.Text) || o.Html.Contains(filterData.Text));

                if (!string.IsNullOrEmpty(filterData.SeoTitle))
                    result = result.Where(o => o.SeoTitle == filterData.SeoTitle);
            }

            return result;
        }

        public Article GetBySeoTitle(string seoTitle)
        {
            var filter = new ArticleFilterData
                {
                    SeoTitle = seoTitle
                };

            return GetAll(filter).FirstOrDefault();
        }

        public int GetTotalRepliesCount(int articleId)
        {
            var article = GetById(articleId);
            var result = Calculate(article.Replies);

            return result;
        }

        public Dictionary<string, int> GetTagsMap()
        {
            var result = Data.Values.SelectMany(article => article.Tags).
                 GroupBy(o => o).
                 ToDictionary(o => o.Key, o => o.Count());

            return result;
        }

        private int Calculate(List<Reply> replies)
        {
            if (replies.IsEmpty())
                return 0;

            return replies.Count + replies.Sum(reply => Calculate(reply.Replies));
        }

    }
}