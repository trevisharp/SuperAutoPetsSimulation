public class Cricket : Pet
{
    private string name = "Cricket";
    private bool minion = false;
    public Cricket() : base(1, 2) { }

    public override Pet Clone()
    {
        Cricket cricket = new Cricket();
        cricket.Attack = this.Attack;
        cricket.Life = this.Life;
        cricket.Experience = this.Experience;
        return cricket;
    }

    public override void OnDie(Team self, Team other, Shop shop)
    {
        if (this.minion)
            return;
        
        this.minion = true;
        this.name = "~Cricket";
        this.Attack = this.Level;
        this.Life = this.Level;
        foreach (var pet in self)
        {
            if (pet is null)
                continue;
            
            pet.OnAllySummoned(self, other, shop, this);
        }
    }

    public override string ToString()
        => this.Life < 1 ? "" : $"{name} {Attack}/{Life}";
}