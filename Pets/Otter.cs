public class Otter : Pet
{
    public Otter() :  base(1, 2) { }

    public override Pet Clone()
    {
        Otter otter = new Otter();
        otter.Attack = this.Attack;
        otter.Life = this.Life;
        otter.Experience = this.Experience;
        return otter;
    }

    // TODO: Improve
    public override void OnBattleStart(Team self, Team other)
    {
        var pet = self.GetRandomPet(this);
        if (pet is null)
            return;
        
        pet.Buff(1, 1);
    }

    public static Otter New => new Otter();
}