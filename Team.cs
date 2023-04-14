using System.Collections;
using System.Collections.Generic;

public class Team : IEnumerable<Pet>
{
    private Pet[] team = new Pet[5];

    public void Buy(Shop shop, int index)
    {
        var purchased = shop.Buy(index);
        if (purchased is null)
            return;
        
        for (int i = 0; i < 5; i++)
        {
            if (this.team is not null)
                continue;
            
            this.team[i] = purchased;
            return;
        }
    }

    public void Sell(Shop shop, int index)
    {
        if (this.team[index] is null)
            return;
        
        var sold = this.team[index];
        this.team[index] = null;
        shop.Sell(sold);
    }

    public Team Clone()
    {
        Team clone = new Team();
        for (int i = 0; i < 5; i++)
        {
            if (this.team[i] is null)
                continue;
            
            clone.team[i] = this.team[i].Clone();
        }
        return clone;
    }

    public IEnumerator<Pet> GetEnumerator()
    {
        for (int i = 0; i < 5; i++)
        {
            if (this.team[i] is null)
                continue;
            
            yield return this.team[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}