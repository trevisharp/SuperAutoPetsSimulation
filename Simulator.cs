using System.Collections.Generic;

public static class Simulator
{
    public static IEnumerable<(Team, Team)> Play(Team teamA, Team teamB)
    {
        var team = teamA.Clone();
        var enem = teamB.Clone();

        var it = team.GetEnumerator();
        var ie = enem.GetEnumerator();

        yield return (team, enem);
        it.MoveNext();
        ie.MoveNext();
        
        while (true)
        {
            if (it.Current.Life < 1)
            {
                if (!it.MoveNext())
                {
                    yield return (team, enem);
                    yield break;
                }
            }
            
            if (ie.Current.Life < 1)
            {
                if (!ie.MoveNext())
                {
                    yield return (team, enem);
                    yield break;
                }
            }

            it.Current.BeforeAttack(team, enem, null);
            ie.Current.BeforeAttack(enem, team, null);

            it.Current.AttackPet(ie.Current);
            ie.Current.AttackPet(it.Current);
            
            it.Current.AfterAttack(team, enem, null);
            ie.Current.AfterAttack(enem, team, null);

            if (it.Current.Life < 1)
            {
                var died = it.Current;
                died.OnDie(team, enem, null);
            }

            if (ie.Current.Life < 1)
            {
                var died = ie.Current;
                died.OnDie(enem, team, null);
            }

            yield return (team, enem);
        }
    }
}