using System.Linq;

public class SwapBot : Bot
{
    public override bool Play(Game game)
    {
        var team = game.Team;
        var shop = game.Shop;
        
        var teamSize = team.Count();

        for (int i = 0; i < teamSize; i++)
        {
            var pet = team[i];
            var iPower = pet.Life * pet.Attack;

            var nextPet = team[i + 1];
            var ippPower = nextPet.Life * nextPet.Attack;

            if (iPower < ippPower)
            {
                game.Team.Swap(i, i + 1);
                return false;
            }
        }
        
        if (teamSize < 5 && shop.Gold > 2 && shop.ShopSize > 0)
        {
            shop.Buy(0);
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