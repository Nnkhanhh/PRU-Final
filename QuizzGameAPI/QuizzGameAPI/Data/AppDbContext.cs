using Microsoft.EntityFrameworkCore;
using QuizzGameAPI.Models;

namespace QuizAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<QuizQuestion> QuizQuestions { get; set; }
    }
}
