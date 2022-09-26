public class UserModel
{
    public string Name { get; set; }
    public long Rating { get; set; }
    public long Amount { get; set; }
    public UserModel(string name, long rating)
    {
        Name = name;
        Rating = rating;
        Amount = 0;
    }
}