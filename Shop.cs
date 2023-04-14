using System;
using System.Collections.Generic;

public class Shop
{
    public int Gold { get; private set; } = 10;

    public Pet this[int index]
        => index < ShopSize ? shop[index] : null;
    
    public int ShopSize => shop.Count;
    
    private List<Pet> possibles = new List<Pet>();
    private List<Pet> shop = new List<Pet>();

    public void Register<T>()
        where T : Pet
        => this.possibles.Add(Activator.CreateInstance<T>());
    
    private Pet getRandomPet()
        => possibles[Random.Shared.Next(possibles.Count)];

    public void Refill()
    {
        shop.Clear();
        possibles.Add(getRandomPet());
        possibles.Add(getRandomPet());
        possibles.Add(getRandomPet());
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

        return purchased;
    }

    public void Sell(Pet sold)
    {
        if (sold is null)
            return;
        
        Gold++;
    }
}