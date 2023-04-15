using System.Collections.Generic;

public class StandarTierOne : Tier
{
    public override IEnumerator<Pet> GetEnumerator()
    {
        yield return new Ant();
        yield return new Cricket();
        yield return new Horse();
        yield return new Duck();
        yield return new Beaver();
        yield return new Fish();
        yield return new Mosquito();
        yield return new Pig();
        yield return new Otter();
    }
}