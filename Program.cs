using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

BotComparer comparer = new BotComparer();

RandomBot rndBot = new RandomBot();
comparer.Add(rndBot);

HumbleBot hmbBot = new HumbleBot();
comparer.Add(hmbBot);

var result = comparer.Avaliate(10000);

Console.WriteLine(
    $"Humble: {result[hmbBot]}"
);
Console.WriteLine(
    $"Random: {result[rndBot]}"
);