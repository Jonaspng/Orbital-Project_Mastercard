using UnityEngine;

public class Fireball : Cards {

    public Cards fireballCard;

    public int damage;

    public Fireball(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    // Start is called before the first frame update
    void Start() {
        fireballCard = new Fireball(8, 1, 3);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(fireballCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].ReceiveFireballDamage(player.GetFullDamage(damage));
    }
}
