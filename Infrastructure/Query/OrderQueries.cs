namespace SafnamBackend.Infrastructure.Query
{
    public static class OrderQueries
    {
        public const string GetAll = "SELECT * FROM Orders";

        public const string GetPending = "SELECT * FROM Orders WHERE Status = 'Pending'";

        public const string GetById = @"SELECT Id, UserId, Address, TotalAmount, PaymentStatus, Status 
                                       FROM Orders WHERE Id=@Id";

        // 🔥 NEW
        public const string GetByUser = "SELECT * FROM Orders WHERE UserId=@UserId";

        // 🔥 NEW (for order details)
        public const string GetItems = @"SELECT 
                                            oi.Id,
                                            oi.OrderId,
                                            oi.MenuId,
                                            oi.Quantity,
                                            m.ItemName
                                        FROM OrderItems oi
                                        INNER JOIN Menu m ON oi.MenuId = m.Id
                                        WHERE oi.OrderId = @Id";

        // 🔥 NEW (for price calculation)
        public const string GetMenuPrice = "SELECT Price FROM Menu WHERE Id=@Id";


        public const string InsertOrder = @"INSERT INTO Orders(UserId,Address,TotalAmount,PaymentStatus,Status)
                                           OUTPUT INSERTED.Id
                                           VALUES(@UserId,@Address,@TotalAmount,@PaymentStatus,@Status)";

        public const string InsertItem = @"INSERT INTO OrderItems(OrderId,MenuId,Quantity)
                                          VALUES(@OrderId,@MenuId,@Quantity)";

        public const string DeleteItems = "DELETE FROM OrderItems WHERE OrderId=@Id";
        public const string DeleteOrder = "DELETE FROM Orders WHERE Id=@Id";
    }
}