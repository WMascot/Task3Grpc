public class CoinModel 
{
    public int Id { get; set; }
    public string History { get; set;}
    public int HistoryCounter { get; set; }
    public string CurrentUser { get; set; }
    public CoinModel(int id, string destination)
    {
        Id = id;
        History = $"From emission to {destination}.";
        CurrentUser = destination;
        HistoryCounter = 1;
    }
    public void UpdateHistory(string destination)
    {
        History += $" From {CurrentUser} to {destination}.";
        HistoryCounter++;
        CurrentUser = destination;
    }
}