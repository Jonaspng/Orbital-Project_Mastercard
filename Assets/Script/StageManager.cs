using System.Collections;
using UnityEngine;


public class StageManager : MonoBehaviour {

    public static StageManager instance;

    public Player player;

    public Enemy enemy;

    public int manaCount;
    
    //key = int; value = AbstractEvent[];
    public Hashtable eventManager;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        player = new Archer(100,1.0,0,1.0, 0);
        enemy = new CatSword(10,1.0,0,1);
        eventManager = new Hashtable();
    }

    public void playerMove(Cards card) {
        card.executeCard(player, enemy);
    }
    
    
}
