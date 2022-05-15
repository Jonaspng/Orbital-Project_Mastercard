using UnityEngine;

public class Bash : Cards {

    public int damage;

    public Cards bashCard;

    public Bash(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }


    // Start is called before the first frame update
    void Start() {
        bashCard = new Bash(8, 2, 2);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(bashCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(this.damage + player.baseAttack);
    }

}
