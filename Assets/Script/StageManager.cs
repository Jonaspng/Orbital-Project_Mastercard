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


    

    public void OnEndTurnClick() {
        foreach(Enemy enemy in enemies) {
            enemy.ResetBaseShield();
            enemy.ResetAttackModifier();
        }
        state = BattleState.ENEMYTURN;
        EndTurn();
        currentTurn++;
        deckManager.RerenderCards();
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] events = eventManager[currentTurn];
            for (int i = 0; i < events.Length; i++) {
            events[i].executeEvent(player, enemies);
            }
        }
        player.ResetBaseShield();
        player.ResetAttackModifier();
        state = BattleState.PLAYERTURN;
        manaCount = 3;

    }


    public void playerMove(Cards card, int enemyIndex) {
        if (this.manaCount - card.manaCost < 0) {

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
