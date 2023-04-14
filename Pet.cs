public abstract class Pet
{
    public Pet(int life, int attack)
    {
        this.Life = life;
        this.Attack = attack;
    }

    public int Life { get; protected set; }
    public int Attack { get; protected set; }
    public int Experience { get; protected set; } = 1;
    public int Level => Experience switch
    {
        <3 => 1,
        <6 => 2,
        _  => 3
    };

    public abstract Pet Clone();

    public void AttackPet(Pet enemy)
    {
        enemy.ReciveDamage(this.Attack);
    }

    public void ReciveDamage(int damage)
    {
        this.Life -= damage;
    }

    public virtual void AfterAttack(Team self, Team other, Shop shop) { }
    public virtual void BeforeAttack(Team self, Team other, Shop shop) { }
    public virtual void OnDie(Team self, Team other, Shop shop) { }
}