namespace SafnamBackend.Infrastructure.Query
{
    public static class TableBookingQueries
    {
        public const string GetAll = "SELECT T.Id, T.TableId, RT.TableNo, RT.Floor, T.UserId," +
            "U.UserName, T.BookingDate, T.TimeSlot, T.Status FROM TableBookings T LEFT JOIN RestaurantTables RT On RT.Id = T.TableId LEFT JOIN " +
            "Users U On U.Id = T.UserId";

        public const string GetActive = "SELECT T.Id, T.TableId, RT.TableNo, RT.Floor, T.UserId," +
            "U.UserName, T.BookingDate, T.TimeSlot, T.Status FROM TableBookings as T Left Join RestaurantTables as RT On RT.Id = T.TableId Left Join " +
            "Users as U On U.Id = T.UserId Where T.Status = 'Active'";

        public const string GetByUser = "SELECT T.Id, T.TableId, RT.TableNo, RT.Floor, T.UserId," +
            "U.UserName, T.BookingDate, T.TimeSlot, T.Status FROM TableBookings as T Left Join RestaurantTables as RT On RT.Id = T.TableId Left Join " +
            "Users as U On U.Id = T.UserId Where T.UserId = @UserId";

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
