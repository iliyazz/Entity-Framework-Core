namespace P01.InitialSetup
{

    public static class SqlQueries
    {
        public const string inputTableData = @"
             CREATE TABLE Countries(
             Id INT PRIMARY KEY IDENTITY,
             [Name] NVARCHAR(50) NOT NULL,
             )

             CREATE TABLE Towns(
             Id INT PRIMARY KEY IDENTITY,
             [Name] NVARCHAR(50) UNIQUE NOT NULL,
             CountryId INT NOT NULL FOREIGN KEY REFERENCES Countries(Id),
             )

             CREATE TABLE Minions(
             Id INT PRIMARY KEY IDENTITY,
             [Name] NVARCHAR(50) NOT NULL,
             Age INT NOT NULL,
             TownId INT NOT NULL FOREIGN KEY REFERENCES Towns(Id),
             )

             CREATE TABLE Villains(
             Id INT PRIMARY KEY IDENTITY,
             [Name] NVARCHAR(50) NOT NULL,
             EvilnessFactor NVARCHAR(25) NOT NULL,
             )

             CREATE TABLE MinionsVillains(
             MinionId  INT FOREIGN KEY REFERENCES Minions(Id),
             VillainId INT FOREIGN KEY REFERENCES Villains(Id),
             PRIMARY KEY(MinionId, VillainId),
             )

             INSERT INTO Countries
             VALUES
             ('Russia'),
             ('Bulgaria'),
             ('China'),
             ('USA'),
             ('Canada')

             INSERT INTO Towns
             VALUES
             ('Moscow', 1),
             ('Sofia', 2),
             ('Ciung Dzang', 3),
             ('Chicago', 4),
             ('Ontario', 5);

             INSERT INTO Minions
             VALUES
             ('Stuart', 15, 3),
             ('Josh', 7, 4),
             ('Vanq', 35, 1),
             ('Tervel', 29, 2),
             ('John', 47, 5);

             INSERT INTO Villains
             VALUES
             ('Bad Guy', 'Bad'),
             ('Evil Guy', 'Evil'),
             ('Good Dude', 'Good'),
             ('Good Friend', 'Good'),
             ('Super Evil Guy', 'Super evil');

             INSERT INTO MinionsVillains
             VALUES
             (1, 3),
             (2, 4),
             (3, 5),
             (4, 1),
             (5, 2),
             (3, 4),
             (2, 1),
             (5, 3);
";
    }
}
