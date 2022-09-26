public class UsersRepository : IUsersRepository
{
    private List<UserModel> _users;
    public UsersRepository()
    {
        _users = InitializeUsers();
    }
    public IEnumerable<UserModel> GetAll()
    {
        return _users;
    }
    public UserModel GetUserByName(string name)
    {
        var user = _users.Where(x => x.Name == name).First();
        return user;
    }
    public void ChangeCoinsAmount(UserModel user, long amount, bool add)
    {
        if (add) _users.Where(x => x == user).First().Amount += amount;
        else _users.Where(x => x == user).First().Amount -= amount;
    }
    private List<UserModel> InitializeUsers()
    {
        List<UserModel> users = new();
        users.Add(new UserModel("boris", 5000));
        users.Add(new UserModel("maria", 1000));
        users.Add(new UserModel("oleg", 800));
        
        return users;
    }
}