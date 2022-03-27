using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yamo.Metadata.Builders;
using Yamo.Playground.CS.Model;
using Yamo.SqlServer;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Yamo.Playground.CS
{
    public class MyContext : DbContext
    {
        private SqlConnection m_Connection;

        public int UserId { get; private set; }

        public MyContext(SqlConnection connection, int userId)
        {
            m_Connection = connection;
            UserId = userId;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            CreateUserModel(modelBuilder);
            CreateUser2Model(modelBuilder);
            CreateBlogModel(modelBuilder);
            CreateArticleModel(modelBuilder);
            CreateArticleCategoryModel(modelBuilder);
            CreateArticlePartModel(modelBuilder);
            CreateArticleSubstitutionModel(modelBuilder);
            CreateCategoryModel(modelBuilder);
            CreateLabelModel(modelBuilder);
            CreatePersonModel(modelBuilder);
        }

        private void CreateUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<User>().Property(u => u.Id).IsKey().IsIdentity();
            modelBuilder.Entity<User>().Property(u => u.Login).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();

            //modelBuilder.Entity<User>().ToTable("UserTable");
            //modelBuilder.Entity<User>().Property(u => u.Login).HasColumnName("UserLogin");
        }

        private void CreateUser2Model(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User2>().ToTable("User");
            modelBuilder.Entity<User2>().Property(u => u.Id).IsKey().IsIdentity();
            modelBuilder.Entity<User2>().Property(u => u.Login).IsRequired();
            modelBuilder.Entity<User2>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User2>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User2>().Property(u => u.Email).IsRequired();
        }

        private void CreateBlogModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>();
            modelBuilder.Entity<Blog>().Property(u => u.Id).IsKey().IsIdentity();
            modelBuilder.Entity<Blog>().Property(u => u.Title).IsRequired();
            modelBuilder.Entity<Blog>().Property(u => u.Content).IsRequired();
            modelBuilder.Entity<Blog>().Property(u => u.Created)
                                       .SetOnInsertTo(() => DateTime.Now);
            modelBuilder.Entity<Blog>().Property(u => u.CreatedUserId)
                                       .SetOnInsertTo((MyContext c) => c.UserId);
            modelBuilder.Entity<Blog>().Property(u => u.Modified)
                                       .SetOnUpdateTo(() => DateTime.Now);
            modelBuilder.Entity<Blog>().Property(u => u.ModifiedUserId)
                                       .SetOnUpdateTo((MyContext c) => c.UserId);
            modelBuilder.Entity<Blog>().Property(u => u.Deleted)
                                       .SetOnDeleteTo(() => DateTime.Now);
            modelBuilder.Entity<Blog>().Property(u => u.DeletedUserId)
                                       .SetOnDeleteTo((MyContext c) => c.UserId);
        }

        private void CreateArticleModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>();
            modelBuilder.Entity<Article>().Property(x => x.Id).IsKey();
            modelBuilder.Entity<Article>().Property(x => x.Price);
            modelBuilder.Entity<Article>().Property(x => x.InStock);

            modelBuilder.Entity<Article>().HasOne(x => x.Label);
            modelBuilder.Entity<Article>().HasMany(x => x.Parts);
            modelBuilder.Entity<Article>().HasMany(x => x.Categories);
        }

        private void CreateArticleCategoryModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleCategory>();
            modelBuilder.Entity<ArticleCategory>().Property(x => x.ArticleId).IsKey();
            modelBuilder.Entity<ArticleCategory>().Property(x => x.CategoryId).IsKey();
        }

        private void CreateArticlePartModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticlePart>();
            modelBuilder.Entity<ArticlePart>().Property(x => x.Id).IsKey();
            modelBuilder.Entity<ArticlePart>().Property(x => x.ArticleId);
            modelBuilder.Entity<ArticlePart>().Property(x => x.Price);

            modelBuilder.Entity<ArticlePart>().HasOne(x => x.Label);
        }

        private void CreateArticleSubstitutionModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleSubstitution>();
            modelBuilder.Entity<ArticleSubstitution>().Property(x => x.OriginalArticleId).IsKey();
            modelBuilder.Entity<ArticleSubstitution>().Property(x => x.SubstitutionArticleId).IsKey();

            modelBuilder.Entity<ArticleSubstitution>().HasOne(x => x.Original);
            modelBuilder.Entity<ArticleSubstitution>().HasOne(x => x.Substitution);
        }

        private void CreateCategoryModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<Category>().Property(x => x.Id).IsKey();

            modelBuilder.Entity<Category>().HasOne(x => x.Label);
        }

        private void CreateLabelModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Label>();
            modelBuilder.Entity<Label>().Property(x => x.TableId).IsKey().IsRequired();
            modelBuilder.Entity<Label>().Property(x => x.Id).IsKey();
            modelBuilder.Entity<Label>().Property(x => x.Language).IsKey().IsRequired();
            modelBuilder.Entity<Label>().Property(x => x.Description).IsRequired();
        }

        private void CreatePersonModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>();
            modelBuilder.Entity<Person>().Property(x => x.Id).IsKey().IsIdentity();
            modelBuilder.Entity<Person>().Property(x => x.FirstName).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.LastName).IsRequired();
            modelBuilder.Entity<Person>().Property(x => x.BirthDate).UseDbType(DbType.Date);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(m_Connection);
        }

        protected override void OnCommandExecuting(DbCommand command)
        {
            // log command.CommandText
        }
    }

    class MyContext1 : DbContext
    {
        private SqlConnection m_Connection;

        public MyContext1(SqlConnection connection)
        {
            m_Connection = connection;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<User>().Property(u => u.Id).IsKey().IsIdentity();
            modelBuilder.Entity<User>().Property(u => u.Login).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();

            //modelBuilder.Entity<User>().ToTable("UserTable");
            //modelBuilder.Entity<User>().Property(u => u.Login).HasColumnName("UserLogin");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(m_Connection);
        }
    }

    class MyContext2 : DbContext
    {
        private SqlConnection m_Connection;

        public int UserId { get; private set; }

        public MyContext2(SqlConnection connection, int userId)
        {
            m_Connection = connection;
            UserId = userId;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blog>();
            modelBuilder.Entity<Blog>().Property(u => u.Id).IsKey().IsIdentity();
            modelBuilder.Entity<Blog>().Property(u => u.Title).IsRequired();
            modelBuilder.Entity<Blog>().Property(u => u.Content).IsRequired();
            modelBuilder.Entity<Blog>().Property(u => u.Created)
                                       .SetOnInsertTo(() => DateTime.Now);
            modelBuilder.Entity<Blog>().Property(u => u.CreatedUserId)
                                       .SetOnInsertTo((MyContext2 c) => c.UserId);
            modelBuilder.Entity<Blog>().Property(u => u.Modified)
                                       .SetOnUpdateTo(() => DateTime.Now);
            modelBuilder.Entity<Blog>().Property(u => u.ModifiedUserId)
                                       .SetOnUpdateTo((MyContext2 c) => c.UserId);
            modelBuilder.Entity<Blog>().Property(u => u.Deleted)
                                       .SetOnDeleteTo(() => DateTime.Now);
            modelBuilder.Entity<Blog>().Property(u => u.DeletedUserId)
                                       .SetOnDeleteTo((MyContext2 c) => c.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(m_Connection);
        }
    }
}
