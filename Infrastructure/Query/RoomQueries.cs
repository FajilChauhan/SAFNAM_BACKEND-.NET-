namespace SafnamBackend.Infrastructure.Query
{
    public static class RoomQueries
    {
        public const string GetAll = "SELECT * FROM Rooms";

        public const string Insert = @"INSERT INTO Rooms(RoomNo,Type,ImagePath,PricePerDay, IsActive)
                                      VALUES(@RoomNo,@Type,@ImagePath,@PricePerDay, 1)";

        public const string GetAllActive = "SELECT * FROM Rooms WHERE IsActive = 1";

        public const string UpdateStatus = @"UPDATE Rooms SET IsActive = @IsActive WHERE Id = @Id";

        public const string UpdateWithoutImage = @"UPDATE Rooms 
            SET RoomNo=@RoomNo, Type=@Type, PricePerDay=@PricePerDay 
            WHERE Id=@Id";

        public const string UpdateWithImage = @"UPDATE Rooms 
            SET RoomNo=@RoomNo, Type=@Type, PricePerDay=@PricePerDay, ImagePath=@ImagePath 
            WHERE Id=@Id";

        public const string Delete = "DELETE FROM Rooms WHERE Id=@Id";

        // 🔥 check booking reference
        public const string GetBookingCount = "SELECT COUNT(*) FROM RoomBookings WHERE RoomId=@Id";
    }
}