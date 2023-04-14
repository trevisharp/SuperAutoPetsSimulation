using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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

    public Pet GetRandomPet()
    {
        int size = 0;
        for (int i = 0; i < 5; i++)
        {
            if (team is null)
                continue;
            
            size++;
        }

        if (size == 0)
            return null;

        int index = Random.Shared.Next(5);
        return this.team[index];

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

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < 5; i++)
        {
            var str = this.team[i]?.ToString() ?? "";
            while (str.Length < 10)
                str = " " + str + " ";
            sb.Append(str);
            sb.Append("\t");
        }

        return sb.ToString();
    }

    public static Team FromPets(params Pet[] pets)
    {
        Team team = new Team();
        for (int i = 0; i < pets.Length && i < 5; i++)
            team.team[i] = pets[i];
        return team;
    }

    public static Team FromPets(IEnumerable<Pet> pets)
    {
        var it = pets.GetEnumerator();
        int index = 0;

        Team team = new Team();
        while (it.MoveNext() && index < 5)
        {
            team.team[index] = it.Current;
            index++;
        }
        return team;
    }
}