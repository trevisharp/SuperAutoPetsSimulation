public class Beaver : Pet
{
    public Beaver() :  base(3, 2) { }

    public override Pet Clone()
    {
        Beaver beaver = new Beaver();
        beaver.Attack = this.Attack;
        beaver.Life = this.Life;
        beaver.Experience = this.Experience;
        return beaver;
    }

    public override string ToString()
        => this.Life < 1 ? "" : $"Beaver {Attack}/{Life}";

    public static Beaver New => new Beaver();
}