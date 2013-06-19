using System;
using System.Collections.Generic;

using Blog.Core.Domain.Articles;
using Blog.Core.Infrastructure;
using Blog.Services.Articles;

namespace BlogMVC.App_Start
{
    public static class MemoryDataConfig
    {
        public static void AddPosts()
        {
            var repository = EngineContext.Current.Container.Resolve<IArticleRepository>();

            repository.Insert(new Article
            {
                Html = "<b>Hello</b> world!",
                PostedDate = DateTime.Now,
                Title = "Hello World!",
                Tags = new List<string> { ".NET", "NHibernate" },
                SeoTitle = "hello_world_1",
                Replies = new List<Reply>
                {
                    new Reply
                    {
                        Id = 1,
                        Date = DateTime.Now.AddMinutes(-5),
                        HtmlText = "<b>some</b> answer"
                    },
                    new Reply
                    {
                        Id = 2,
                        Date =  DateTime.Now.AddMinutes(-3),
                        HtmlText = "crap!",
                        Replies = new List<Reply>
                        {
                            new Reply
                            {
                                Id = 3,
                                Date = DateTime.Now.AddMinutes(-2),
                                HtmlText = "Shut up!",
                                Replies = new List<Reply>
                                {
                                    new Reply
                                    {
                                        Id = 4,
                                        Date = DateTime.Now.AddMinutes(-1),
                                        HtmlText = "fuck off!"
                                    }
                                }
                            }
                        }
                    },
                    new Reply
                    {
                        Id = 5,
                        Date = DateTime.Now,
                        HtmlText = "cool!"
                    },
                    new Reply
                    {
                        Id = 6,
                        Date = DateTime.Now,
                        HtmlText = "great!"
                    }
                }
            });

            repository.Insert(new Article
            {
                Html = "<b>Hello</b> world!",
                PostedDate = DateTime.Now,
                Title = "Hello World 2!",
                SeoTitle = "hello_world_2"
            });

            repository.Insert(new Article
            {
                Html = "<b>Hello</b> world!",
                PostedDate = DateTime.Now,
                Title = "Hello World 3!",
                SeoTitle = "hello_world_3"
            });

            for (var i = 0; i < 10; i++)
            {
                var post = new Article
                {
                    Title = "hello, " + i,
                    Html = "<b><i>Hello</i></b> world - " + i,
                    PostedDate = DateTime.Now.AddMinutes(-10 * i),
                    SeoTitle = "hello-world-gen-" + i,
                    Tags = new List<string> { ".NET", "ORM" }
                };

                repository.Insert(post);
            }
        }
    }
}