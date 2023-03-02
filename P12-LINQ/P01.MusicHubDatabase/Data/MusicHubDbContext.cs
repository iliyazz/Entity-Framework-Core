﻿namespace MusicHub.Data;

using Microsoft.EntityFrameworkCore;
using Models;

public class MusicHubDbContext : DbContext
{
    public MusicHubDbContext()
    {
    }

    public MusicHubDbContext(DbContextOptions options)
        : base(options)
    {
    }

    DbSet<Producer> Producers { get; set; }
    DbSet<Album> Albums { get; set; }
    DbSet<Song> Songs { get; set; }
    DbSet<Performer> Performers { get; set; }
    DbSet<Writer> Writers { get; set; }
    DbSet<SongPerformer> SongsPerformers { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Configuration.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Song>(entity =>
        {
            entity
                .Property(s => s.CreatedOn)
                .HasColumnType("date");
            //entity
            //    .Property(s => s.Name)
            //    .IsUnicode(false)
            //    .IsRequired(true)
            //    .HasMaxLength(50);
        });
        builder.Entity<Album>(entity =>
        {
            entity
                .Property(a => a.ReleaseDate)
                .HasColumnType("date");
        });
        builder.Entity<SongPerformer>(entity =>
        {
            entity.HasKey(sp => new { sp.PerformerId, sp.SongId });
        });
			
    }
}