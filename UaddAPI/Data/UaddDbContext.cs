using Microsoft.EntityFrameworkCore;
using UaddAPI.Models;

namespace UaddAPI.Data;

public class UaddDbContext : DbContext
{
    public UaddDbContext(DbContextOptions<UaddDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<NewsPost> NewsPosts => Set<NewsPost>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Internship> Internships => Set<Internship>();
    public DbSet<IntensiveCourse> IntensiveCourses => Set<IntensiveCourse>();

}