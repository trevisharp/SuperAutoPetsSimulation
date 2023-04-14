using System.Collections;
using System.Collections.Generic;

public abstract class Tier : IEnumerable<Pet>
{
    public abstract IEnumerator<Pet> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}