namespace SafnamBackend.Infrastructure.Query
{
    public static class TableBookingQueries
    {
        public const string GetAll = "SELECT * FROM TableBookings";

        public const string GetActive = "SELECT * FROM TableBookings WHERE Status='Active'";

        public const string GetByUser = "SELECT * FROM TableBookings WHERE UserId=@UserId";

        public const string GetByTable = "SELECT * FROM TableBookings WHERE TableId=@TableId";

        public const string GetById = "SELECT * FROM TableBookings WHERE Id=@Id";

        public const string Insert = @"INSERT INTO TableBookings
        (TableId,UserId,BookingDate,TimeSlot,Status)
        VALUES(@TableId,@UserId,@BookingDate,@TimeSlot,'Active')";

        public const string Update = @"UPDATE TableBookings
        SET TableId=@TableId,
            UserId=@UserId,
            BookingDate=@BookingDate,
            TimeSlot=@TimeSlot,
            Status=@Status
        WHERE Id=@Id";

        public const string Delete = "DELETE FROM TableBookings WHERE Id=@Id";

        // 🔥 conflict check (same date + same hour)
        public const string CheckConflict = @"SELECT COUNT(*)
        FROM TableBookings
        WHERE TableId=@TableId
        AND BookingDate=@BookingDate
        AND TimeSlot=@TimeSlot
        AND Status='Active'";
    }
}
