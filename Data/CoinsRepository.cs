public class CoinsRepository : ICoinsRepository
{
    private List<CoinModel> _coins;
    public CoinsRepository()
    {
        _coins = new();
    }
    public IEnumerable<CoinModel> GetAll()
    {
        return _coins;
    }
    public void AddCoin(CoinModel coin)
    {
        throw new NotImplementedException();
    }

    public CoinModel GetACoinWithTheLongestHistory()
    {
        throw new NotImplementedException();
    }
}