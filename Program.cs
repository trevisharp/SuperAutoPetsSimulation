using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

Shop shop = new Shop();
Tier tier = new StandarTierOne();
shop.Register(tier);
shop.Refill();
Team team = new Team();
Bot bot = new RandomBot();

do
{
    Console.WriteLine(shop);
    Console.WriteLine(team);
    Console.WriteLine();
} while (!bot.Play(shop, team));



IEnumerable<(Type, int)> getBestPieces()
{
    Dictionary<Type, int> dict = new Dictionary<Type, int>();

    foreach (var comp in iterativeBests())
    {
        foreach (var pet in comp)
        {
            var key = pet.GetType();
            if (!dict.ContainsKey(key))
                dict[key] = 0;
            dict[key]++;
        }
    }

    var bests = 
        from pair in dict
        orderby pair.Value descending
        select (pair.Key, pair.Value);
    return bests;
}

IEnumerable<Team> getCounters(Team team, IEnumerable<Team> comps)
{
    return getBest(comps, new Team[] { team }, 10);
}

IEnumerable<Team> iterativeBests()
{
    var tier = new StandarTierOne();
    var comps = Compositions.FromTier(tier, 3);
    int N = comps.Count();

    while (N > 20)
    {
        int Next = N switch
        {
            >100 => N / 2,
            >50 => 50,
            _ => N - 10
        };
        var bests = getBest(comps, comps, Next);
        comps = bests;
        N = comps.Count();
    }

    return comps;
}

IEnumerable<Team> getBest(IEnumerable<Team> comps, IEnumerable<Team> enemies, int N)
{
    List<(Team, (int, int, int))> list = new List<(Team, (int, int, int))>();

    foreach (var compA in comps)
    {
        int win = 0;
        int draw = 0;
        int lose = 0;
        foreach (var compB in enemies)
        {
            for (int i = 0; i < 5; i++)
            {
                var t1 = compA.Clone();
                var t2 = compB.Clone();
                foreach (var (a, b) in Simulator.Play(t1, t2));
                bool t1Wind = t1.Any(p => p.IsLive);
                bool t2Wind = t2.Any(p => p.IsLive);

                if (t1Wind)
                    win++;
                else if (t2Wind)
                    lose++;
                else draw++;
            }
        }
        var score = (win, draw, lose);
        list.Add((compA, score));
    }

    var bests =
        from result in list
        orderby result.Item2.Item1 * 1000 + result.Item2.Item2 descending
        select result;
    return bests.Select(b => b.Item1).Take(N);
}

void battle(Team t1, Team t2)
{
    foreach (var (a, b) in Simulator.Play(t1, t2))
    {
        WriteLine(a);
        WriteLine(b);
        WriteLine();
    }
}