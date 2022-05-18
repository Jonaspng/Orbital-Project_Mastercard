using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class StageManager : MonoBehaviour {

    public BattleState state;

    public GameObject playerPrefab;

    public GameObject enemyPrefab;

    public Transform playerBattleStation;

    public Transform enemyBattleStation;

    public static StageManager instance;
    
    public Player player;

    public Enemy[] enemies;

    public int manaCount;

    public int currentTurn;

    public BattleHUD playerHUD;

    public BattleHUD enemyHUD;

    public DeckManager deckManager;
    
    //key = int; value = AbstractEvent[];
    public Dictionary<int, AbstractEvent[]> eventManager;

    private void Awake() {
        this.manaCount = 3;
        this.currentTurn = 0;
        instance = this;
    }

    private void Start() {
        state = BattleState.START;
        eventManager = new Dictionary<int, AbstractEvent[]>();
        deckManager.Initialise();
        InitialiseBattle();
         
    }


    public void InitialiseBattle() {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        player = playerGO.GetComponent<Player>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        Enemy enemy1 = enemyGO.GetComponent<Enemy>();
        Enemy[] temp = {enemy1};
        enemies = temp;

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy1);

        state = BattleState.PLAYERTURN;
        
    }


    private void Update() {
        if (state == BattleState.ENEMYTURN) {
            EndTurn();
            state = BattleState.PLAYERTURN; 
        }
    }

    public void OnEndTurnClick() {
        state = BattleState.ENEMYTURN;

    }


    public void playerMove(Cards card, int enemyIndex) {
        if (this.manaCount - card.manaCost < 0) {
            print("No Mana");
        } else {
            card.executeCard(player, enemies, enemyIndex);
            this.manaCount -= card.manaCost;
        }
    }

    public void EndTurn() {
        foreach(Enemy enemy in enemies) {
            enemy.EnemyMove(player, enemies);
        }
        // deckManager.DrawCard(5);
    }

    
}
