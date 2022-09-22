public class UserModel
{
    public string Name { get; set; }
    public int Rating { get; set; }
    public int Amount { get; set; }
    public UserModel(string name, int rating)
    {
        Name = name;
        Rating = rating;
        Amount = 0;
    }
}