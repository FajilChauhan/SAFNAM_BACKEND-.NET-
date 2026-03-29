namespace SafnamBackend.Infrastructure.Query
{
    public static class RestaurantTableQueries
    {
        public const string GetAll = "SELECT * FROM RestaurantTables";

        public const string GetById = "SELECT * FROM RestaurantTables WHERE Id=@Id";

        public const string GetAllActive = "SELECT * FROM RestaurantTables WHERE IsActive = 1";

        public const string Insert = @"INSERT INTO RestaurantTables(TableNo,Floor,ExtraCharge, IsActive)
                                      VALUES(@TableNo,@Floor,@ExtraCharge, 1)";


        public const string UpdateStatus = @"UPDATE RestaurantTables SET IsActive = @IsActive WHERE Id = @Id";

        public const string Update = @"UPDATE RestaurantTables 
                                      SET TableNo=@TableNo,
                                          Floor=@Floor,
                                          ExtraCharge=@ExtraCharge
                                      WHERE Id=@Id";

        public const string Delete = "DELETE FROM RestaurantTables WHERE Id=@Id";
    }
}