using System.Collections.Generic;

public class Game
{
    public Game()
    {
        this.Team = new Team();
        this.Shop = new Shop();

        this.Shop.Register(new StandarTierOne());
        this.StartRound();
    }

    public Team Team { get; set; }
    public Shop Shop { get; set; }
    public int Round { get; private set; }

    public void StartRound()
    {
        this.Round++;
        this.Shop.Gold = 10;
        this.Shop.Refill();
    }

    public IEnumerable<(Team, Team)> Play(Team enemy)
        => Simulator.Play(this.Team, enemy);
}