
class OvertimeDamageEvent : AbstractEvent {

    public int damagePerTurn;

    public OvertimeDamageEvent(int damagePerTurn, int numberOfTurns, int enemyIndex)
    : base(numberOfTurns, enemyIndex) {
        this.damagePerTurn = damagePerTurn;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        enemies[enemyIndex].receiveDamage(damagePerTurn);
    }

    public override void ExecuteEnemyEvent(Player player, Enemy enemy) {
        //do nothing
    }


}