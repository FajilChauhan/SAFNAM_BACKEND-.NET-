namespace SafnamBackend.Infrastructure.Query
{
    public static class RestaurantTableQueries
    {
        public const string GetAll = "SELECT * FROM RestaurantTables";

        public const string GetById = "SELECT * FROM RestaurantTables WHERE Id=@Id";

        public const string Insert = @"INSERT INTO RestaurantTables(TableNo,Floor,ExtraCharge)
                                      VALUES(@TableNo,@Floor,@ExtraCharge)";

        public const string Update = @"UPDATE RestaurantTables 
                                      SET TableNo=@TableNo,
                                          Floor=@Floor,
                                          ExtraCharge=@ExtraCharge
                                      WHERE Id=@Id";

        public const string Delete = "DELETE FROM RestaurantTables WHERE Id=@Id";
    }
}