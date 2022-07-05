using UnityEngine;

[System.Serializable]
public abstract class Cards : MonoBehaviour {

    public int id;

    public int manaCost;

    public int turns;

    public Cards(int manaCost, int turns) {
        this.manaCost = manaCost;
        this.turns = turns;
    }

    public abstract void executeCard(Player player, Enemy[] enemies, int enemyindex);

    public abstract void OnDrop(int enemyIndex);

    public void DisableAllScripts() {
        foreach (MonoBehaviour c in this.GetComponents<MonoBehaviour>()) {
            if (!(c is CardDisplay)) {
                c.enabled = false;
            }
        }
    }

}
