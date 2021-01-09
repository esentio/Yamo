﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamo.Playground.CS.Model;

namespace Yamo.Playground.CS
{
    class Program
    {
        private static SqlConnection m_Connection;

        static void Main(string[] args)
        {
            m_Connection = new SqlConnection("Server=localhost;Database=YamoTest;User Id=dbuser;Password=dbpassword;");
            m_Connection.Open();

            //Test1();
            Test2();
            Test2b();
            //Test3();
            //Test4();
            //Test5();
            //Test6();
            //Test7();
            //Test8a();
            //Test8b();
            //Test8c();
            //Test9();
            //Test10();
            //Test11();
            //Test12();
            //Test13();
            //Test14();
            //Test15();
            //Test16();
            //Test17();
            //Test18();
            //Test19();
            //Test20();
            //Test21();
            //Test22();
            //Test23();
            //Test24();
            //Test25();
            //Test26();
            //Test27();
            //Test28();
            //Test29();
            //Test30();
            //Test31();
            //Test32();
            //Test33();
            //Test34();
            //Test35();
            //Test36();
            //Test37();
            //Test38();
            //Test39();
            //Test40();
            //Test41();
            //Test42();
            Test43();
            Test44();
        }

        public static MyContext CreateContext()
        {
            return new MyContext(m_Connection, 42);
        }

        public static User GetUser()
        {
            return new User();
        }

        public static User2 GetUser2()
        {
            return new User2();
        }

        public static void Test1()
        {
            using (var db = CreateContext())
            {
                int count = db.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM [User]");
                List<string> logins = db.Query<string>("SELECT Login FROM [User]");
                int affectedRows = db.Execute("DELETE FROM [User]");
            }
        }

        public static void Test2()
        {
            using (var db = CreateContext())
            {
                var login = "foo";
                var affectedRows1 = db.Execute($"DELETE FROM [User] WHERE Login = {login}");

                login = "boo";
                var affectedRows2 = db.Execute("DELETE FROM [User] WHERE Login = {0}", login);
            }
        }

        public static void Test2b()
        {
            using (var db = CreateContext())
            {
                var login = "foo";

                // NEVER DO THIS!!! sql variable is string and login is not converted to SQL parameter
                //var sql = $"DELETE FROM [User] WHERE Login = {login}";
                //var affectedRows = db.Execute(sql);

                FormattableString sql = $"DELETE FROM [User] WHERE Login = {login}";
                var affectedRows = db.Execute(sql);
            }
        }

        public static void Test3()
        {
            using (var db = CreateContext())
            {
                var login = "foo";
                var data = db.QueryFirstOrDefault<(int, string)?>($"SELECT Id, Email FROM [User] WHERE Login = {login}");
                var list = db.Query<(int Login, int Email)>("SELECT Login, Email FROM [User]");
            }
        }

        public static void Test4()
        {
            using (var db = CreateContext())
            {

                var underlyingConnection = db.Database.Connection;
            }
        }

        public static void Test5()
        {
            using (var db = CreateContext())
            {
                try
                {
                    db.Database.BeginTransaction();

                    // do some stuff

                    db.Database.CommitTransaction();
                }
                catch (Exception)
                {
                    db.Database.RollbackTransaction();
                    throw;
                }
            }
        }

        public static void Test6()
        {
            using (var db = CreateContext())
            {
                var user = new User()
                {
                    Login = "admin",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = ""
                };
                db.Insert<User>(user);

                var id = user.Id; // Id is retrieved from the database
            }
        }

        public static void Test7()
        {
            using (var db = CreateContext())
            {
                var user = new User()
                {
                    Id = 42,
                    Login = "admin",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = ""
                };
                db.Insert<User>(user, useDbIdentityAndDefaults: false);
            }
        }

        public static void Test8a()
        {
            using (var db = CreateContext())
            {
                var user = GetUser();

                user.Email = "john.doe@example.com";

                db.Update<User>(user);
            }
        }

        public static void Test8b()
        {
            using (var db = CreateContext())
            {
                var user = GetUser2();

                user.Email = "john.doe@example.com";

                db.Update<User2>(user);
            }
        }

