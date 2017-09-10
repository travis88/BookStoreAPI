using Microsoft.EntityFrameworkCore;

namespace AspNetCorePublisherWebAPI.Entities
{
    /// <summary>
    /// Контекст для соединения с БД
    /// </summary>
    public class SqlDbContext : DbContext
    {
        /// <summary>
        /// Издатели
        /// </summary>
        /// <returns></returns>
        public DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Книги
        /// </summary>
        /// <returns></returns>
        public DbSet<Book> Books { get; set; }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) 
            : base(options) { }
    }
}