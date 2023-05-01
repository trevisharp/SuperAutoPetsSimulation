using System;
using System.Linq;
using System.Collections.Generic;

public static class ChromosomeExtension
{
    public static IEnumerable<Chromosome> Selection(
        this IEnumerable<Chromosome> population,
        Func<Chromosome, float> fitness
    )
    {
        int size = population.Count();
        float sum = population.Sum(p => fitness(p));

        while (size > 0)
        {
            float randFitnessRoulette = sum * Random.Shared.NextSingle();

            foreach (var el in population)
            {
                randFitnessRoulette -= fitness(el);
                if (randFitnessRoulette > 0)
                    continue;
                
                yield return el;
                size--;
                break;
            }
        }
    }

    public static IEnumerable<Chromosome> Crossover(
        this IEnumerable<Chromosome> population
    )
    {
        var it = population.GetEnumerator();

        while (it.MoveNext())
        {
            var elA = it.Current;

            if (!it.MoveNext())
                yield break;
            var elB = it.Current;

            yield return elA.Crossover(elB);
            yield return elB.Crossover(elA);
        }
    }

    public static IEnumerable<Chromosome> Mutate(
        this IEnumerable<Chromosome> population,
        float prob = 0.01f
    )
    {
        foreach (var el in population)
        {
            el.TryMutate(prob);
            yield return el;
        }
    }

    public static IEnumerable<Chromosome> Epoch(
        this IEnumerable<Chromosome> population,
        Func<Chromosome, float> fitness,
        float prob = 0.01f
    ) => population
        .Selection(fitness)
        .Crossover()
        .Mutate(prob);
}