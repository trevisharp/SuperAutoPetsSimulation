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

    public static Duck New => new Duck();
}