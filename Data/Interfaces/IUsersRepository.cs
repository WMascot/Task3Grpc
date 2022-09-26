public interface IUsersRepository
{
    IEnumerable<UserModel> GetAll();
    UserModel GetUserByName(string name);
    void ChangeCoinsAmount(UserModel user, long amount, bool sign);
}