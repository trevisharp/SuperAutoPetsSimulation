using System.Linq;
using System.Collections.Generic;

public static class Compositions
{
    public static IEnumerable<Team> FromTier(Tier tier, int size)
    {
        List<IEnumerator<Pet>> its = new List<IEnumerator<Pet>>();
        for (int i = 0; i < size; i++)
        {
            its.Add(tier.GetEnumerator());
            its[i].MoveNext();
        }
        int index = 0;
        yield return Team.FromPets(
            its.Select(it => it.Current)
        );
        
        while (true)
        {
            while (!its[index].MoveNext())
            {
                its[index] = tier.GetEnumerator();
                index++;
                if (index == size)
                    yield break;
            }

            yield return Team.FromPets(
                its.Select(it => it.Current)
            );
        }
    }
}