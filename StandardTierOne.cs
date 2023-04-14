using System.Collections.Generic;

public class StandarTierOne : Tier
{
    public override IEnumerator<Pet> GetEnumerator()
    {
        yield return new Ant();
    }
}