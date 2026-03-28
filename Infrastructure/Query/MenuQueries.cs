namespace SafnamBackend.Infrastructure.Query;


public static class MenuQueries
{
    public const string GetAll = "SELECT * FROM Menu";

    public const string Insert = @"INSERT INTO Menu(ItemName,ImagePath,Price,Type,IsAvailable)
                                  VALUES(@ItemName,@ImagePath,@Price,@Type,@IsAvailable)";

    public const string UpdateWithoutImage = @"UPDATE Menu 
        SET ItemName=@ItemName, Price=@Price, Type=@Type, IsAvailable=@IsAvailable 
        WHERE Id=@Id";

    public const string UpdateWithImage = @"UPDATE Menu 
        SET ItemName=@ItemName, ImagePath=@ImagePath, Price=@Price, Type=@Type, IsAvailable=@IsAvailable 
        WHERE Id=@Id";

    public const string Delete = "DELETE FROM Menu WHERE Id=@Id";
}