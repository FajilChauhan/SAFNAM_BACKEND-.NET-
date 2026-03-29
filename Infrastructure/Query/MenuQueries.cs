namespace SafnamBackend.Infrastructure.Query;


public static class MenuQueries
{
    public const string GetAll = "SELECT * FROM Menu";

    public const string GetById = "SELECT * FROM Menu WHERE Id=@Id";

    public const string Insert = @"INSERT INTO Menu(ItemName,ImagePath,Price,Type,IsAvailable, IsActive)
                                  VALUES(@ItemName,@ImagePath,@Price,@Type,@IsAvailable, 1)";

    public const string GetAllActive = "SELECT * FROM Menu WHERE IsActive = 1";

    public const string UpdateStatus = @"UPDATE Menu SET IsActive = @IsActive WHERE Id = @Id";

    public const string UpdateWithoutImage = @"UPDATE Menu 
        SET ItemName=@ItemName, Price=@Price, Type=@Type, IsAvailable=@IsAvailable 
        WHERE Id=@Id";

    public const string UpdateWithImage = @"UPDATE Menu 
        SET ItemName=@ItemName, ImagePath=@ImagePath, Price=@Price, Type=@Type, IsAvailable=@IsAvailable 
        WHERE Id=@Id";

    public const string Delete = "DELETE FROM Menu WHERE Id=@Id";

    public const string IsUsedInOrder = "SELECT COUNT(*) FROM OrderItems WHERE MenuId=@MenuId";
}