class StealthEvent : AbstractEvent {

    public bool isStealthed;
    public StealthEvent(int numberOfTurns, bool isStealthed, int enemyIndex) 
    : base(numberOfTurns, enemyIndex) {
        this.isStealthed = isStealthed;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        Archer archer = (Archer) player;
        archer.ChangeStealthStatus(isStealthed);
    }
}