        public static void Test8c()
        {
            using (var db = CreateContext())
            {
                // in VB.NET, you can also write:
                // db.Update(Of User).Set(Function(u) u.Email = "")
                db.Update<User>().Set(u => u.Email, "").Execute();

                // in VB.NET, you can also write:
                // db.Update(Of User).Set(Function(u) u.Login = u.Login & "_invalid")
                db.Update<User>().Set(u => u.Login, u => u.Login + "_invalid").Execute();

                db.Update<User>()
                  .Set(u => u.FirstName, "John")
                  .Set(u => u.LastName, "Smith")
                  .Where(u => u.Id == 42)
                  .Execute();
            }
        }

        public static void Test9()
        {
            using (var db = CreateContext())
            {
                var user = GetUser();

                db.Delete<User>(user);
            }
        }

        public static void Test10()
        {
            using (var db = CreateContext())
            {
                var user = GetUser();

                // delete all users named Joe
                db.Delete<User>().Where(u => u.FirstName == "Joe").Execute();

                // this will delete all records
                db.Delete<User>().Execute();
            }
        }

        public static void Test11()
        {
            using (var db = CreateContext())
            {
                var blog = new Blog()
                {
                    Title = "My awesome blog post",
                    Content = "Lorem ipsum dolor sit amet."
                };

                // Created will contain current timestamp and CreatedUserId will contain user id
                db.Insert<Blog>(blog);

                blog.Title = "My really awesome blog post";

                // Modified will contain current timestamp and ModifiedUserId will contain user id
                db.Update<Blog>(blog);

                blog.Title = "My awesome blog post";

                // Modified will contain OLD timestamp and ModifiedUserId will contain OLD user id
                db.Update<Blog>(blog, setAutoFields: false);

                // Modified in all records will contain current timestamp and ModifiedUserId in all records will contain user id
                db.Update<Blog>().Set(b => b.Content, "").Execute();

                // Deleted will contain current timestamp and DeletedUserId will contain user id
                db.SoftDelete<Blog>(blog);
            }
        }

        public static void Test12()
        {
            using (var db = CreateContext())
            {
                // get number of all records in the table
                var count = db.From<Blog>().SelectCount();

                // get all records
                var records = db.From<Blog>().SelectAll().ToList();

                // get first record or null if the table is empty
                var record = db.From<Blog>().SelectAll().FirstOrDefault();
            }
        }

        public static void Test13()
        {
            using (var db = CreateContext())
            {
                var result1 = db.From<Blog>()
                                .Where(b => b.Title == "My awesome blog post")
                                .SelectAll().FirstOrDefault();

                var result2 = db.From<Blog>()
                                .Where(b => (new int[] { 1, 2, 3 }).Contains(b.CreatedUserId))
                                .SelectAll().ToList();

                var result3 = db.From<Blog>()
                                .Where(b => DateTime.Now.Date.AddDays(-10) <= b.Created)
                                .SelectAll().ToList();

                var result4 = db.From<Blog>()
                                .Where(b => b.Deleted.HasValue && b.Title.StartsWith("Lorem"))
                                .SelectAll().ToList();

                // same as above
                var result5 = db.From<Blog>()
                                .Where(b => b.Deleted.HasValue)
                                .And(b => b.Title.StartsWith("Lorem"))
                                .SelectAll().ToList();
            }
        }

        public static void Test14()
        {
            using (var db = CreateContext())
            {
                var result = db.From<Blog>()
                               .Where(b => Yamo.Sql.DateTime.SameDay(b.Created, DateTime.Now))
                               .SelectAll().ToList();
            }
        }

        public static void Test15()
        {
            using (var db = CreateContext())
            {
                var value = "My awesome blog post";

                var result = db.From<Blog>()
                               .Where("Title = {0} AND Deleted IS NULL", value)
                               .SelectAll().ToList();
            }
        }

        public static void Test16()
        {
            using (var db = CreateContext())
            {
                var value = "My awesome blog post";

                var result = db.From<Blog>()
                               .Where(b => $"{b.Title} = {value} AND {b.Deleted} IS NULL")
                               .SelectAll().ToList();
            }
        }

        public static void Test17()
        {
            using (var db = CreateContext())
            {
                var result = db.From<Blog>()
                               .OrderBy(b => b.Title)
                               .ThenByDescending(b => b.Created)
                               .SelectAll().ToList();
            }
        }

