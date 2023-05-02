using System;

public class RandomBot : Bot
{
    public override bool Play(Game game)
    {
        var team = game.Team;
        var shop = game.Shop;
        
        var r = Random.Shared;
        var rand = r.NextSingle();

        if (rand < .5f && shop.Gold >= 3 && shop.ShopSize > 0)
        {
            team.Buy(shop, r.Next() % shop.ShopSize);
            return false;
        }

        if (rand < .75f)
        {
            team.Swap(r.Next() % 5, r.Next() % 5);
            return false;
        }

        if (rand < .85f && shop.ShopSize > 0)
        {
            shop.ToogleFreeze(r.Next() % shop.ShopSize);
            return false;
        }

        if (rand > .9f && shop.Gold < 3)
            return true;

        shop.PayRefill();
        return false;
    }
}