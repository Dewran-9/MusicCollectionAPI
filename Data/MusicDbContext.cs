using Microsoft.EntityFrameworkCore;

public class MusicDbContext : DbContext
{
    public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
    {
    }

    public DbSet<MusicRecord> MusicRecords { get; set; }
}