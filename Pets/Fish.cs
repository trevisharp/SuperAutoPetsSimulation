public class Fish : Pet
{
    public Fish() :  base(2, 2) { }

    public override Pet Clone()
    {
        Fish fish = new Fish();
        fish.Attack = this.Attack;
        fish.Life = this.Life;
        fish.Experience = this.Experience;
        return fish;
    }

    public static Fish New => new Fish();
}