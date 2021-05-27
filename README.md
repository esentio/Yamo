# Yamo

Yamo is a simple micro ORM written in VB.NET targeting .NET Standard 2.0. It currently supports MS SQL Server and SQLite databases.

## Install

There are 3 NuGet packages available:

- [Yamo](https://www.nuget.org/packages/Yamo/) (core features)
- [Yamo.SQLite](https://www.nuget.org/packages/Yamo.SQLite/) (support for SQLite)
- [Yamo.SqlServer](https://www.nuget.org/packages/Yamo.SqlServer/) (support for MS SQL Server)

## What's new

You can find [release notes here](https://github.com/esentio/Yamo/releases).

## License

This library is under the MIT License.

## FAQ

**Why Yamo?**

Yamo stands for Yet Another Micro ORM. In the beginning I couldn't come with better name and it kind of stuck.

**What? VB.NET? You're joking, right?**

Nope. Reason is simple: I use VB.NET daily in my work and I'm much more fluent in it than I'm in C#. I know that C# gets most attention these days, but I personally like VB.NET more (with Option Strict On and Microsoft.VisualBasic namespace banned - I'm not _that_ crazy).

All examples below are in C# though. I understand it is more convenient for general .NET community.

**Why another (micro) ORM? There is plenty of them already.**

I know, but each of them lacks certain feature(s) that I would like them to have. On the contrary, Yamo might not be the right tool for you either. Just check the features and see. There are similarities with tools like EF Core or OrmLite, but Yamo has its own unique features.

**What ADO.NET providers does Yamo require?**

You should be able to use any provider. But it is tested with Microsoft.Data.Sqlite and Microsoft.Data.SqlClient.

**It's not yet in version 1.0. Can I use it already?** 

It is a work in progress, but it is pretty stable now and can do a lot of things already. It has been used in couple of commercial projects by now. Public API shouldn't change much except adding new features. See mentions about planned features below. Internals will certainly change, because of refactoring/code cleaning.

Most of the public API is covered by tests (executed against real databases - both MS SQL Server and SQLite).

This documentation is being updated to always match latest released version.

**Can I contribute?**

As mentioned above, this is work in progress right now, so you might want to wait.

## Usage and features

### General overview

Yamo might be right tool for you when you aim for:

- Simple, convenient and typed access to your database.
- Possibility to use raw SQL.
- Map your POCO classes 1:1 to database tables.
- Query POCOs, even with 1:N and M:N relationships.
- Cross platform.
- Performance.
- You didn't leave immediately after you found out about VB.NET.

On the contrary, you might want to look elsewhere when you need:

- Heavy ORM with object state tracking, lazy loading, migrations, insert-your-enterprise-feature-here...

### Basics

*Note: in following examples, SQL Server database is used, but same would work with SQLite (except few platform specific differences).*

All database access in Yamo is done via `DbContext`. It's similar to context in Entity Framework, but there are some conceptual differences. You can define your context like this:

```c#
using Yamo;

class MyContext : DbContext
{
    private SqlConnection m_Connection;

    public MyContext(SqlConnection connection)
    {
        m_Connection = connection;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(m_Connection);
    }
}
```

There is also possibility to pass `Func<DbConnection>` factory in `UseSqlServer` and `UseSQLite` methods. Yamo will then create new connection for every context.

For brevity, in all following samples assume that `CreateContext` is a factory method that creates new `DbContext` instance.

#### Simple queries

We can now run simple SQL queries:

```c#
using (var db = CreateContext())
{
    int count = db.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM [User]");
    List<string> logins = db.Query<string>("SELECT Login FROM [User]");
    int affectedRows = db.Execute("DELETE FROM [User]");
}
```

For parametrized query we can use string interpolation and pass `FormattableString` or pass string format with parameters:

```c#
using (var db = CreateContext())
{
    var login = "foo";
    var affectedRows1 = db.Execute($"DELETE FROM [User] WHERE Login = {login}");

    login = "boo";
    var affectedRows2 = db.Execute("DELETE FROM [User] WHERE Login = {0}", login);
}
```

Both options will translate to the following query:

```sql
DELETE FROM [User] WHERE Login = @p0
```

Every argument is converted to `DbParameter`, so we are safe from SQL injections. However, if string interpolation is used, make sure you really pass `FormattableString` and not `String`:

````cs
using (var db = CreateContext())
{
    var login = "foo";

    // NEVER DO THIS!!! sql variable is string and login is not converted to SQL parameter
    //var sql = $"DELETE FROM [User] WHERE Login = {login}";
    //var affectedRows = db.Execute(sql);

    FormattableString sql = $"DELETE FROM [User] WHERE Login = {login}";
    var affectedRows = db.Execute(sql);
}
````

#### Transactions

If we for some reason need to access underlying connection, we can achieve that by accessing `Database` property of `DbContext`:

```c#
using (var db = CreateContext())
{
    var underlyingConnection = db.Database.Connection;
}
```

More importantly, `Database` facade contains methods for begin, commit or rollback of transaction. 

```c#
using (var db = CreateContext())
{
    try
    {
        db.Database.BeginTransaction(); // default level is serializable

        // do some stuff
        // db.Database.Transaction contains current transaction

        db.Database.CommitTransaction();
    }
    catch (Exception)
    {
        db.Database.RollbackTransaction();
        throw;
    }
}
```

It is important to note that Yamo doesn't implicitly open transaction inside the context/when executing the query nor does it commit transaction when disposing the context. If you want to use transactions, you need to call beginn/commit explicitly.

### Creating model

Model in Yamo is set of POCO classes - entities - that are mapped to your database tables. Model is built with fluent API, which is very similar to API Entity Framework Core. In contrast to EF Core, this is the only way how to define the model. There is no support for using attributes in your entity classes (at least not at this moment).

To configure your model, you need to override `OnModelCreating` method in your context.

For example, let's consider following table and class:

```sql
CREATE TABLE [dbo].[User] (
	[Id] int NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED, 
	[Login] nvarchar(50) NOT NULL, 
	[FirstName] nvarchar(50) NOT NULL, 
	[LastName] nvarchar(50) NOT NULL, 
	[Email] nvarchar(50) NOT NULL
)
```

```c#
class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
```

We map table `User` to entity `User` like this:

```c#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>();
    modelBuilder.Entity<User>().Property(u => u.Id).IsKey().IsIdentity();
    modelBuilder.Entity<User>().Property(u => u.Login).IsRequired();
    modelBuilder.Entity<User>().Property(u => u.FirstName).IsRequired();
    modelBuilder.Entity<User>().Property(u => u.LastName).IsRequired();
    modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
}
```

If table name equals class name and if column name equals property name, all you need to do is to call `Entity` and `Property` methods respectively. If the names differ, you must specify corresponding database names:

```c#
modelBuilder.Entity<User>().ToTable("UserTable", "MySchema"); // schema is optional
modelBuilder.Entity<User>().Property(u => u.Login).HasColumnName("UserLogin");
```

Primary key is defined with `IsKey` call. If PK consists of multiple columns, you have to call `IsKey` on all of them (order matters).

Autoincrement/Indentity column is marked with `IsIdentity` method. Columns with default values are marked with `HasDefaultValue` method. There are some limitations though - please see the chapter describing inserts.

Right now, you can map to properties of following types: `Guid`, `Guid`, `Guid?`, `string`, `bool`, `bool?`, `Int16`, `Int16?`, `Int32`, `Int32?`, `Int64`, `Int64?`, `float`, `float?`, `double`, `double?`, `decimal`, `decimal?`, `decimal`, `decimal?`, `DateTime`, `DateTime?` and `byte[]`.

Database nullability is infered by property type. However, it is not possible to infer nullability for `string` and `byte[]`, since these are reference types and could always be `null`. Therefore, you should call `IsRequired` builder method for string and binary `NOT NULL` columns. Also, make sure such properties don't have `null` values when doing inserts or updates.

It is important to note that you need to explicitly call `Entity` and `Property` builder methods for all your entities and all their mapped properties. Unmapped properties will be ignored by Yamo. An attempt to pass non-defined entity will cause runtime exception.

Besides properties that maps directly to database columns, you can also define navigation properties. They define how entities relate to each other. You can have either reference navigation property which holds reference to single related entity (1:1 relationship) or collection navigation property that holds reference to multiple related entities (1:N or M:N relationships).

For example following class `Article` has reference navigation property `Label` and collection navigation property `Categories`:

```c#
class Article
{
    public int Id { get; set; }
    // ...
    public Label Label { get; set; }
    public List<Category> Categories { get; set; }
}
```

We can define the relationship in model using `HasOne` and `HasMany` builder methods:

```c#
modelBuilder.Entity<Article>().HasOne((x) => x.Label);
modelBuilder.Entity<Article>().HasMany((x) => x.Categories);
```

`Label` and `Categories` properties could now be automatically filled when corresponding tables are joined as you'll see later. Although, it is possible not to define any relationship in your model and still get the properties filled with joined entities. Details are discussed in chapter about joins.

Collection navigation property could be of any type which implements `IList<T>` interface and has parameterless constructor.

Unlike in EF Core, there is currently no support for inverse navigation properties.

###### Planned features:

- [#10](https://github.com/esentio/Yamo/issues/10) Support mapping to various property types (using convert functions).
- [#12](https://github.com/esentio/Yamo/issues/12) Create/detele table for model entity.
- [#13](https://github.com/esentio/Yamo/issues/13) Support for inverse relationship navigation.

### Insert 

To insert a record to the database, call `Insert` method on `DbContext`.

```c#
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
```

Note that we didn't set Id when we create user instance. Because `Id` is an autoincrement field, its value is read from the database during insert. Same would apply if there was any column with default value.

However, latter only works in MS SQL Server database. In SQLite, value of (single) autoincrement field (PK) is read fine, but any attempt to read field's default value will fail.

If you wish to insert a value defined in code instead, set `useDbIdentityAndDefaults` parameter to `false`.

```c#
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
```

###### Planned features:

- [#15](https://github.com/esentio/Yamo/issues/15) Batch inserts.

### Update

Update of record can be done by calling `Update` method:

```c#
using (var db = CreateContext())
{
    var user = GetUser();

    user.Email = "john.doe@example.com";

    db.Update<User>(user);
}
```

Generated SQL:

```sql
UPDATE [User] SET
[Login] = @p0, 
[FirstName] = @p1, 
[LastName] = @p2, 
[Email] = @p3
WHERE
[Id] = @p4
```

Note that values of all columns are updated, even if only value of single column has been changed. Unlike some "big" ORM frameworks, Yamo doesn't track objects and their inner state. That's not its job. After you call select, insert or update, Yamo doesn't hold the reference to your POCOs.

Sometimes updating of all database fields is exactly what you want. Sometimes it's not necessary and might even lead to performance issues. Yamo solves this dilemma in a way that object itself could track its state. All you need to do is implement `IHasDbPropertyModifiedTracking` interface in your model objects.

```c#
public interface IHasDbPropertyModifiedTracking {
    bool IsAnyDbPropertyModified();
    bool IsDbPropertyModified(string propertyName);
    void ResetDbPropertyModifiedTracking();
}
```

If we modify `User` class to implement `IHasDbPropertyModifiedTracking`, following update statement will be generated instead:

```sql
UPDATE [User] SET
[Email] = @p3
WHERE
[Id] = @p4
```

If you need to override this behavior, just set `forceUpdateAllFields` parameter of `Update` method to `true` and `UPDATE` statement with all columns will be generated.

After operations like insert, update or select, `ResetDbPropertyModifiedTracking` is called automatically, so you don't need to worry about that.

Note that if `IsAnyDbPropertyModified` call returns `false`, no SQL `UPDATE` statement is executed.

Parameterless `Update` method returns an instance of `UpdateSqlExpression`, which allows you to build `UPDATE` command and update more than one record at once. Just don't forget to call `Execute` method at the end.

````c#
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
````

You can use complex expressions in `Set` or `Where` clauses. Details are discussed in chapter about selecting data.

###### Planned features:

- [#16](https://github.com/esentio/Yamo/issues/16) Batch updates.
- [#19](https://github.com/esentio/Yamo/issues/19) Support for upsert.

### Delete

Unsurprisingly, delete of record is achieved by calling `Delete` method:

```c#
using (var db = CreateContext())
{
    var user = GetUser();

    db.Delete<User>(user);
}
```

Similar to updates, parameterless `Delete` allows you to build delete query and delete more than one record at once.

```c#
using (var db = CreateContext())
{
    var user = GetUser();

    // delete all users named Joe
    db.Delete<User>().Where(u => u.FirstName == "Joe").Execute();

    // this will delete all records
    db.Delete<User>().Execute();
}
```

Besides normal deletes, Yamo also supports soft deletes. More on them in the next chapter.

###### Planned features:

- [#17](https://github.com/esentio/Yamo/issues/17) Batch deletes.

### Support for audit fields

In LOB applications it is quite common that tables contain audit fields. These store info about who created the record, when it was created, who updated it last time and so on.

Imagine following table:

```sql
CREATE TABLE [dbo].[Blog] (
	[Id] int NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED, 
	[Title] nvarchar(50) NOT NULL, 
	[Content] ntext NOT NULL, 
	[Created] datetime NOT NULL, 
	[CreatedUserId] int NOT NULL, 
	[Modified] datetime NULL, 
	[ModifiedUserId] int NULL, 
	[Deleted] datetime NULL, 
	[DeletedUserId] int NULL
)
```

And corresponding entity:

```c#
class Blog
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public int CreatedUserId { get; set; }
    public DateTime? Modified { get; set; }
    public int? ModifiedUserId { get; set; }
    public DateTime? Deleted { get; set; }
    public int? DeletedUserId { get; set; }
}
```

We have 6 audit fields in `Blog` table. For sure we can manage their values manually, but that would be annoying and we might forget to update the right values with every insert/update/soft delete operation.

Yamo can do it automatically for us. All we need to do is adjust the model:

```c#
class MyContext : DbContext
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(m_Connection);
    }
}
```

The example should be pretty self-explanatory. You have to call `SetOnXYZTo` methods when building the metadata and provide factory methods that return new value for each field. You can even use combinations, e.g. when field's value should be set on both insert and update.

But be carefull with your factory methods! After first use, model is cached and factories as well. If you use a captured variable inside your lambda, it might lead to memory leaks, exceptions (e.g. if the object behind the variable changes its state during app lifetime) or setting wrong value (e.g. user id changed, but captured variable isn't updated). To avoid these situations, there are overloads which accept `DbContext` instance. Current context is passed to the factory method when the value is required. Example above illustrates this approach on providing up to date user id value.

We can now do operations with `Blog` without worrying about audit fields:

```c#
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

    blog.Title = "My blog post";

    // Modified will contain OLD timestamp and ModifiedUserId will contain OLD user id
    db.Update<Blog>(blog, setAutoFields: false);

    // Modified in ALL records will contain current timestamp and
    // ModifiedUserId in ALL records will contain user id
    db.Update<Blog>().Set(b => b.Content, "").Execute();

    // Deleted will contain current timestamp and DeletedUserId will contain user id
    db.SoftDelete<Blog>(blog);
}
```

If we for some reason don't want to set values to audit fields (or set them manually instead), we can pass `false` in `setAutoFields` parameter when doing insert or update.

Soft deletes are basically updates that mark the record as deleted instead of performing real delete operation. Make sure you explicitly filter these records using condition when you perform select queries (unless you want to include them as well). Yamo doesn't filter them for you (yet)!

Note that when `IHasDbPropertyModifiedTracking` record is being updated, Yamo first checks whether at least one property is changed. If not, no `UPDATE` statement is executed and therefore no audit field are updated. If there is a change, all changed fields + (update) audit fields are updated in the database. This isn't true for parameterless `Update` and `SoftDelete` methods, where `UPDATE` command is always executed.

###### Planned features:

- [#20](https://github.com/esentio/Yamo/issues/20) Optionally exclude soft deleted records without explicit where condition.

### Querying data

To query entities from the database, you need to call `From` method on `DbContext`. Then you have to specify what you want to select. Here is simplest example of acquiring POCOs from database:

```c#
using (var db = CreateContext())
{
    // get number of all records in Blog table
    var count = db.From<Blog>().SelectCount();

    // get all records
    var records = db.From<Blog>().SelectAll().ToList();

    // get first record or null if table is empty
    var record = db.From<Blog>().SelectAll().FirstOrDefault();
}
```

Querying all or just first record wouldn't be very usefull. Just like many other ORMs, Yamo allows you to specify your filter conditions, ordering, etc. via lambda expressions. Unlike some of them, Yamo doesn't use `IQueryable ` interface (main reason is to have API closer to actual SQL).

#### Where

Here are some examples of using `Where` method:

```c#
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
```

These will translate to following SQL queries:

```sql
SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE ([T0].[Title] = @p0)

SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE ([T0].[CreatedUserId] IN (1, 2, 3))

SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE (@p0 <= [T0].[Created])

SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE ([T0].[Deleted] IS NOT NULL AND [T0].[Title] LIKE @p0 + '%')

SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE ([T0].[Deleted] IS NOT NULL AND [T0].[Title] LIKE @p0 + '%')
```

`Where` accepts lambda `Expression` that are parsed and translated to SQL `WHERE` clause. Of course not every .NET operation could be translated to SQL. Here is a more or less complete list of things you can use:

| C#                                                           | VB.NET                                                       | SQL                                                          |
| ------------------------------------------------------------ | ------------------------------------------------------------ | ------------------------------------------------------------ |
| `&&`                                                         | `And`, `AndAlso`                                             | `AND`                                                        |
| `||`                                                         | `Or`, `OrElse`                                               | `OR`                                                         |
| `==`, `is`                                                   | `=`, `Is`                                                    | `=`                                                          |
| `!=`                                                         | `<>`, `IsNot`                                                | `<>`                                                         |
| `!`                                                          | `Not`                                                        | `NOT`                                                        |
| `<`, `<=`, `>`, `>=`                                         | `<`, `<=`, `>`, `>=`                                         | `<`, `<=`, `>`, `>=`                                         |
| `+`, `-`, `*`, `/`, `%`                                      | `+`, `-`, `*`, `/`, `Mod`, `&`                               | `+`, `-`, `*`, `/`, `%`, `+`                                 |
| `null`                                                       | `Nothing`                                                    | `NULL`                                                       |
| `nullableVar.Value`                                          | `nullableVar.Value`                                          | `.Value` part is ignored                                     |
| `nullableVar.HasValue`                                       | `nullableVar.HasValue`                                       | `nullableVar IS NOT NULL`                                    |
| `!nullableVar.HasValue`                                      | `Not nullableVar.HasValue`                                   | `nullableVar IS NULL`                                        |
| `(Type)var`                                                  | `CType`, `DirectCast`                                        | cast is ignored                                              |
| `stringValue.StartsWith("a")`                                | `stringValue.StartsWith("a")`                                | `LIKE @p + '%'` (`LIKE @p` in SQLite)                        |
| `stringValue.EndsWith("a")`                                  | `stringValue.EndsWith("a")`                                  | `LIKE '%' + @p` (`LIKE @p` in SQLite)                        |
| `stringValue.Contains("a")`                                  | `stringValue.Contains("a")`                                  | `LIKE '%' + @p + '%'` (`LIKE @p` in SQLite)                  |
| `(new int[] { 1, 2 }).Contains(x)`                           | `{ 1, 2 }.Contains(x)`                                       | `@p IN (1, 2)`                                               |
| `(new int[] { }).Contains(x)`                                | `(new Int32() {}).Contains(x)`                               | `0 = 1`                                                      |
| `listVar.Contains(x)`                                        | `listVar.Contains(x)`                                        | `@p0 IN (@p1, @p2, ...)`                                     |
| `emptyListVar.Contains(x)`                                   | `emptyListVar.Contains(x)`                                   | `0 = 1`                                                      |
| `true`, `false`                                              | `True`, `False`                                              | `1`, `0`                                                     |
| Number constants: `42`, `42.6`, ...                          | `42`, `42.6`, ...                                            | `42`, `42.6`, ...                                            |
| String values: `"foo"`                                       | `"foo"`                                                      | `@p` (always SQL parameter)                                  |
| Empty string: `""`                                           | `""`                                                         | `''`                                                         |
| Calls like: `x.SomeProperty`, `x.Foo.GetValue()`, `Foo.GetStaticValue(x, y)`, ... | `x.SomeProperty`, `x.Foo.GetValue()`, `Foo.GetStaticValue(x, y)`, ... | `@p` (always evaluated and value is passed via SQL parameter - might fail for certain types!) |
| Access to entity property: `x => x.Title == "foo"`           | `Function(x) x.Title = "foo"`                                | `[TableAlias].[Title] = @p`                                  |
| Access to joined entity property: `join => join.T1.Title == "foo"` | `Function(join) join.T1.Title = "foo"`                       | `[TableAlias].[Title] = @p`                                  |

Long story short: everything that has direct equivalent in SQL is translated; values, function calls etc. are evaluated and passed as parameters; strings are always passed as parameters to avoid SQL injection and access to entity properties is converted to `tablealias.column` construct.

##### SQL helpers

However, it's still not enough when we need to use specific SQL functions in our query. E.g. something like:

```sql
... WHERE DATEDIFF(day, column, @p) = 0
```

Yamo's attempt to solve this problem are SQL helpers. Let's start with an example:

```c#
using Sql = Yamo.Sql;
...

using (var db = CreateContext())
{
    var result = db.From<Blog>()
                   .Where(b => Sql.DateTime.SameDay(b.Created, DateTime.Now))
                   .SelectAll().ToList();
}
```

This will be translated to:

```sql
SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE (DATEDIFF(day, [T0].[Created], @p0) = 0)
```

`DateTime` is an SQL helper class - descendant from `SqlHelper`. It implements bunch of static methods like `SameDay`, `SameMonth`, etc. When `Where` predicate is parsed and call to SQL helper is found, Yamo asks helper class what the final SQL chunk should look like (string format) and how to convert helper method arguments (to become string format parameters).

In this case `b.Created` will become `[T0].[Created]`, `DateTime.Now` will be `@p0` and `"DATEDIFF(day, {0}, {1}) = 0"` string format will be used.

Below is the list of currently available and built-in SQL helpers:

| Class       | Available methods                                            |
| ----------- | ------------------------------------------------------------ |
| `DateTime`  | `GetCurrentDateTime`, `GetCurrentDate`, `GetDate`, `SameYear`, `SameQuarter`, `SameMonth`, `SameDay`, `SameHour`, `SameMinute`, `SameSecond`, `SameMillisecond` |
| `Aggregate` | `Count`, `CountDistinct`, `Sum`, `SumDistinct`, `Avg`, `AvgDistinct`, `Stdev`, `StdevDistinct`, `Min`, `Max` |
| `Exp`       | `As`, `Raw`, `Coalesce`, `IsNull`, `IfNull`, `NullIf`, `IIf` |
| `Model`     | `Columns`, `Column`, `Table`                                 |

You also can implement your own SQL helpers. All you need to do is inherit from `SqlHelper`, write you own (static) helper methods and overload `GetSqlFormat` static method.

When call to any static method of `SqlHelper` descendant is detected, Yamo translates it to SQL instead of evaluating the method call.

As an example, here is the implementation of our `DateTime` helper:

```c#
public class DateTime : SqlHelper
{

    public static bool SameYear(System.DateTime date1, System.DateTime date2)
    {
        throw new Exception("This method is not intended to be called directly.");
    }

    public static bool SameQuarter(System.DateTime date1, System.DateTime date2)
    {
        throw new Exception("This method is not intended to be called directly.");
    }

    // ...

    public static new SqlFormat GetSqlFormat(MethodCallExpression method, SqlDialectProvider dialectProvider)
    {
        switch (method.Method.Name)
        {
            case nameof(DateTime.SameYear):
                return new SqlFormat("DATEDIFF(year, {0}, {1}) = 0", method.Arguments);
            case nameof(DateTime.SameQuarter):
                return new SqlFormat("DATEDIFF(quarter, {0}, {1}) = 0", method.Arguments);
            // ...
            default:
                throw new NotSupportedException($"Method '{method.Method.Name}' is not supported.");
        }
    }
}
```

Don't worry about actually implementing the methods. They are never called (remember, you pass `Expression<Func<T, bool>>`, not `Func<T, bool>` to `Where`). Of course you can implement them anyway and use them as your .NET helper methods if you want.

If the helper produces platform specific SQL and you need to support both MS SQL Server and SQLite databases, it is recommended to do the following. Create one common helper class and then platform specific helper classes. Inheriting them from common helper class is not required, but it is recommended. Register platform specific helpers with `SqlDialectProvider.RegisterDialectSpecificSqlHelper<TSqlHelper, TDialectSqlHelper>()` method. When helper method of the main class is used in the query, Yamo will actually call `GetSqlFormat()` for the particular platform.

That is also the case of built-in `DateTime` helper. Calling `SameDay` for SQLite will return `"(strftime('%Y-%m-%d', {0}) = strftime('%Y-%m-%d', {1}))"` format string, since `DATEDIFF` doesn't work in SQLite.

You don't need to implement a helper for each call of native SQL. Goal is not to blindly port every SQL function, but to provide additional value and/or convenience. For "one time job" you can simply write chunks of SQL with `Exp.Raw<>()` method or use raw SQL string in your clauses.

##### Raw SQL

If .NET expressions and SQL helpers are still not enough, you can always write your condition using raw SQL string:

```c#
using (var db = CreateContext())
{
    var value = "My awesome blog post";

    var result = db.From<Blog>()
                   .Where("Title = {0} AND Deleted IS NULL", value)
                   .SelectAll().ToList();
}
```

Or `FormattableString`, which gives you even more power:

```c#
using (var db = CreateContext())
{
    var value = "My awesome blog post";

    var result = db.From<Blog>()
                   .Where(b => $"{b.Title} = {value} AND {b.Deleted} IS NULL")
                   .SelectAll().ToList();
}
```

Not only all variables are passed as SQL parameters, but entity properties are translated to their column names together with proper table alias:

```sql
SELECT [T0].[Id], [T0].[Title], [T0].[Content], [T0].[Created], [T0].[CreatedUserId], [T0].[Modified], [T0].[ModifiedUserId], [T0].[Deleted], [T0].[DeletedUserId] FROM [Blog] [T0] WHERE [T0].[Title] = @p0 AND [T0].[Deleted] IS NULL
```

#### Order by

To sort the data, use `OrderBy`, `OrderByDescending`, `ThenBy` and `ThenByDescending` methods:

```c#
using (var db = CreateContext())
{
    var result = db.From<Blog>()
                   .OrderBy(b => b.Title)
                   .ThenByDescending(b => b.Created)
                   .SelectAll().ToList();
}
```

#### Joins

Let's first introduce the database and entities that will be used to explain SQL joins:

```sql
CREATE TABLE [dbo].[Article] (
	[Id] int NOT NULL PRIMARY KEY CLUSTERED, 
	[Price] decimal(10, 2) NOT NULL
)

CREATE TABLE [dbo].[ArticleCategory] (
	[ArticleId] int NOT NULL, 
	[CategoryId] int NOT NULL,
	CONSTRAINT [PK_ArticleCategory] PRIMARY KEY CLUSTERED ([ArticleId], [CategoryId])
)

CREATE TABLE [dbo].[ArticlePart] (
	[Id] int NOT NULL PRIMARY KEY CLUSTERED, 
	[ArticleId] int NOT NULL, 
	[Price] decimal(10, 2) NOT NULL
)

CREATE TABLE [dbo].[ArticleSubstitution] (
	[OriginalArticleId] int NOT NULL, 
	[SubstitutionArticleId] int NOT NULL,
	CONSTRAINT [PK_ArticleSubstitution] PRIMARY KEY CLUSTERED ([OriginalArticleId], [SubstitutionArticleId])
)

CREATE TABLE [dbo].[Category] (
	[Id] int NOT NULL PRIMARY KEY CLUSTERED
)

CREATE TABLE [dbo].[Label] (
	[TableId] nvarchar(50) NOT NULL, 
	[Id] int NOT NULL, 
	[Language] nvarchar(3) NOT NULL, 
	[Description] nvarchar(100) NOT NULL,
	CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED ([TableId], [Id], [Language])
)
```

```c#
class Article
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    public Label Label { get; set; }
    public List<ArticlePart> Parts { get; set; }
    public List<Category> Categories { get; set; }
}

class ArticleCategory
{
    public int ArticleId { get; set; }
    public int CategoryId { get; set; }
}

class ArticlePart
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    public decimal Price { get; set; }

    public Label Label { get; set; }
}

class ArticleSubstitution
{
    public int OriginalArticleId { get; set; }
    public int SubstitutionArticleId { get; set; }

    public Article Original { get; set; }
    public Article Substitution { get; set; }
}

class Category
{
    public int Id { get; set; }
    public Label Label { get; set; }
}

class Label
{
    public string TableId { get; set; }
    public int Id { get; set; }
    public string Language { get; set; }
    public string Description { get; set; }
}
```

So we have an article which can be in multiple categories (M:N relationship). Each article can have multiple parts (1:N relationship). Some articles can be substitued to other articles. Because we have multilanguage system, all descriptions are stored in separate table (Label).

Let's say we want to get all articles with their english descriptions:

```c#
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
```

Corresponding SQL:

```sql
SELECT [T0].[Id], [T0].[Price], [T1].[TableId], [T1].[Id], [T1].[Language], [T1].[Description] FROM [Article] [T0] INNER JOIN [Label] [T1] ON ((([T1].[TableId] = @p0) AND ([T1].[Id] = [T0].[Id])) AND ([T1].[Language] = @p1))
```

So when we want to perform a join, we call one of the following methods:

- `Join` - translates to `INNER JOIN`
- `LeftJoin` - translates to `LEFT OUTER JOIN`
- `RightJoin` - translates to `RIGHT OUTER JOIN`
- `FullJoin` - translates to `FULL OUTER JOIN`
- `CrossJoin` - translates to `CROSS JOIN`

We also have to provide an expression that is translated to SQL `ON` clause. There are overloads that allows you to pass lambda with 2 parameters: first is of type that equals to one of previously used entities. Type of second parameter matches currently joined entity.

That is not enough if you need to join one entity (table) multiple times (ambiguous match) or you want to use condition based on multiple entities. In that case, you can pass lambda that accepts only one parameter: `Join<...>`. In our example only 2 tables are joined, so it would be `Join<TTable1, TTable2>`. Join object contains properties `T1`, `T2`, ... which correspond to all entities in query in order they were introduced. Here is the same example rewritten using join object:

```c#
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
```

You can use `Join<...>` in filtering and sorting methods as well.

As you can see, all `Article` objects now contain their description in `Label` property. Why when we didn't explicitly requested that? Actually, we did. Calling `SelectAll` means that all columns of all tables are returned. But that's only one part of the puzzle. Additionally, we have defined navigation property in the model:

```c#
modelBuilder.Entity<Article>();
modelBuilder.Entity<Article>().Property((x) => x.Id).IsKey();
modelBuilder.Entity<Article>().Property((x) => x.Price);

modelBuilder.Entity<Article>().HasOne((x) => x.Label);
modelBuilder.Entity<Article>().HasMany((x) => x.Parts);
modelBuilder.Entity<Article>().HasMany((x) => x.Categories);
```

Yamo detects that we are joining table `Label` and tries to determine where that entity belongs in the model. Algorithm is pretty straightforward: is there any navigation property defined on previously used entities that points to currently joined one? Is it unambigous? If yes, we set the value accordingly. If not, value is ignored (maybe it is just a junction table in M:N relationship we are not interested in returning it).

In case the relationship is not defined in the model or if the match is unambigous, we can still instruct Yamo to fill the correct property using `As` method. Here is an updated example:

```c#
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
```

We explicitly instructed Yamo to fill `Article.Label` property with joined `Label` entity, although it wasn't necessary in this particular case (because of the model definition).

Here is an example where using `As` hint is necessary, because we are joining the same table twice:

```c#
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
```

Let's try something different now. What about 1:N relationships?

```c#
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
```

Because `Article.Parts` is marked as a collection navigation property, it is filled with related `ArticlePart` entities. Result then contains only unique `Article` objects.

Ok, how about something more complex?

```c#
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
```

We just queried data using three 1:1 relationships (`Label`), one 1:N relationship (`Article` - `ArticlePart`) and one M:N relationship (`Article` - `ArticleCategory` - `Category`). How cool is that?

You can nest 1:N and M:N relationships - Yamo will group records correctly based on their primary key values. This is important to know the consequences. For example if we drop `j.T2.Language == lang` condition in the example above and our database contains english and german translations, `Article` result set will be doubled, because half of them will have english `Label` set and half of them german. But every `Article` will contain the same `Categories` and `Parts`. If we dropped `j.T4.Language == lang` condition instead, `Article` and `Categories` will remain the same, but all `Parts` in every `Article` will contain twice as many records - half of them with english `Label` and half of them with german.

Note that if the resultset contains multiple copies of the same entity (same = same primary key value), Yamo always creates new object instance for each processed row. If in our example two `Article`s belong to the same `Category`, both will contain the same `Category` in their `Categories` property. But it won't be the same object instance.

Of course, instances are only created when necessary. From 2 rows containing the same `Article`, just one `Article` object is created.

How does 1:N and M:N relationships grouping work with `FirstOrDefault`? Are the collection navigation properties always filled only with one record or with all records related to the first main entity? Actually, it does what you need!

You can pass an optional `CollectionNavigationFillBehavior` parameter to `FirstOrDefault` method to define how the resultset should be processed. Parameter has no effect if there is no joined table or all joined tables are configured to fill purely reference navigation properties (1:1 relationship). Shall any joined table fill a collection navigation property (1:N or M:N relationship), behavior will control when to stop processing the resultset.

`CollectionNavigationFillBehavior` has following enum members:

- `ProcessOnlyFirstRow`: process only first row from the resultset. Any collection navigation property will contain maximum 1 item. This is the default behavior.
- `ProcessUntilMainEntityChange`: process the resultset until it contains the same main entity. If resultset is sorted properly and all rows related to main entity are grouped together, all collection navigation properties will be filled with all related items.
- `ProcessAllRows`: process the whole resultset. All collection navigation properties will be filled with all related items, no matter how the resultset is sorted. **This always processes all the rows in the resultset**, so if you don't limit the records, there might be a negative performance impact (but Yamo is smart enough to read only primary keys and not to create all the entities when not necessary).

For example, let's imagine joining our `Article` and `ArticlePart` tables and getting following resultset:

| Article.Id | ...  | ArticlePart.Price |
| ---------- | ---- | ----------------- |
| 1          | ...  | 10                |
| 1          | ...  | 11                |
| 2          | ...  | 12                |
| 2          | ...  | 13                |
| 3          | ...  | 14                |
| 1          | ...  | 15                |

This is how the processing will behave using different `CollectionNavigationFillBehavior` values:

```cs
using (var db = CreateContext())
{
    // processes only first row
    var result1 = db.From<Article>()
                    .LeftJoin<ArticlePart>((a, ap) => a.Id == ap.ArticleId)
                    .OrderBy((ArticlePart ap) => ap.Price)
                    .SelectAll().FirstOrDefault();

    Assert.AreEqual(1, result1.Id);
    Assert.AreEqual(1, result1.Parts.Count);
    Assert.AreEqual(10, result1.Parts[0].Price);

    var result2 = db.From<Article>()
                    .LeftJoin<ArticlePart>((a, ap) => a.Id == ap.ArticleId)
                    .OrderBy((ArticlePart ap) => ap.Price)
                    .SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessOnlyFirstRow);

    // result2 is same as result1

    // -----------------------------------

    // processes only first 2 rows
    var result3 = db.From<Article>()
                    .LeftJoin<ArticlePart>((a, ap) => a.Id == ap.ArticleId)
                    .OrderBy((ArticlePart ap) => ap.Price)
                    .SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessUntilMainEntityChange);

    Assert.AreEqual(1, result3.Id);
    Assert.AreEqual(2, result3.Parts.Count);
    Assert.AreEqual(10, result3.Parts[0].Price);
    Assert.AreEqual(11, result3.Parts[1].Price);

    // -----------------------------------

    // processes all rows
    var result4 = db.From<Article>()
                    .LeftJoin<ArticlePart>((a, ap) => a.Id == ap.ArticleId)
                    .OrderBy((ArticlePart ap) => ap.Price)
                    .SelectAll().FirstOrDefault(CollectionNavigationFillBehavior.ProcessAllRows);

    Assert.AreEqual(1, result4.Id);
    Assert.AreEqual(3, result4.Parts.Count);
    Assert.AreEqual(10, result3.Parts[0].Price);
    Assert.AreEqual(11, result3.Parts[1].Price);
    Assert.AreEqual(15, result3.Parts[2].Price);
}
```

###### Planned features:

- [#27](https://github.com/esentio/Yamo/issues/27) Allow nested selects in joins.

#### Select

It is important to remark that calling `SelectAll` doesn't hit the database yet. You need to call `ToList` or `FirstOrDefault` methods.

To return number of rows in the resultset, use `SelectCount` method which translates to `SELECT COUNT(*)`. 

```c#
using (var db = CreateContext())
{
    var articlesCount = db.From<Article>().SelectCount();
}
```

#####Excluding columns

So far we only showed limited selecting possibilities using `SelectAll`, which translates to `SELECT <all_column_from_all_queried_tables>`. However, it is possible to exclude certain columns or tables with `Exclude` and `ExcludeTX` methods.

In one the examples above, it is actually not necessary to select columns from `ArticleCategory` junction table. Here is a simplified query, where columns of this junction table are excluded from the select statement:

```c#
using (var db = CreateContext())
{
    var list = db.From<Article>()
                 .LeftJoin<ArticleCategory>(j => j.T1.Id == j.T2.ArticleId)
                 .LeftJoin<Category>(j => j.T2.CategoryId == j.T3.Id)
                 .As(j => j.T1.Categories)
                 .SelectAll().Exclude(j => j.T1.Price).ExcludeT2().ToList();
}
```

Column with price is excluded as well, so every returned `Article` record will have price set to `default(decimal)`. Useful when you need to exclude large BLOB values, etc.

##### Including columns

You can also include columns and fill properties of the entities not defined in the model using `Include` method.

This for example adds a new column to SQL resultset with `Price * 0.9` value and assign that value to `Article.PriceWithDiscount` property:

```cs
using (var db = CreateContext())
{
    var list = db.From<Article>()
                 .SelectAll()
                 .Include(x => x.PriceWithDiscount, x => x.Price * 0.9m)
                 .ToList();
}
```

VB.NET allows to use event nicer assignment syntax:

```vbnet
Using db = CreateDbContext()
  Dim list = db.From(Of Article).
                SelectAll().
                Include(Sub(x) x.PriceWithDiscount = x.Price * 0.9D).
                ToList()
End Using
```

Every expression used in `Include` method will be added as an additional column (or multiple columns) to the `SELECT` statement. If you don't need columns automatically added by `SelectAll` method, you need to exclude them, like in the following examples:

```cs
// Exclude ArticleCategory columns to make GROUP BY work:
using (var db = CreateContext())
{
    var list = db.From<Category>()
                 .LeftJoin<ArticleCategory>(j => j.T1.Id == j.T2.CategoryId)
                 .GroupBy(j => j.T1)
                 .SelectAll()
                 .ExcludeT2()
                 .Include(j => j.T1.ArticleCount, j => Yamo.Sql.Aggregate.Count(j.T2.ArticleId))
                 .ToList();
}
```
```cs
// Assuming: modelBuilder.Entity<Article>().HasOne(x => x.Label);
// Exclude Label columns to not create Label object and to not assign it to Article.Label property.
// Only Article columns and Label.Description column will be in the resultset.
using (var db = CreateContext())
{
    var list = db.From<Article>()
                 .LeftJoin<Label>(j => j.T1.Id == j.T2.Id)
                 .SelectAll()
                 .ExcludeT2()
                 .Include(j => j.T1.LabelDescription, j => j.T2.Description)
                 .ToList();
}
```
```cs
// If ExcludeT2() were not called, Label columns would be included twice (and Article.Label property would be filled).
using (var db = CreateContext())
{
    var list = db.From<Article>()
                 .LeftJoin<Label>(j => j.T1.Id == j.T2.Id)
                 .SelectAll()
                 .ExcludeT2()
                 .Include(j => j.T1.Tag, j => j.T2)
                 .ToList();
}
```
You can include simple scalar values like, whole entity, `ValueTuple` (although only VB.NET [supports this](https://github.com/dotnet/roslyn/issues/12897)) and anonymous object (probably useful only in limited number of use cases due to casting issues).

**Important note:** if whole entity is included, it's always a "detached copy" unrelated to what would normally work using `SelectAll` method. This means:

- any `Exclude` method on that entity is ignored for `Include` entity instance (it always contains all columns defined in model configuration)
- no relationships are set for `Include` entity instance
- `Include` entity instance is not used in any relationship

If you don't need a "detached copy", it's probably just better to use `As` method to define an ad hoc relationship.

**Important note:** `Include` is only available when `SelectAll()` is used. It's not supported in custom selects, i.e. when `Select(<expression>)` is used!

#####Custom selects

Returning just entity POCOs or simple count would be very limiting. But you can use custom selects.

You can return simple value(s):

```c#
using (var db = CreateContext())
{
    // get prices of all articles
    var prices = db.From<Article>()
                   .Select(a => a.Price)
                   .ToList();
}
```

Entity POCOs:

````c#
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
````

Or anonymous types when you need complex result:

````c#
using (var db = CreateContext())
{
    // get ids of original articles plus articles and their substitutions
    var list = db.From<ArticleSubstitution>()
                 .Join<Article>(j => j.T1.OriginalArticleId == j.T2.Id)
                 .Join<Article>(j => j.T1.SubstitutionArticleId == j.T3.Id)
                 .Select(j => new {
                     OriginalId = j.T2.Id,
                     Original = j.T2,
                     Substitution = j.T3
                 })
                 .ToList();
}
````

In VB.NET you can even return `ValueTuple`. C# [doesn't allow that](https://github.com/dotnet/roslyn/issues/12897) currently.

````vbnet
Using db = CreateDbContext()
  Dim list = db.From(Of ArticleSubstitution).
                Join(Of Article)(Function(j) j.T1.OriginalArticleId = j.T2.Id).
                Join(Of Article)(Function(j) j.T1.SubstitutionArticleId == j.T3.Id).
                Select(Function(x) (OriginalId:=j.T2.Id, Original:=j.T2, Substitution:=j.T3)).
                ToList()
End Using
````

**Important note:** when custom select is used, Yamo doesn't set any relationship properties. You can return multiple entities with anonymous type (or `ValueTuple`), but you have to build entity hierarchy by yourself in postprocessing (if you need that). Number of returned objects matches number of rows in resultset. So be aware that any 1:N relationship join will result to copies of parent entity.

#### Distinct

To retrieve the only distinct records, use `Distinct` method:

````c#
using (var db = CreateContext())
{
    // get all unique languages
    var languages = db.From<Label>()
                      .Select(l => l.Language)
                      .Distinct()
                      .ToList();
}
````

#### Group by and having

To define `GROUP BY` clause, use `GroupBy` method. You can group by single column:

````c#
using (var db = CreateContext())
{
    // get how many articles have the same price
    var pricelist = db.From<Article>()
                      .GroupBy(a => a.Price)
                      .Select(a => new
                      {
                          a.Price,
                          ArticleCount = Sql.Aggregate.Count()
                      })
                      .ToList();
}
````

Or by multiple columns using anonymous type (or even `ValueTuple` in VB.NET).

````c#
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
                     SubstitutionsCount = Sql.Aggregate.Count(j.T1.SubstitutionArticleId)
                 })
                 .ToList();
}
````

Notice that when you need to group by all columns of an entity, you don't need to explicitly enumerate them. Just use that entity in the grouping. For illustration, here is generated SQL from our example:

````sql
SELECT [T1].[Id] [C0_0], [T1].[Price] [C0_1], [T2].[Description] [C1], COUNT([T0].[SubstitutionArticleId]) [C2]
FROM [ArticleSubstitution] [T0]
INNER JOIN [Article] [T1] ON ([T0].[OriginalArticleId] = [T1].[Id])
INNER JOIN [Label] [T2] ON ((([T2].[TableId] = @p0) AND ([T2].[Id] = [T1].[Id])) AND ([T2].[Language] = @p1))
GROUP BY [T1].[Id], [T1].[Price], [T2].[Description]
````

And finally, you can use `Having` method to define ` HAVING` clause.

````c#
using (var db = CreateContext())
{
    // get tables which have at least 10 translations
    var tables = db.From<Label>()
                   .GroupBy(l => l.TableId)
                   .Having(l => 10 < Sql.Aggregate.Count())
                   .Select(l => l.TableId)
                   .ToList();
}
````

#### Limiting number of returned rows

You can constrain the number of rows returned by the query with a `Limit` method. You can specify number of returned rows as well as offset.

````c#
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
````

This will translate to appropriate `LIMIT`, `TOP` or `OFFSET FETCH` clauses depending on the database.

**Important note:** it is generally recommended to use `ORDER BY` together with `LIMIT` or `TOP` clauses. But databases allow to use them without ordering and so does Yamo. In MS SQL Server, `Limit(count)` is translated to `TOP`. But `Limit(offset, count)` is translated to `OFFSET FETCH`, which forces you to use `ORDER BY` clause. Expect an exception, when you forget to use it.

**Important note:** `Limit` translates directly to SQL and affects the number of rows in the resultset. Keep that in mind when you use it together with 1:N joins. Not all joined entities which actually belong to last main entity from the output might be present in its relationship property list. Also, number or main entities might be lower that you specify in `count` parameter, because of join multiplications.

### Conditionally built queries

Often you need to build query dynamically. Add where conditions based on user input. Join a table if certain filter criteria are applied, but avoid joining otherwise for better performance. The list goes on.

Yamo has built-in `If` method, which can be used to conditionally build select queries using fluent style API.

Here is an example of conditionally applying where clause:

```cs
using (var db = CreateContext())
{
    int? priceFilter = 42;

    var list = db.From<Article>()
                 .If(priceFilter.HasValue, exp => exp.Where(a => a.Price < priceFilter.Value))
                 .SelectAll().ToList();
}
```

You can provide both if and else variants if you want:

```cs
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
```

You are not limited to `Where`, here we conditionally join another table:

```cs
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
```

Keep in mind that conditional joins have consequences. Affected entity and its properties will be `null` if you use them later:
```cs
using (var db = CreateContext())
{
    var includeLabel = false;

    // Label and Description properties will be set to null
    var list = db.From<Article>()
                 .If(includeLabel, exp => exp.Join<Label>((a, l) => l.Id == a.Id))
                 .Select((a, l) => new { Article = a, Label = l, Description = l.Description})
                 .ToList();
}
```

Behavior is following:
- If condition is not met, we don't append affected clauses to SQL string at all.
- Conditions could be nested (`If` inside `If`).
- Expressions inside `If` could be chained, for example `Where(...).OrderBy(...)`.
- When method `SelectAll`, `SelectCount`, `Select`, `ToList` or `FirstOrDefault` is used inside `If` without providing `otherwise` parameter and when condition is not met, an `InvalidOperationException` is thrown (this scenario does not make much sense).
- If both `then` and `otherwise` expressions are provided as parameters, they must be of the same type.
- If there is conditional join and condition is not met, behavior is following:
  - When `SelectAll` method is used, affected reference navigation property will be set to `null` and no record will be added to affected collection navigation property.
  - If property from affected joined entity is used in a clause (`Where`, `OrderBy`, custom `Select`, etc.), `NULL` will be used in an output SQL statement instead of that column.
  - If whole affected joined entity is used in `GroupBy` or in custom `Select`, all columns normally added to an SQL statement will be replaced with `NULL` value.

### Advanced queries

Sometimes, you really need to write your query manually. How to get simple value via raw SQL was already described [above](#simple-queries). But `Query` and `QueryFirstOrDefault` methods can do more than that.

You can return multiple values as a `ValueTuple` or nullable `ValueTuple`:

```c#
using (var db = CreateContext())
{
    var login = "foo";
    var data = db.QueryFirstOrDefault<(int, string)?>($"SELECT Id, Email FROM [User] WHERE Login = {login}");
    var list = db.Query<(int Login, int Email)>("SELECT Login, Email FROM [User]");
}
```

You can even get whole model entity object! To simplify writing all columns to the query (they need to be stated in correct order) just use `Yamo.Sql.Model.Columns` helper:

```c#
using (var db = CreateContext())
{
    var articles = db.Query<Article>($"SELECT {Sql.Model.Columns<Article>()} FROM Article");

    var data = db.QueryFirstOrDefault<(decimal, Label, Label)?>($@"
        SELECT a.Price,
        {Sql.Model.Columns<Label>("le")},
        {Sql.Model.Columns<Label>("lg")}
        FROM Article AS a
        LEFT JOIN Label AS le ON a.Id = le.Id AND le.Language = 'en'
        LEFT JOIN Label AS lg ON a.Id = lg.Id AND lg.Language = 'de'
        WHERE a.Id = 1");
}
```

**Important note:** similarly to `Select` method, `Query` and `QueryFirstOrDefault` don't set any relationship properties and you have to do it by yourself in postprocessing (if you need to).

### Overriding table name

Sometimes the name of the table is generated dynamically. Or there are multiple versions of the same table (with different prefix/suffix). Or we store old records in an archive table (with the same structure). That might be a problem, because once the model definition is cached, it cannot be changed. We can always create new `DbContext` class, but that's not always very handy.

For these scenarios Yamo allows you to ad hoc override table name in the queries:

```cs
using (var db = CreateContext())
{
    var tableName = "ArticleArchive";
    var article = new Article() {Id = 42, Price = 10};

    db.Insert<Article>(tableName).Execute(article);

    article.Price = 11;

    db.Update<Article>(tableName).Execute(article);

    db.SoftDelete<Article>(tableName).Execute(article);
    
    db.Delete<Article>(tableName).Execute(article);
}
```

Select statements even allow you to use table source (in both `FROM` and `JOIN` clauses), so you can write nested selects:

```cs
using (var db = CreateContext())
{
    var lang = "en";
    var list = db.From<Article>("ArticleArchive")
                 .Join<Label>($"(SELECT {Yamo.Sql.Model.Columns<Label>()} FROM LabelArchive WHERE Language = {lang})")
                 .On((a, l) => l.Id == a.Id)
                 .SelectAll().ToList();
}
```

In the future it should be possible to write nested selects also with managed API.

### Table hints

If you need to specify table hints in your queries, you can do so using `WithHints` method.

Hints are supported in `SELECT` statements:

```cs
using (var db = CreateContext())
{
    var list = db.From<Article>().WithHints("WITH (TABLOCK)")
                 .Join<Label>().WithHints("WITH (NOLOCK)").On((a, l) => l.Id == a.Id)
                 .SelectAll().ToList();
}
```

And also in `INSERT`, `UPDATE` and `DELETE` statements:

```cs
using (var db = CreateContext())
{
    var blog = new Blog() { Title = "Lorem ipsum", Content = ""};

    db.Insert<Blog>().WithHints("WITH (TABLOCK)").Execute(blog);

    blog.Content = "TODO";
    db.Update<Blog>().WithHints("WITH (TABLOCK)").Execute(blog);

    db.Delete<Blog>().WithHints("WITH (TABLOCK)").Execute(blog);
}
```

### Logging

There is no build-in support for logging, but you can override `OnCommandExecuting` method in `DbContext` to intercept all queries and use whatever logging framework you like.

````c#
protected override void OnCommandExecuting(DbCommand command)
{
    // log command.CommandText
}
````

### Performance

General goal is to make Yamo as fast as possible. For that purpose - like in other frameworks - lot of methods are code-generated in runtime. Everything is done using Expression Trees API; Reflection Emit (`ILGenerator`) is not used.

Current benchmarks are promising. But still, there is a place for improvements :-)

Below is comparison between Yamo, Dapper and EF Core (full reports [here](../../tree/master/Benchmarks)):

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.959 (1909/November2018Update/19H2)
Intel Core i7 CPU 950 3.07GHz (Nehalem), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.200
  [Host]     : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
  DefaultJob : .NET Core 3.1.2 (CoreCLR 4.700.20.6602, CoreFX 4.700.20.6702), X64 RyuJIT
```
#### Select 1 record

| Method                          |     Mean | Ratio | Allocated |
| ------------------------------- | -------: | ----: | --------: |
| Dapper                          | 112.9 μs |  0.89 |   5.39 KB |
| Yamo                            | 126.6 μs |  1.00 |    7.1 KB |
| Yamo (using query)              | 136.5 μs |  1.09 |   6.98 KB |
| EF Core (no tracking)           | 551.8 μs |  4.43 |  36.45 KB |
| EF Core                         | 644.1 μs |  5.31 |  38.23 KB |

#### Select 500 records one by one

| Method                |      Mean | Ratio | Allocated |
| --------------------- | --------: | ----: | --------: |
| Dapper                |  56.70 ms |  0.68 |   2.64 MB |
| Yamo (using query)    |  65.93 ms |  0.79 |   3.31 MB |
| Yamo                  |  84.15 ms |  1.00 |    4.2 MB |
| EF Core               | 147.72 ms |  1.77 |   7.25 MB |
| EF Core (no tracking) | 154.32 ms |  1.69 |   6.78 MB |

#### Select list of 1000 records

| Method                |      Mean | Ratio |  Allocated |
| --------------------- | --------: | ----: | ---------: |
| Yamo                  |  4.009 ms |  1.00 |  193.88 KB |
| Yamo (using query)    |  4.080 ms |  1.01 |  193.72 KB |
| Dapper                |  4.107 ms |  1.02 |   317.5 KB |
| EF Core (no tracking) |  4.944 ms |  1.34 |   434.3 KB |
| EF Core               | 12.951 ms |  4.85 | 1416.51 KB |

#### Select list of 1000 records with 1:1 join

| Method                |      Mean | Ratio |  Allocated |
| --------------------- | --------: | ----: | ---------: |
| Yamo                  |  6.572 ms |  1.00 |  464.71 KB |
| Dapper                |  6.614 ms |  1.00 |   607.5 KB |
| EF Core (no tracking) |  7.917 ms |  1.16 |  832.96 KB |
| EF Core               | 31.282 ms |  5.70 | 3141.84 KB |

#### Select list of 1000 records with 1:N join

| Method                |      Mean | Ratio | Allocated |
| --------------------- | --------: | ----: | --------: |
| EF Core (no tracking) |  49.69 ms |  0.86 |   2.75 MB |
| Dapper                |  56.50 ms |  0.98 |   2.96 MB |
| Yamo                  |  57.47 ms |  1.00 |   2.45 MB |
| EF Core               | 103.01 ms |  1.80 |   8.17 MB |

