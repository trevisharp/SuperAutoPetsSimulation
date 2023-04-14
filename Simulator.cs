using System.Collections.Generic;

public static class Simulator
{
    public static IEnumerable<(Team, Team)> Play(Team teamA, Team teamB)
    {
        var team = teamA.Clone();
        var enem = teamB.Clone();

        var it = team.GetEnumerator();
        var ie = enem.GetEnumerator();
        
        while (true)
        {
            it.Current.BeforeAttack(teamA, teamB, null);
            ie.Current.BeforeAttack(teamB, teamA, null);

            it.Current.AttackPet(ie.Current);
            ie.Current.AttackPet(it.Current);
            
            it.Current.AfterAttack(teamA, teamB, null);
            ie.Current.AfterAttack(teamB, teamA, null);

            if (it.Current.Life < 1)
            {
                it.Current.OnDie(teamA, teamB, null);
                if (!it.MoveNext())
                    yield break;
            }

            if (ie.Current.Life < 1)
            {
                ie.Current.OnDie(teamB, teamA, null);
                if (!ie.MoveNext())
                    yield break;
            }

            yield return (team, enem);
        }
    }
}