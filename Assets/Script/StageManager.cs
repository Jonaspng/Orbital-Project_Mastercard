using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class StageManager : MonoBehaviour {

    public BattleState state;

    public GameObject playerPrefab;

    public GameObject enemyPrefab;

    public Transform playerBattleStation;

    public Transform enemy1BattleStation;

    public Transform enemy2BattleStation;

    public static StageManager instance;
    
    public Player player;

    public Enemy[] enemies;

    public int manaCount;

    public int currentTurn;

    public BattleHUD playerHUD;

    public BattleHUD[] enemyHUDs;

    public DeckManager deckManager;
    
    //key = int; value = AbstractEvent[];
    public Dictionary<int, AbstractEvent[]> eventManager;

    public GameObject playerIconsPanel;

    public GameObject enemyIconsPanel;

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

        GameObject enemy1GO = Instantiate(enemyPrefab, enemy1BattleStation);
        Enemy enemy1 = enemy1GO.GetComponent<Enemy>();
        enemy1.enemyIndex = 0;

        GameObject enemy2GO = Instantiate(enemyPrefab, enemy2BattleStation);
        Enemy enemy2 = enemy2GO.GetComponent<Enemy>();
        enemy2.enemyIndex = 1;

        Enemy[] temp = {enemy1, enemy2};
        enemies = temp;

        playerHUD.SetHUD(player);

        enemyHUDs[0].SetHUD(enemy1);
         enemyHUDs[1].SetHUD(enemy2);

        state = BattleState.PLAYERTURN;
    }



    

    public void OnEndTurnClick() {
        foreach(Enemy enemy in enemies) {
            enemy.ResetBaseShield();
            enemy.ResetAttackModifier();
        }

        enemyHUDs[0].RemoveShieldIcon();
        enemyHUDs[1].RemoveShieldIcon();

        state = BattleState.ENEMYTURN;
        EndTurn();
        currentTurn++;
        playerHUD.RemoveShieldIcon();
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
        for (int i = 0; i< enemies.Length; i++) {
            enemies[i].EnemyMove(player, enemies, i);
        }
    }

    
}
