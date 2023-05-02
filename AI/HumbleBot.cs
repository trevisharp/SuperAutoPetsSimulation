using System.Linq;

public class HumbleBot : Bot
{
    public override bool Play(Game game)
    {
        var team = game.Team;
        var shop = game.Shop;
        
        var teamSize = team.Count();
        
        if (teamSize < 5 && shop.Gold > 2 && shop.ShopSize > 0)
        {
            team.Buy(shop, 0);
            return false;
        }

        if (shop.Gold > 0)
        {
            shop.PayRefill();
            return false;
        }

        return true;
    }
}