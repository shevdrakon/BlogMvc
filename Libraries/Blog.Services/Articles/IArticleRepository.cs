using System.Collections.Generic;

using Blog.Core.Data;
using Blog.Core.Domain.Articles;

namespace Blog.Services.Articles
{
    public interface IArticleRepository : IFilteredRepository<Article, ArticleFilterData>
    {
        Article GetBySeoTitle(string seoTitle);

        int GetTotalRepliesCount(int id);

        Dictionary<string, int> GetTagsMap();
    }
}