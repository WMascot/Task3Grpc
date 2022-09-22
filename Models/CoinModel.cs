public class CoinModel 
{
    public int Id { get; set; }
    public string History { get; set;}
    public int HistoryCounter { get; set; }
    public string CurrentUser { get; set; }
    public CoinModel(int id, string destination)
    {
        Id = id;
        History = $"From emission to {destination}\\n";
        CurrentUser = destination;
        HistoryCounter = 1;
    }
}