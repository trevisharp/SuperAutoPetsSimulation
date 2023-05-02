using System.Linq;
using System.Collections.Generic;

public class GeneticBot : Bot
{
    public GeneticBot(Chromosome chromosome)
        => this.Chromosome = chromosome;

    public Chromosome Chromosome { get; private set; }

    public override bool Play(Game game)
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
            
            // TODO

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