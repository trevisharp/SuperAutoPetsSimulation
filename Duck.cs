public class Duck : Pet
{
    public Duck() :  base(2, 3) { }

    public override Pet Clone()
    {
        Duck duck = new Duck();
        duck.Attack = this.Attack;
        duck.Life = this.Life;
        duck.Experience = this.Experience;
        return duck;
    }

    public override string ToString()
        => this.Life < 1 ? "" : $"Duck {Attack}/{Life}";

    public static Duck New => new Duck();
}