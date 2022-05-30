class GainManaEvent : AbstractEvent {

    public int manaGained;

    public GainManaEvent(int numberOfTurns, int manaGained, int enemyIndex) 
    : base(numberOfTurns, enemyIndex) {
        this.manaGained = manaGained;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        StageManager.instance.manaCount += manaGained;
    }

}