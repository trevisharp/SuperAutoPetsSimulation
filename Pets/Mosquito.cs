public class Mosquito : Pet
{
    public Mosquito() :  base(2, 2) { }

    public override Pet Clone()
    {
        Mosquito mosquito = new Mosquito();
        mosquito.Attack = this.Attack;
        mosquito.Life = this.Life;
        mosquito.Experience = this.Experience;
        return mosquito;
    }

    public override void OnBattleStart(Team self, Team other)
    {
        var pet = other.GetRandomPet();
        pet.ReciveDamage(1);
    }

    public static Mosquito New => new Mosquito();
}