using static System.Console;

var tier = new StandarTierOne();
foreach (var comp in Compositions.FromTier(tier, 3))
{
    WriteLine(comp);
}