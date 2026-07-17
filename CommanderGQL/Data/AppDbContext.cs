using CommanderGQL.Commands;
using CommanderGQL.Platforms;
using Microsoft.EntityFrameworkCore;

namespace CommanderGQL.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Command> Commands { get; set; }
}