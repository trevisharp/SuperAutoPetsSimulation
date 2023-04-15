using static System.Console;

battle(
    Team.FromPets(Cricket.New, Cricket.New, Horse.New),
    Team.FromPets(Mosquito.New, Mosquito.New, Mosquito.New)
);


void battle(Team t1, Team t2)
{
    foreach (var (a, b) in Simulator.Play(t1, t2))
    {
        WriteLine(a);
        WriteLine(b);
        WriteLine();
    }
}