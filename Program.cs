using static System.Console;

BotComparer comparer = new BotComparer();

RandomBot rndBot = new RandomBot();
comparer.Add(rndBot);

HumbleBot hmbBot = new HumbleBot();
comparer.Add(hmbBot);

SwapBot swpBot = new SwapBot();
comparer.Add(swpBot);

var result = comparer.Avaliate(10000);

WriteLine(
    $"Humble: {result[hmbBot]}"
);
WriteLine(
    $"Random: {result[rndBot]}"
);
WriteLine(
    $"Swap: {result[swpBot]}"
);