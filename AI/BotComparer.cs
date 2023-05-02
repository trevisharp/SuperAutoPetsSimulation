using System.Collections.Generic;

public class BotComparer
{
    private List<Bot> bots = new List<Bot>();

    public void Add(Bot bot)
        => this.bots.Add(bot);
    
    public Dictionary<Bot, float> Avaliate(int N)
    {
        const int rounds = 2;
        int gameCount = N * (bots.Count - 1) * (bots.Count - 1);
        var dict = new Dictionary<Bot, float>();

        foreach (var bot in this.bots)
            dict.Add(bot, 0);

        for (int g = 0; g < N; g++)
        {
            var gameDict = new Dictionary<Bot, Game>();
            foreach (var bot in bots)
                gameDict.Add(bot, new Game());

            for (int s = 0; s < rounds; s++)
            {
                foreach (var bot in bots)
                {
                    Game game = gameDict[bot];
                    while (!bot.Play(game));
                }

                foreach (var bot1 in bots)
                {
                    foreach (var bot2 in bots)
                    {
                        if (bot1 == bot2)
                            continue;

                        var team1 = gameDict[bot1].Team;
                        var team2 = gameDict[bot2].Team;
                        var res = Simulator.GetResult(team1, team2);

                        if (res == 0)
                        {
                            dict[bot1] += .5f;
                            dict[bot2] += .5f;
                        }
                        else if (res > 0)
                            dict[bot1]++;
                        else dict[bot2]++;
                    }
                }

                foreach (var bot in bots)
                    gameDict[bot].StartRound();
            }
        }
        
        foreach (var bot in this.bots)
            dict[bot] /= 2 * rounds * gameCount;

        return dict;
    }
}