using Grpc.Core;
using Task3Grpc;

namespace Task3Grpc.Services;

public class BillingerService : Billing.BillingBase
{
    public List<UserModel> ListOfUsers { get; set; }
    public List<CoinModel> ListOfCoins { get; set; }
    private readonly ILogger<GreeterService> _logger;
    public BillingerService(ILogger<GreeterService> logger)
    {
        _logger = logger;
        ListOfCoins = new();
        ListOfUsers = new();
        ListOfUsers.Add(new UserModel("boris", 5000));
        ListOfUsers.Add(new UserModel("maria", 1000));
        ListOfUsers.Add(new UserModel("oleg", 800));
    }
    public override Task<Response> CoinsEmission(EmissionAmount request, ServerCallContext context)
    {
        long totalRating = ListOfUsers.Sum(x => x.Rating);
        long usersAmount = ListOfUsers.Count();
        long coinsAmount = request.Amount;

        if (usersAmount > coinsAmount)
        {
            var responseFailed = new Response
            {
                Status = Response.Types.Status.Failed,
                Comment = "Not enough coins for emission."
            };
            return Task.FromResult(responseFailed);
        }

        for (int i = 0; i < usersAmount; i++)
        {
            CoinModel newCoin = new(ListOfCoins.Count() + 1, ListOfUsers[i].Name);
            ListOfCoins.Add(newCoin);
            ListOfUsers[i].Amount++;
        }
        var response = new Response
        {
            Status = Response.Types.Status.Ok,
            Comment = "Successful emission." 
        };
        if (usersAmount == coinsAmount) return Task.FromResult(response);

        coinsAmount -= usersAmount;
        for (int i = 0; i < usersAmount; i++)
        {
            if (coinsAmount > 0)
            {
                double ratingCoef = ListOfUsers[i].Rating / totalRating;
                double ratingCoefRounded = Math.Round(ratingCoef, 1);
                double emissionCoins = ratingCoefRounded * coinsAmount;
                int emissionCoinsRounded = (int)Math.Round(emissionCoins);

                coinsAmount -= emissionCoinsRounded;

                for (int y = 0; i < emissionCoinsRounded; y++)
                {
                    CoinModel newCoin = new(ListOfCoins.Count() + 1, ListOfUsers[i].Name);
                    ListOfCoins.Add(newCoin);
                }

                ListOfUsers[i].Amount += emissionCoinsRounded;
            } else break;
        }
        return Task.FromResult(response);
    }
    public override async Task ListUsers(None request, IServerStreamWriter<UserProfile> responseStream, ServerCallContext context)
    {
        foreach(var user in ListOfUsers)
        {
            await responseStream.WriteAsync(new UserProfile{Name = user.Name, Amount = user.Amount});
            await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
        }
    }
    public override Task<Coin> LongestHistoryCoin(None request, ServerCallContext context)
    {
        return base.LongestHistoryCoin(request, context);
    }
    public override Task<Response> MoveCoins(MoveCoinsTransaction request, ServerCallContext context)
    {
        return base.MoveCoins(request, context);
    }
}