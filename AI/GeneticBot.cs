using System.Linq;
using System.Collections.Generic;

public class GeneticBot : Bot
{
    public GeneticBot(Chromosome chromosome)
        => this.Chromosome = chromosome;

    public Chromosome Chromosome { get; private set; }

    public override bool Play(Shop shop, Team team)
    {
        return true;
    }

    public GeneticBot Fit(
        int N = 1000, int genesSize = 1024, int genesWidth = 1024, 
        float prob = 0.01f, int limit = 5000, int nonImprovingLimit = 100
    )
    {
        int gameCount = (N - 1) * (N - 1);
        var chromosomes = new Chromosome[N];
        for (int i = 0; i < N; i++)
            chromosomes[i] = new Chromosome(genesSize, genesWidth);
        
        IEnumerable<Chromosome> population = chromosomes;
        var dict = new Dictionary<Chromosome, float>();
        float betsFit = 0f;
        int count = 0;
        for (int n = 0; N < limit; n++)
        {
            IEnumerable<GeneticBot> bots = 
                from el in population
                select new GeneticBot(el);
            
            dict.Clear();
            foreach (var chromo in population)
                dict.Add(chromo, 0);
            
            int stages = 2;
            for (int s = 0; s < stages; s++)
            {
                var teamDict = new Dictionary<GeneticBot, (Shop shop, Team team)>();
                foreach (var bot in bots)
                {
                    Shop shop = new Shop();
                    Team team = new Team();
                    while (!bot.Play(shop, team));
                    teamDict.Add(bot, (shop, team));
                }

                foreach (var bot1 in bots)
                {
                    foreach (var bot2 in bots)
                    {
                        if (bot1 == bot2)
                            continue;

                        var team1 = teamDict[bot1].team;
                        var team2 = teamDict[bot2].team;
                        var res = Simulator.GetResult(team1, team2);

                        if (res == 0)
                        {
                            dict[bot1.Chromosome] += .5f;
                            dict[bot2.Chromosome] += .5f;
                        }
                        else if (res > 0)
                            dict[bot1.Chromosome]++;
                        else dict[bot2.Chromosome]++;
                    }
                }
            }
            
            foreach (var chromo in population)
                dict[chromo] /= 2 * gameCount;

            var fits =
                from el in population
                select dict[el];
            var newBestFit = fits.MaxBy(fit => fit);

            if (newBestFit < betsFit)
                count++;
            else count = 0;
            
            if (count >= nonImprovingLimit)
                break;
            
            population = population
                .Epoch(p => dict[p], prob);
        }

        var bests = 
            from el in population
            orderby dict[el] descending
            select el;
        var best = bests.FirstOrDefault();
        return new GeneticBot(best);
    }
}