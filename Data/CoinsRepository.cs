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
        _coins.Add(coin);
    }
    public void UpdateCoinHistory(CoinModel coin, string destination)
    {
        _coins.Where(x => x == coin).First().UpdateHistory(destination);
    }
    public CoinModel GetACoinWithTheLongestHistory()
    {
        var coin = _coins.OrderByDescending(x => x.HistoryCounter).First();
        return coin;
    }
}