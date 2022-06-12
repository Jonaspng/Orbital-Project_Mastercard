using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class StageManager : MonoBehaviour {

    public BattleState state;

    public Transform playerBattleStation;

    public int enemyCount;

    public static StageManager instance;
    
    public Player player;

    public GameObject playerGO;

    public Enemy[] enemies;

    public GameObject gameOverMenu;

    public int manaCount;

    public int currentTurn;

    public BattleHUD playerHUD;

    public DeckManager deckManager;

    public GameObject endTurnButton;
    
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
        playerHUD.RemoveAllIcons();
        playerEventManager = new Dictionary<int, AbstractEvent[]>();
        enemyEventManager = new Dictionary<int, AbstractEvent[]>();
        this.manaCount = 3;
        RerenderManaCount(3);
        this.currentTurn = 0;
        deckManager.Initialise();
        GameObject.Find("GameManager").GetComponent<GameManager>().InitialiseStage();
        StageEventExecute();
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

            playerHUD.RemoveAllIcons();
            player.ResetBaseShield();
            player.ResetAttackModifier();
            
            deckManager.GenerateNewCards();
            if (PlayerPrefs.GetInt("stage") != 6) {
                    this.GetComponent<PopUpMenu>().PopUp();
                if (player is Warrior) {
                    Warrior temp = (Warrior) player;
                    temp.ChangeIsStrongWillpower(false);
                    temp.resetHitCount();
                } else if (player is Archer) {
                    Archer temp = (Archer) player;
                    temp.ChangeStickyArrowStatus(false);
                }

                PlayerPrefs.SetInt("health", player.health);
            } else {
                SceneManager.LoadScene("Start Menu");
            }
            
        }
    }
    

    public void OnEndTurnClick() {
        StartCoroutine(OnEndTurnCoroutine());
    }

    public IEnumerator OnEndTurnCoroutine() {

        foreach(Enemy enemy in enemies) {
            if (enemy != null) {
                enemy.ResetBaseShield();
                enemy.GetComponentInParent<BattleHUD>().RemoveShieldIcon();
            }
        }        

        yield return StartCoroutine(ExecuteEventsInManager(enemyEventManager));

        state = BattleState.ENEMYTURN;
        
        yield return StartCoroutine(EndTurn());

        state = BattleState.PLAYERTURN;
    }

    public IEnumerator ExecuteEventsInManager(Dictionary<int, AbstractEvent[]> eventManager) {
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] events = eventManager[currentTurn];
            print("Dictionary event length is " + events.Length);
            for (int i = 0; i < events.Length; i++) {
                events[i].executeEvent(player, enemies);
                yield return new WaitForSeconds(0.4f);                    
            }
        }

    }


    public void playerMove(Cards card, int enemyIndex) {
        if (this.manaCount - card.manaCost < 0) {

        } else {
            card.executeCard(player, enemies, enemyIndex);
            this.manaCount -= card.manaCost;
            RerenderManaCount(this.manaCount);
        }
    }

    public void RerenderManaCount(int manaCount) {
        string strManaCount = manaCount.ToString();
        GameObject.Find("Mana Counter").GetComponent<TextMeshProUGUI>().text = strManaCount;
    }


    public IEnumerator EndTurn() {

        if (enemyCount > 0) {

            deckManager.ClearCards();

            endTurnButton.SetActive(false);
            for (int i = 0; i< enemies.Length; i++) {
                if (enemies[i] != null) {
                    yield return new WaitForSeconds(1f);
                    enemies[i].EnemyMove(player, enemies, i);
                }
            }

            yield return new WaitForSeconds(0.5f);        

            manaCount = 3; // resets mana for player
            
            if (player is Archer) { // resets evasion count of archer
                Archer temp = (Archer) player;
                temp.evasionCount = 0;
            }

            playerHUD.RemoveShieldIcon();
            player.ResetBaseShield();
            player.ResetAttackModifier();

            StartCoroutine(ExecuteEventsInManager(playerEventManager));

            RerenderManaCount(this.manaCount);


            currentTurn++;

            foreach(Enemy enemy in enemies) {
                if (enemy != null) {
                    enemy.ResetAttackModifier();
                    // just in case go 2 attack up icon
                    for (int i = 0; i < 2; i++) {
                        enemy.gameObject.GetComponentInParent<BattleHUD>().RemoveAttackUpIcon();
                        yield return new WaitForSeconds(0.001f);
                    }
                }
            }

            if (player.getHealth() <= 0) {
                GameObject.Destroy(player.gameObject);
                foreach (Transform obj in GameObject.Find("Enemy Panel").transform) {
                    GameObject.Destroy(obj.gameObject);
                }
                gameOverMenu.SetActive(true);
            }

            endTurnButton.SetActive(true);
            
            if (enemyCount > 0 && player.getHealth() > 0) {
                deckManager.DrawCard(5);
            }
        }        
    }


    public void StageEventExecute() {
        int eventNumber = 0;
        if (PlayerPrefs.HasKey("random event")) {
            eventNumber = PlayerPrefs.GetInt("random event");
        }
        // Event 1: lock cat
        if (eventNumber == 1) {
            deckManager.LockCard();
        }
        // Event 2: Heal Cat
        else if (eventNumber == 2) {
            if (player.maxHp - player.health <= 20) {
                player.health = player.maxHp;
            } else {
                player.health += 20;
            }
            PlayerPrefs.SetInt("health", player.health);
            playerHUD.SetHP(player.health);
        }
        // Event 3: Poison Cat
        else if (eventNumber == 3) {
            player.ChangeIsPoisoned(true);
            playerHUD.RenderEnemyPoisonIcon();

            AbstractEvent[] newEvent = {new PlayerPoisonDamageEvent(3, 2, 0)};
            AbstractEvent[] resetEvent = {new PlayerPoisonEvent(1, false, 0)};

            if (playerEventManager.ContainsKey(currentTurn)) {
                AbstractEvent[] currEvent = (AbstractEvent[])playerEventManager[currentTurn];
                playerEventManager[currentTurn] = currEvent.Concat(newEvent).ToArray();
            } else {
                playerEventManager.Add(currentTurn, newEvent);
            }

            if (playerEventManager.ContainsKey(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[])playerEventManager[currentTurn + 1];
                playerEventManager[currentTurn + 1] = currEvent.Concat(newEvent).ToArray();
            } else {
                playerEventManager.Add(currentTurn + 1, newEvent);
            }

            if (playerEventManager.ContainsKey(currentTurn + 2)) {
                AbstractEvent[] currEvent = (AbstractEvent[])playerEventManager[currentTurn + 2];
                playerEventManager[currentTurn + 2] = currEvent.Concat(resetEvent).ToArray();
            } else {
                playerEventManager.Add(currentTurn + 2, resetEvent);
            }        
        }
        // Event 4: Recess Cat
        else if (eventNumber == 4) {
            int randomEnemy = Random.Range(0, enemies.Count());
            enemies[randomEnemy].ChangeIsImmobilised(true);
            enemies[randomEnemy].GetComponentInParent<BattleHUD>().RenderStunIcon();
            
            AbstractEvent[] newResetEvent = {new StunEvent(1, false, randomEnemy)};
            if (enemyEventManager.ContainsKey(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[])enemyEventManager[currentTurn + 1];
                enemyEventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
            } else {
                enemyEventManager.Add(currentTurn + 1, newResetEvent);
            }
        }   
        PlayerPrefs.SetInt("random event", 0); //reseted
    }




    
}
