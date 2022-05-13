using UnityEngine;

public class Bash : Cards {

    public int damage;

    public Cards bashCard;

    public Bash(int damage, int turns, 
    bool isAoe, int manaCost) : base(manaCost, turns, isAoe) {
        this.damage = damage;
    }


    // Start is called before the first frame update
    void Start() {
        bashCard = new Bash(8, 2, false, 2);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(bashCard);
        }        
    }

    public override void executeCard(Player player, Enemy enemy) {
        enemy.receiveDamage(this.damage);
    }

}
