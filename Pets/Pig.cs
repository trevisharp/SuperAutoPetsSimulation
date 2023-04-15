public class Pig : Pet
{
    public Pig() :  base(4, 1) { }

    public override Pet Clone()
    {
        Pig pig = new Pig();
        pig.Attack = this.Attack;
        pig.Life = this.Life;
        pig.Experience = this.Experience;
        return pig;
    }

    public static Pig New => new Pig();
}