        public static void Test18()
        {
            // TODO: to many brackets in ON clause (update readme.md when fixed).
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                             .Join<Label>((a, l) => l.TableId == nameof(Article) &&
                                                    l.Id == a.Id &&
                                                    l.Language == "en")
                             .SelectAll().ToList();

                foreach (var article in list)
                {
                    Console.WriteLine($"{article.Id}: {article.Label.Description}");
                }
            }
        }

        public static void Test19()
        {
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                             .Join<Label>(j => j.T2.TableId == nameof(Article) &&
                                               j.T2.Id == j.T1.Id &&
                                               j.T2.Language == "en")
                             .SelectAll().ToList();

                foreach (var article in list)
                {
                    Console.WriteLine($"{article.Id}: {article.Label.Description}");
                }
            }
        }

        public static void Test20()
        {
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                             .Join<Label>((a, l) => l.TableId == nameof(Article) &&
                                                    l.Id == a.Id &&
                                                    l.Language == "en")
                             .As(a => a.Label)
                             .SelectAll().ToList();

                foreach (var article in list)
                {
                    Console.WriteLine($"{article.Id}: {article.Label.Description}");
                }
            }
        }

        public static void Test21()
        {
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                             .LeftJoin<ArticlePart>((a, ap) => a.Id == ap.ArticleId)
                             .SelectAll().ToList();

                foreach (var article in list)
                {
                    Console.WriteLine($"Parts of article {article.Id}:");

                    foreach (var part in article.Parts)
                    {
                        Console.WriteLine($"- {part.Id}");
                    }
                }
            }
        }

        public static void Test22()
        {
            using (var db = CreateContext())
            {
                var list = db.From<ArticleSubstitution>()
                             .Join<Article>(j => j.T1.OriginalArticleId == j.T2.Id)
                             .As(s => s.Original)
                             .Join<Article>(j => j.T1.SubstitutionArticleId == j.T3.Id)
                             .As(s => s.Substitution)
                             .SelectAll().ToList();

                foreach (var s in list)
                {
                    Console.WriteLine($"Instead of {s.Original.Price}$, you can pay {s.Substitution.Price}$.");
                }
            }
        }

        public static void Test23()
        {
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                             .LeftJoin<ArticlePart>((a, ap) => a.Id == ap.ArticleId)
                             .SelectAll().ToList();

                foreach (var article in list)
                {
                    Console.WriteLine($"Parts of article {article.Id}:");

                    foreach (var part in article.Parts)
                    {
                        Console.WriteLine($"- {part.Id}");
                    }
                }
            }
        }

        public static void Test24()
        {
            using (var db = CreateContext())
            {
                var lang = "en";

                var list = db.From<Article>()
                             .Join<Label>(j => j.T2.TableId == nameof(Article) &&
                                               j.T2.Id == j.T1.Id &&
                                               j.T2.Language == lang)
                             .LeftJoin<ArticlePart>(j => j.T1.Id == j.T3.ArticleId)
                             .Join<Label>(j => j.T4.TableId == nameof(ArticlePart) &&
                                                 j.T4.Id == j.T3.Id &&
                                                 j.T4.Language == lang)
                             .LeftJoin<ArticleCategory>(j => j.T1.Id == j.T5.ArticleId)
                             .LeftJoin<Category>(j => j.T5.CategoryId == j.T6.Id)
                             .As(j => j.T1.Categories)
                             .Join<Label>(j => j.T7.TableId == nameof(Category) &&
                                               j.T7.Id == j.T6.Id &&
                                               j.T7.Language == lang)
                             .SelectAll().ToList();

                foreach (var article in list)
                {
                    Console.WriteLine($"Article {article.Label.Description}:");

                    Console.WriteLine($"- Categories:");
                    foreach (var category in article.Categories)
                    {
                        Console.WriteLine($"  - {category.Label.Description}");
                    }

                    Console.WriteLine($"- Parts:");
                    foreach (var part in article.Parts)
                    {
                        Console.WriteLine($"  - {part.Label.Description}");
                    }
                }
            }
        }

        public static void Test25()
        {
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                             .LeftJoin<ArticleCategory>(j => j.T1.Id == j.T2.ArticleId)
                             .LeftJoin<Category>(j => j.T2.CategoryId == j.T3.Id)
                             .As(j => j.T1.Categories)
                             .SelectAll().Exclude(j => j.T1.Price).ExcludeT2().ToList();
            }
        }

        public static void Test26()
        {
            using (var db = CreateContext())
            {
                var articlesCount = db.From<Article>().SelectCount();
            }
        }

        public static void Test27()
        {
            using (var db = CreateContext())
            {
                // get prices of all articles
                var prices = db.From<Article>()
                               .Select(a => a.Price)
                               .ToList();
            }
        }

        public static void Test28()
        {
            using (var db = CreateContext())
            {
                // get all categories of single article
                var articleId = 42;
                var categories = db.From<Article>()
                                   .Join<ArticleCategory>(j => j.T1.Id == j.T2.ArticleId)
                                   .Join<Category>(j => j.T2.CategoryId == j.T3.Id)
                                   .Where(j => j.T1.Id == articleId)
                                   .Select(j => j.T3)
                                   .ToList();
            }
        }

        public static void Test29()
        {
            using (var db = CreateContext())
            {
                // get ids of original articles plus articles and their substitutions
                var list = db.From<ArticleSubstitution>()
                             .Join<Article>(j => j.T1.OriginalArticleId == j.T2.Id)
                             .Join<Article>(j => j.T1.SubstitutionArticleId == j.T3.Id)
                             .Select(j => new
                             {
                                 OriginalId = j.T2.Id,
                                 Original = j.T2,
                                 Substitution = j.T3
                             })
                             .ToList();
            }
        }

        public static void Test30()
        {
            //using (var db = CreateContext())
            //{
            //    var list = db.From<ArticleSubstitution>()
            //                 .Join<Article>(j => j.T1.OriginalArticleId == j.T2.Id)
            //                 .Join<Article>(j => j.T1.SubstitutionArticleId == j.T3.Id)
            //                 .Select(j => (j.T2, j.T3))
            //                 .ToList();
            //}

            //Using db = CreateDbContext()
            //  Dim list = db.From(Of ArticleSubstitution).
            //                Join(Of Article)(Function(j) j.T1.OriginalArticleId = j.T2.Id).
            //                Join(Of Article)(Function(j) j.T1.SubstitutionArticleId == j.T3.Id).
            //                Select(Function(x) (OriginalId:=j.T2.Id, Original:=j.T2, Substitution:=j.T3)).
            //                ToList()
            //End Using
        }

        public static void Test31()
        {
            using (var db = CreateContext())
            {
                // get how many articles have the same price
                var pricelist = db.From<Article>()
                                  .GroupBy(a => a.Price)
                                  .Select(a => new
                                  {
                                      a.Price,
                                      ArticleCount = Yamo.Sql.Aggregate.Count()
                                  })
                                  .ToList();
            }
        }


        public static void Test32()
        {
            using (var db = CreateContext())
            {
                // get article, article description and number of substitutions
                var list = db.From<ArticleSubstitution>()
                             .Join<Article>(j => j.T1.OriginalArticleId == j.T2.Id)
                             .Join<Label>(j => j.T3.TableId == nameof(Article) &&
                                               j.T3.Id == j.T2.Id &&
                                               j.T3.Language == "en")
                             .GroupBy(j => new { j.T2, j.T3.Description })
                             .Select(j => new
                             {
                                 Article = j.T2,
                                 ArticleDescription = j.T3.Description,
                                 SubstitutionsCount = Yamo.Sql.Aggregate.Count(j.T1.SubstitutionArticleId)
                             })
                             .ToList();
            }
        }

        public static void Test33()
        {
            using (var db = CreateContext())
            {
                // get tables which have at least 10 translations
                var tables = db.From<Label>()
                               .GroupBy(l => l.TableId)
                               .Having(l => 10 < Yamo.Sql.Aggregate.Count())
                               .Select(l => l.TableId)
                               .ToList();
            }
        }

        public static void Test34()
        {
            using (var db = CreateContext())
            {
                var articles = db.Query<Article>($"SELECT {Yamo.Sql.Model.Columns<Article>()} FROM {Yamo.Sql.Model.Table<Article>()}");

                var data = db.QueryFirstOrDefault<(decimal, Label, Label)?>($@"
                    SELECT a.Price,
                    {Yamo.Sql.Model.Columns<Label>("le")},
                    {Yamo.Sql.Model.Columns<Label>("lg")}
                    FROM Article AS a
                    LEFT JOIN Label AS le ON a.Id = le.Id AND le.Language = 'en'
                    LEFT JOIN Label AS lg ON a.Id = lg.Id AND lg.Language = 'de'
                    WHERE a.Id = 1");
            }
        }

        public static void Test35()
        {
            using (var db = CreateContext())
            {
                // get 3 most expensive articles
                var articles1 = db.From<Article>()
                                  .OrderByDescending(a => a.Price)
                                  .Limit(3)
                                  .SelectAll()
                                  .ToList();

                // get second and third cheapest article
                var articles2 = db.From<Article>()
                                  .OrderBy(a => a.Price)
                                  .Limit(1, 2)
                                  .SelectAll()
                                  .ToList();
            }
        }

        public static void Test36()
        {
            using (var db = CreateContext())
            {
                // get all unique languages
                var languages = db.From<Label>()
                                  .Select(l => l.Language)
                                  .Distinct()
                                  .ToList();
            }
        }

        public static void Test37()
        {
            using (var db = CreateContext())
            {
                int? priceFilter = 42;

                var list = db.From<Article>()
                             .If(priceFilter.HasValue, exp => exp.Where(a => a.Price < priceFilter.Value))
                             .SelectAll().ToList();
            }
        }

        public static void Test38()
        {
            using (var db = CreateContext())
            {
                int? priceFilter = 42;
                int maxPrice = 1000;

                var list = db.From<Article>()
                             .If(priceFilter.HasValue,
                                then: exp => exp.Where(a => a.Price < priceFilter.Value),
                                otherwise: exp => exp.Where(a => a.Price < maxPrice)
                              )
                             .SelectAll().ToList();
            }
        }

        public static void Test39()
        {
            using (var db = CreateContext())
            {
                string labelFilter = "Foo";
                int? priceFilter = 42;

                var list = db.From<Article>()
                             .If(!string.IsNullOrWhiteSpace(labelFilter),
                                then: exp => exp.Join<Label>((a, l) => l.Id == a.Id)
                                                .Where(l => l.Description == labelFilter)
                              )
                             .If(priceFilter.HasValue, exp => exp.And(a => a.Price < priceFilter.Value))
                             .SelectAll().ToList();
            }
        }

        public static void Test40()
        {
            using (var db = CreateContext())
            {
                var includeLabel = false;

                // Label and Description properties will be set to null
                var list = db.From<Article>()
                             .If(includeLabel, exp => exp.Join<Label>((a, l) => l.Id == a.Id))
                             .Select((a, l) => new { Article = a, Label = l, Description = l.Description })
                             .ToList();
            }
        }

        public static void Test41()
        {
            using (var db = CreateContext())
            {
                var tableName = "ArticleArchive";
                var article = new Article() { Id = 42, Price = 10 };

                db.Insert<Article>(tableName).Execute(article);

                article.Price = 11;

                db.Update<Article>(tableName).Execute(article);

                db.SoftDelete<Article>(tableName).Execute(article);

                db.Delete<Article>(tableName).Execute(article);
            }
        }

        public static void Test42()
        {
            using (var db = CreateContext())
            {
                var lang = "en";
                var list = db.From<Article>("ArticleArchive")
                             .Join<Label>($"(SELECT {Yamo.Sql.Model.Columns<Label>()} FROM LabelArchive WHERE Language = {lang})")
                             .On((a, l) => l.Id == a.Id)
                             .SelectAll().ToList();
            }
        }

        public static void Test43()
        {
            using (var db = CreateContext())
            {
                var count = db.From<Blog>()
                              .Where(x => Yamo.Sql.Exp.Coalesce<DateTime?>(x.Deleted, x.Modified).HasValue)
                              .SelectCount();

                var today = DateTime.Now.Date;

                var list = db.From<Blog>()
                             .Where(x => Yamo.Sql.Exp.Coalesce<DateTime?>(x.Deleted, x.Modified).Value != today)
                             .SelectCount();
            }
        }

        public static void Test44()
        {
            using (var db = CreateContext())
            {
                var list = db.From<Article>()
                              .Where(x => x.InStock)
                              .SelectCount();
            }
        }
    }

}
