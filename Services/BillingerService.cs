using Grpc.Core;
using Task3Grpc;

namespace Task3Grpc.Services;

public class BillingerService : Billing.BillingBase
{
    private IUsersRepository ListOfUsers;
    private ICoinsRepository ListOfCoins;
    private readonly ILogger<BillingerService> _logger;
    public BillingerService(ILogger<BillingerService> logger, IUsersRepository listOfUsers, ICoinsRepository listOfCoins)
    {
        _logger = logger;
        ListOfCoins = listOfCoins;
        ListOfUsers = listOfUsers;
    }
    public override Task<Response> CoinsEmission(EmissionAmount request, ServerCallContext context)
    {
        var users = ListOfUsers.GetAll();
        long totalRating = users.Sum(x => x.Rating);
        long usersAmount = users.Count();
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

        foreach(UserModel user in users)
        {
            ListOfUsers.ChangeCoinsAmount(user, 1, true);
        }
        var response = new Response
        {
            Status = Response.Types.Status.Ok,
            Comment = "Successful emission." 
        };
        if (usersAmount == coinsAmount) return Task.FromResult(response);

        coinsAmount -= usersAmount;
        long emissionedCoins = 0;
        foreach(var user in users)
        {
            if (emissionedCoins < coinsAmount)
            {
                double ratingCoef = user.Rating / (double)totalRating;
                double ratingCoefRounded = Math.Round(ratingCoef, 1);
                double emissionCoins = ratingCoefRounded * coinsAmount;
                long emissionCoinsRounded = (long)Math.Round(emissionCoins);

                emissionedCoins += emissionCoinsRounded;
                ListOfUsers.ChangeCoinsAmount(user, emissionCoinsRounded, true);
            } else break;
        }
        return Task.FromResult(response);
    }
    public override async Task ListUsers(None request, IServerStreamWriter<UserProfile> responseStream, ServerCallContext context)
    {
        foreach(var user in ListOfUsers.GetAll())
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