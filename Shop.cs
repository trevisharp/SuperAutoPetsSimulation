using System;
using System.Text;
using System.Collections.Generic;

public class Shop
{
    public int Gold { get; set; } = 10;

    public Pet this[int index]
        => index < ShopSize ? shop[index] : null;
    
    public int ShopSize => shop.Count;
    public int MaxShopSize => 3;
    
    private List<bool> freezeState = new List<bool>();
    private List<Pet> possibles = new List<Pet>();
    private List<Pet> shop = new List<Pet>();

    public void Register<T>()
        where T : Pet
        => this.possibles.Add(Activator.CreateInstance<T>());
    
    public void Register(Tier tier)
        => this.possibles.AddRange(tier);

    private Pet getRandomPet()
        => possibles[Random.Shared.Next(possibles.Count)];

    public void ToogleFreeze(int index)
    {
        if (index >= freezeState.Count)
            return;
        
        freezeState[index] = !freezeState[index];
    }

    public void Refill()
    {
        for (int i = 0; i < freezeState.Count; i++)
        {
            if (freezeState[i])
                continue;
            
            shop.RemoveAt(i);
            freezeState.RemoveAt(i);
            i--;
        }

        while (freezeState.Count < MaxShopSize)
        {
            shop.Add(getRandomPet());
            freezeState.Add(false);
        }
    }

    public void PayRefill()
    {
        if (this.Gold < 1)
            return;
        this.Gold--;
        this.Refill();
    }

    public Pet Buy(int index)
    {
        if (Gold < 3)
            return null;
        
        if (index >= shop.Count)
            return null;
        
        this.Gold -= 3;
        var purchased = shop[index];
        shop.Remove(purchased);
        freezeState.RemoveAt(index);

        return purchased;
    }

    public void Sell(Pet sold)
    {
        if (sold is null)
            return;
        
        Gold += sold.Level;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < this.shop.Count; i++)
        {
            var str = this.shop[i].ToString() ?? "";
            if (this.freezeState[i])
                str += "(F)";
            
            while (str.Length < 10)
                str = " " + str + " ";
            sb.Append(str);
            sb.Append("\t");
        }

        return sb.ToString();
    }
}