﻿namespace Footballers.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FootballersContext : DbContext
    {
        public FootballersContext() { }

        public FootballersContext(DbContextOptions options)
            : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamFootballer>()
                .HasKey(p => new { p.TeamId, p.FootballerId });
        }

        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<Coach> Coaches { get; set; } = null!;
        public DbSet<TeamFootballer> TeamsFootballers { get; set; } = null!;
    }
}
