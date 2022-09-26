public interface ICoinsRepository
{
    IEnumerable<CoinModel> GetAll();
    void AddCoin(CoinModel coin);
    CoinModel GetACoinWithTheLongestHistory();
}