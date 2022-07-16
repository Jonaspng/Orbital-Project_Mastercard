using UnityEngine;

[System.Serializable]
public abstract class Cards : MonoBehaviour {

    [SerializeField] private int damage;

    [SerializeField] private int originalDamage;
    [SerializeField] private int id;

    [SerializeField] private string description;

    [SerializeField] private int manaCost;

    [SerializeField] private int turns;

    public abstract void executeCard(Player player, Enemy[] enemies, int enemyindex);

    public abstract void OnDrop(int enemyIndex);

    public void DisableAllScripts() {
        foreach (MonoBehaviour c in this.GetComponents<MonoBehaviour>()) {
            if (!(c is CardDisplay)) {
                c.enabled = false;
            }
        }
    }

    public virtual void RefreshString() {
        // does nothing
    }

    public void InitialiseValues(int originalDamage, int damage, string description) {
        this.originalDamage = originalDamage;
        this.damage = damage;
        this.description = description;
    }

    public void InitialiseValues(string description) {
        this.description = description;
    }

    public int GetDamage() {
        return this.damage;
    }

    public int GetOriginalDamage() {
        return this.originalDamage;
    }

    public string GetDescription() {
        return this.description;
    }

    public int GetId() {
        return this.id;
    }

    public int GetManaCost() {
        return this.manaCost;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

}
