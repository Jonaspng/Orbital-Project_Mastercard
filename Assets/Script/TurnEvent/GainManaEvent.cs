using UnityEngine;
class GainManaEvent : AbstractEvent {

    [SerializeField] private int manaGained;

    public GainManaEvent(int manaGained, int enemyIndex) 
    : base(enemyIndex) {
        this.manaGained = manaGained;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        StageManager.GetInstance().AddManaCount(manaGained);
    }

}