namespace P05.ChangeTownNamesCasing
{

    public static class SqlQueries
    {
        public const string SelectTownByCountry =
            @"SELECT Name
                FROM Towns
                WHERE CountryCode =
                                   (
                                    SELECT Id
                                    FROM Countries
                                    WHERE name = @countryName
                                   )";

        public const string UpdateTownByCountry =
            @"UPDATE Towns
                   SET
                       Name = UPPER(Name)
                 WHERE CountryCode =
                                    (
                                     SELECT Id
                                     FROM Countries
                                     WHERE name = @countryName
                                    )";
    }
}
