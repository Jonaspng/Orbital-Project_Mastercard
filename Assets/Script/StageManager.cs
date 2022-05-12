using UnityEngine;

public class StageManager : MonoBehaviour {

    public static StageManager instance;

    public Player player;

    public Enemy enemy;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        player = new Warrior(100,1,0,1);
        enemy = new CatSword(10,1,0,1);
        
    }

    public void playerAttack() {
        player.attack(enemy);
        print(enemy.getHealth()); // should be -70
    }
    
}
