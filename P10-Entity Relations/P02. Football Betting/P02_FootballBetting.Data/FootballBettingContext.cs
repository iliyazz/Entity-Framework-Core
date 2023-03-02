namespace P02_FootballBetting.Data
{
    using Common;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class FootballBettingContext : DbContext
    {
        //Testing Use it when developing  the application
        public FootballBettingContext()
        {

        }
        //Judge used by Judge. Loading of DbContext itn DI. in real app is usefull
        public FootballBettingContext(DbContextOptions options)
        : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerStatistic> PlayersStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }


        //Establish connection to the SQL server 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //True -> Connection string is already set
            //false -> Connection string is not set

            if (!optionsBuilder.IsConfigured)
            {
                //set default connection string
                optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
            }
        }

        //Define rules for creating database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Composite PK for mapping entity
            modelBuilder.Entity<PlayerStatistic>(entity =>
                {
                    //Configuration for the current Entity(PlayerStatistic)
                    entity.HasKey(ps => new { ps.PlayerId, ps.GameId });
                });
            modelBuilder.Entity<Team>(entity =>
            {
                entity
                    .HasOne(t => t.PrimaryKitColor)
                    .WithMany(c => c.PrimaryKitTeams)
                    .HasForeignKey(t => t.PrimaryKitColorId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(t => t.SecondaryKitColor)
                    .WithMany(c => c.SecondaryKitTeams)
                    .HasForeignKey(t => t.SecondaryKitColorId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Game>(entity =>
            {
                entity
                    .HasOne(g => g.HomeTeam)
                    .WithMany(t => t.HomeGames)
                    .HasForeignKey(g => g.HomeTeamId)
                    .OnDelete(DeleteBehavior.NoAction);
                entity
                    .HasOne(g => g.AwayTeam)
                    .WithMany(t => t.AwayGames)
                    .HasForeignKey(g => g.AwayTeamId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

        }

        //private void DiscoverDbSets()
        //{
        //    Assembly assembly = Assembly.GetAssembly(typeof(Player));
        //    Type[] entities = assembly.GetTypes();

        //    Type DbContext this.GetType();
        //    foreach (Type entity in entities)
        //    {
        //        object dbSet = typeof(DbSet<>)
        //            .MakeGenericType(entity);
        //    }
        //}
    }
}