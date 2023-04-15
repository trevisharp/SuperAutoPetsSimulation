public class Ant : Pet
{
    public Ant() :  base(2, 1) { }

    public override Pet Clone()
    {
        Ant ant = new Ant();
        ant.Attack = this.Attack;
        ant.Life = this.Life;
        ant.Experience = this.Experience;
        return ant;
    }

    public override void OnDie(Team self, Team other, Shop shop)
    {
        var ally = self.GetRandomPet(this);
        if (ally is null)
            return;
        ally.Buff(2 * this.Level, this.Level);
    }

    public static Ant New => new Ant();
}