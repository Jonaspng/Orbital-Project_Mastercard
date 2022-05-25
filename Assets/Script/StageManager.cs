using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class StageManager : MonoBehaviour {

    public BattleState state;

    public Transform playerBattleStation;

    public int enemyCount;

    public static StageManager instance;
    
    public Player player;

    public GameObject playerGO;

    public Enemy[] enemies;

    public int manaCount;

    public int currentTurn;

    public BattleHUD playerHUD;

    public DeckManager deckManager;
    
    //key = int; value = AbstractEvent[];
    public Dictionary<int, AbstractEvent[]> playerEventManager; // affects player
    public Dictionary<int, AbstractEvent[]> enemyEventManager; // affects enemy

    private void Awake() {
        
        instance = this;
    }

    private void Start() {
        state = BattleState.START;
        
        InitialiseBattle();
             
    }


    public void InitialiseBattle() {
        playerEventManager = new Dictionary<int, AbstractEvent[]>();
        enemyEventManager = new Dictionary<int, AbstractEvent[]>();
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

        ExecuteEventsInManager(enemyEventManager);

        state = BattleState.ENEMYTURN;
        
        EndTurn();

        manaCount = 3; // resets mana for player

        if (player is Archer) { // resets evasion count of archer
            Archer temp = (Archer) player;
            temp.evasionCount = 0;
        }

        ExecuteEventsInManager(playerEventManager);

        playerHUD.RemoveShieldIcon();
        deckManager.RerenderCards();
        player.ResetBaseShield();
        player.ResetAttackModifier();

        currentTurn++;

        state = BattleState.PLAYERTURN;
    }

    public void ExecuteEventsInManager(Dictionary<int, AbstractEvent[]> eventManager) {
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] events = eventManager[currentTurn];
            for (int i = 0; i < events.Length; i++) {
            events[i].executeEvent(player, enemies);
            }
        }

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
