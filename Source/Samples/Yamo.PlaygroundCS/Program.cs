using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamo.PlaygroundCS.Model;

namespace Yamo.PlaygroundCS
{
    class Program
    {
        private static SqlConnection m_Connection;

        static void Main(string[] args)
        {
            m_Connection = new SqlConnection("Server=localhost;Database=YamoTest;User Id=dbuser;Password=dbpassword;");
            m_Connection.Open();

            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            //Test6();
            //Test7();
            //Test8a();
            //Test8b();
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
            Test25();
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
                var affectedRows = db.ExecuteNonQuery("DELETE FROM [User]");
                var count = db.ExecuteScalar<int>("SELECT COUNT(*) FROM [User]");
            }
        }

        public static void Test3()
        {
            using (var db = CreateContext())
            {
                var login = "foo";
                var affectedRows = db.ExecuteNonQuery($"DELETE FROM [User] WHERE Login = {login}");
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

                // Modified will contain current timestamp and ModifiedUserId will contain user id
                db.Update<Blog>(blog);

                // Modified will contain OLD timestamp and ModifiedUserId will contain OLD user id
                db.Update<Blog>(blog, setAutoFields: false);

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
            }
        }

        public static void Test14()
        {
            using (var db = CreateContext())
            {
                var result = db.From<Blog>()
                               .Where(b => Yamo.Sql.DateDiff.SameDay(b.Created, DateTime.Now))
                               .SelectAll().ToList();
            }
        }

        public static void Test15()
        {
            using (var db = CreateContext())
            {
                var result = db.From<Blog>()
                               .Where("Title = 'My awesome blog post' AND Deleted IS NULL")
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
                                                    (l.Id == a.Id &&
                                                    l.Language == "en"))
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
                             .As(j => j.T1.Original)
                             .Join<Article>(j => j.T1.SubstitutionArticleId == j.T3.Id)
                             .As(j => j.T1.Substitution)
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

        //public static void Test26()
        //{
        //    using (var db = CreateContext())
        //    {
        //        var list = db.From<Category>()
        //                     .LeftJoin<ArticleCategory>((c, ac) => c.Id == ac.CategoryId)
        //                     .SelectT1().Include((c, ac) => c.ArticleCount = Sql.Count(ac.ArticleId))
        //                     .GroupBy((c, ac) => c)
        //                     .ToList();

        //        foreach (var category in list)
        //        {
        //            Console.WriteLine($"Category {category.Id} has {category.ArticleCount} article(s).");
        //        }
        //    }
        //}
    }
}
