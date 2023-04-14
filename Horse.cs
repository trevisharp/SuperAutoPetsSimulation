public class Horse : Pet
{
    public Horse() : base(2, 1) { }

    public override Pet Clone()
    {
        Horse horse = new Horse();
        horse.Attack = this.Attack;
        horse.Life = this.Life;
        horse.Experience = this.Experience;
        return horse;
    }

    public override void OnAllySummoned(Team self, Team other, Shop shop, Pet summoned)
        => summoned.Buff(this.Level, 0);

    public override string ToString()
        => this.Life < 1 ? "" : $"Horse {Attack}/{Life}";
}