namespace SafnamBackend.Infrastructure.Query
{
    public static class RoomBookingQueries
    {
        public const string GetAll = @"SELECT 
                                        Rb.Id,
                                        R.RoomNo,
                                        U.Username,
                                        Rb.CheckIn,
                                        Rb.CheckOut,
                                        Rb.Status,
                                        Rb.PaymentStatus,
                                        Rb.TotalAmount
                                      FROM RoomBookings Rb
                                      LEFT JOIN Rooms R ON R.Id = Rb.RoomId
                                      LEFT JOIN Users U ON U.Id = Rb.UserId";

        public const string GetByRoom = "SELECT * FROM RoomBookings WHERE RoomId=@RoomId";

        public const string GetByUser = @"SELECT 
                                        Rb.Id,
                                        R.RoomNo,
                                        U.Username,
                                        Rb.CheckIn,
                                        Rb.CheckOut,
                                        Rb.Status,
                                        Rb.PaymentStatus,
                                        Rb.TotalAmount
                                      FROM RoomBookings Rb
                                      LEFT JOIN Rooms R ON R.Id = Rb.RoomId
                                      LEFT JOIN Users U ON U.Id = Rb.UserId
                                      WHERE Rb.UserId=@UserId";

        public const string GetActive = @"SELECT 
                                    Rb.Id,
                                    R.RoomNo,
                                    U.Username,
                                    Rb.CheckIn,
                                    Rb.CheckOut,
                                    Rb.Status,
                                    Rb.PaymentStatus,
                                    Rb.TotalAmount
                                 FROM RoomBookings Rb
                                 LEFT JOIN Rooms R ON R.Id = Rb.RoomId
                                 LEFT JOIN Users U ON U.Id = Rb.UserId
                                 WHERE Rb.CheckOut >= CAST(GETDATE() AS DATE)
                                 AND (Rb.Status IS NULL OR Rb.Status != 'Cancelled')";

        public const string GetRoomPrice = "SELECT PricePerDay FROM Rooms WHERE Id=@Id";

        public const string Insert = @"INSERT INTO RoomBookings
            (RoomId, UserId, CheckIn, CheckOut, Status, PaymentStatus, TotalAmount)
            VALUES
            (@RoomId, @UserId, @CheckIn, @CheckOut, @Status, @PaymentStatus, @TotalAmount)";

        public const string Update = @"UPDATE RoomBookings
            SET RoomId=@RoomId,
                UserId=@UserId,
                CheckIn=@CheckIn,
                CheckOut=@CheckOut,
                Status=@Status,
                PaymentStatus=@PaymentStatus,
                TotalAmount=@TotalAmount
            WHERE Id=@Id";

        public const string Delete = "DELETE FROM RoomBookings WHERE Id=@Id";

        public const string CheckConflict = @"SELECT COUNT(*) 
                                            FROM RoomBookings
                                            WHERE RoomId = @RoomId
                                            AND (@CheckIn < CheckOut AND @CheckOut > CheckIn)";
    }
}