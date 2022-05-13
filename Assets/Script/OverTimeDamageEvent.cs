
class OvertimeDamageEvent : AbstractEvent {

    public int damagePerTurn;

    public OvertimeDamageEvent(int damagePerTurn, int numberOfTurns)
    : base(numberOfTurns) {
        this.damagePerTurn = damagePerTurn;
    }

    public override void executeEvent(Player player, Enemy enemy) {

    }



}