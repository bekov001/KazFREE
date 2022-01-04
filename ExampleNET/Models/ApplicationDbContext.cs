using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExampleNET.Models;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> MyUsers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}