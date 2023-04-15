public abstract class Pet
{
    public Pet(int attack, int life)
    {
        this.Attack = attack;
        this.Life = life;
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
    public bool IsLive => Life > 0;

    public abstract Pet Clone();

    public void AttackPet(Pet enemy)
    {
        enemy.ReciveDamage(this.Attack);
    }

    public void ReciveDamage(int damage)
    {
        this.Life -= damage;
    }

    public void Buff(int attack, int life)
    {
        this.Attack += attack;
        this.Life += life;
    }

    public override string ToString()
        => IsLive ? $"{GetType().Name} {Attack}/{Life}" : "";

    public virtual void AfterAttack(Team self, Team other, Shop shop) { }
    public virtual void BeforeAttack(Team self, Team other, Shop shop) { }
    public virtual void OnDie(Team self, Team other, Shop shop) { }
    public virtual void OnAllySummoned(Team self, Team other, Shop shop, Pet summoned) { }
    public virtual void OnBattleStart(Team self, Team other) { }
}