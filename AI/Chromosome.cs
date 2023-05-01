using System;
using System.Collections.Generic;

public class Chromosome
{
    private int dataWidth;
    private int[] genes;

    public int this[int index]
        => this.genes[index];

    private Chromosome(Chromosome parentA, Chromosome parentB, int crossoverPoint)
    {
        if (parentA.dataWidth != parentB.dataWidth)
            throw new InvalidOperationException();
            
        if (parentA.genes.Length != parentB.genes.Length)
            throw new InvalidOperationException();

        int[] genes = new int[parentA.genes.Length];

        for (int i = 0; i < crossoverPoint; i++)
            genes[i] = parentA[i];
        
        for (int i = crossoverPoint; i < genes.Length; i++)
            genes[i] = parentB[i];

        this.genes = genes;
        this.dataWidth = parentA.dataWidth;
    }
    
    public Chromosome(int chromosomeSize, int dataWidth)
    {
        this.dataWidth = dataWidth;
        this.genes = new int[chromosomeSize];
        for (int i = 0; i < this.genes.Length; i++)
            this.genes[i] = Random.Shared.Next() % dataWidth;
    }

    public Chromosome Crossover(Chromosome parent)
    {
        if (parent.genes.Length != this.genes.Length)
            throw new InvalidOperationException();
        int crossoverPoint = Random.Shared.Next() % this.genes.Length;
        
        return new Chromosome(this, parent, crossoverPoint);
    }

    public void Mutate()
    {
        int mutationIndex = Random.Shared.Next() % this.genes.Length;
        int randomAdd = Random.Shared.Next() % (dataWidth / 2);
        this.genes[mutationIndex] += randomAdd - dataWidth / 4;
    }

    public void TryMutate(float prob = 0.01f)
    {
        if (Random.Shared.NextSingle() > prob)
            return;
        
        Mutate();
    }
}