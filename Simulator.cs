using System.Linq;
using System.Collections.Generic;

public static class Simulator
{
    public static IEnumerable<(Team, Team)> Play(Team teamA, Team teamB)
    {
        yield return (teamA, teamB);

        var pets = 
            from pet in teamA.Concat(teamB)
            orderby pet.Attack descending
            select pet;

        foreach (var pet in pets)
        {
            if (teamA.Contains(pet))
                pet.OnBattleStart(teamA, teamB);
            else pet.OnBattleStart(teamB, teamA);
        }

        foreach (var pet in pets)
        {
            if (pet.Life < 1)
            {
                var died = pet;
                if (teamA.Contains(died))
                    died.OnDie(teamA, teamB, null);
                else died.OnDie(teamB, teamA, null);
            }
        }

        var it = teamA.GetEnumerator();
        var ie = teamB.GetEnumerator();
        it.MoveNext();
        ie.MoveNext();
        yield return (teamA, teamB);
        
        while (true)
        {
            if (it.Current.Life < 1)
            {
                if (!it.MoveNext())
                {
                    yield return (teamA, teamB);
                    yield break;
                }
            }
            
            if (ie.Current.Life < 1)
            {
                if (!ie.MoveNext())
                {
                    yield return (teamA, teamB);
                    yield break;
                }
            }

            it.Current.BeforeAttack(teamA, teamB, null);
            ie.Current.BeforeAttack(teamB, (Team)teamA, null);

            it.Current.AttackPet(ie.Current);
            ie.Current.AttackPet(it.Current);
            
            it.Current.AfterAttack(teamA, teamB, null);
            ie.Current.AfterAttack(teamB, (Team)teamA, null);

            if (it.Current.Life < 1)
            {
                var died = it.Current;
                died.OnDie(teamA, teamB, null);
            }

            if (ie.Current.Life < 1)
            {
                var died = ie.Current;
                died.OnDie(teamB, (Team)teamA, null);
            }

            yield return (teamA, teamB);
        }
    }
    
    public static int GetResult(Team teamA, Team teamB)
    {
        var last = Play(teamA, teamB).LastOrDefault();

        bool teamADies = last.Item1.All(p => p is null || p.Life < 1);
        bool teamBDies = last.Item2.All(p => p is null || p.Life < 1);
        
        if (teamADies && teamBDies)
            return 0;
        
        if (teamBDies)
            return 1;
        
        return -1;
    }
}