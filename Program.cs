using static System.Console;

var tier = new StandarTierOne();
var comps = Compositions.FromTier(tier, 3);
var it = comps.GetEnumerator();

it.MoveNext();
var fst = it.Current;

it.MoveNext();
var scn = it.Current;

foreach (var (a, b) in Simulator.Play(fst, scn))
{
    WriteLine(a);
    WriteLine(b);
    WriteLine();
}