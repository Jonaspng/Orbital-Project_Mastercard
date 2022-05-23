using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class StageManager : MonoBehaviour {

    public BattleState state;

    public GameObject WarriorPrefab;

    public GameObject ArcherPrefab;

    public GameObject MagePrefab;

    public GameObject confirmedCharacter;

    public GameObject enemyPrefab;

    public Transform playerBattleStation;

    public int enemyCount = 2;

    public Transform enemy1BattleStation;

    public Transform enemy2BattleStation;



    public static StageManager instance;

    public string playerType;
    
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
        GameObject playerGO = null;
        playerType = PlayerPrefs.GetString("character");
        if (playerType == "Warrior") {
            print("warrior selected");
            playerGO = Instantiate(WarriorPrefab, playerBattleStation);
        } else if (playerType == "Archer") {
            playerGO = Instantiate(ArcherPrefab, playerBattleStation);
        } else {
            playerGO = Instantiate(MagePrefab, playerBattleStation);
        }

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

    public void DestroyEnemy(int enemyIndex) {
        GameObject.Destroy(enemies[enemyIndex].gameObject);
        GameObject.Destroy(enemyHUDs[enemyIndex].gameObject);
        enemies[enemyIndex] = null;
        enemyHUDs[enemyIndex] = null;
        enemyCount -= 1;
        if (enemyCount == 0) {
            foreach (Transform obj in GameObject.Find("Current Hand").transform) {
                GameObject.Destroy(obj.gameObject);
            }
            deckManager.GenerateNewCards();
            this.GetComponent<PopUpMenu>().PopUp();
        }
    }
    

    public void OnEndTurnClick() {
        foreach(Enemy enemy in enemies) {
            if (enemy != null) {
                enemy.ResetBaseShield();
                enemy.ResetAttackModifier();
            }
            
        }

        for (int i = 0; i < enemyHUDs.Length; i ++) {
            if (enemyHUDs[i] != null) {
                enemyHUDs[i].RemoveShieldIcon();
            }
        }

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
            if (enemies[i] != null) {
                enemies[i].EnemyMove(player, enemies, i);
            }
        }
    }

    
}
