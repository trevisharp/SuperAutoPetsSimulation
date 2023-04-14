using System.Collections.Generic;

public class Game
{
    public Game()
    {
        this.Team = new Team();
        this.Shop = new Shop();
        this.Shop.Refill();
    }

    public Team Team { get; set; }
    public Shop Shop { get; set; }

    public IEnumerable<(Team, Team)> Play(Team enemy)
        => Simulator.Play(this.Team, enemy);
}