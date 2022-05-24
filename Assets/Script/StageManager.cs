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

    public int enemyCount;

    public Transform enemy1BattleStation;

    public Transform enemy2BattleStation;

    public Transform enemy3BattleStation;

    public Transform enemy4BattleStation;

    public static StageManager instance;

    public string playerType;
    
    public Player player;

    public GameObject playerGO;

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
        
        instance = this;
    }

    private void Start() {
        
        state = BattleState.START;
        eventManager = new Dictionary<int, AbstractEvent[]>();
        InitialiseBattle();
             
    }


    public void InitialiseBattle() {
        this.manaCount = 3;
        this.currentTurn = 0;
        GameObject.Find("GameManager").GetComponent<GameManager>().InitialiseStage();
        deckManager.Initialise();
        state = BattleState.PLAYERTURN;
    }

    public void DestroyEnemy(int enemyIndex) {
        GameObject.Destroy(enemies[enemyIndex].transform.parent.parent.gameObject);
        enemies[enemyIndex] = null;
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
                enemy.GetComponentInParent<BattleHUD>().RemoveShieldIcon();
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
