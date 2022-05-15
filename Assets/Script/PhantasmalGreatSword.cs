using UnityEngine;

public class PhantasmalGreatSword : Cards {

    public Cards phantasmalCard;

    public int damage;

    public PhantasmalGreatSword(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    // Start is called before the first frame update
    void Start() {
        phantasmalCard = new PhantasmalGreatSword(20, 1, 3);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(phantasmalCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage));
    }
}
