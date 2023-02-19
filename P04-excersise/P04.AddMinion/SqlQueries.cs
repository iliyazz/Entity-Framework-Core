namespace P04.AddMinion
{

    public static class SqlQueries
    {
        public const string AddNewTown = @"INSERT INTO Towns (Name) VALUES (@townName)";
        public const string GetTownIdByName = @"SELECT Id FROM Towns WHERE Name = @townName";
        public const string AddVillainWithDefaultEvilnessFactor =  @"INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
        public const string GetVillainIdByName = @"SELECT Id FROM Villains WHERE Name = @Name";
        public const string AddNewMinion = @"INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";
        public const string GetMinionByName = @"SELECT Id FROM Minions WHERE Name = @Name";
        public const string SetMinionToBeServantOfVillain =
            @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";
    }
}
