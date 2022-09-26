public interface ICoinsRepository
{
    IEnumerable<CoinModel> GetAll();
    void AddCoin(CoinModel coin);
    void UpdateCoinHistory(CoinModel coin, string destination);
    CoinModel GetACoinWithTheLongestHistory();
}