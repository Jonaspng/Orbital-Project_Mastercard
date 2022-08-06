using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class StageManager : MonoBehaviour {

    [SerializeField] private BattleState state;

    [SerializeField] private Transform playerBattleStation;

    [SerializeField] private int enemyCount;

    [SerializeField] private static StageManager instance;
    
    [SerializeField] private Player player;

    [SerializeField] private GameObject playerGO;

    [SerializeField] private Enemy[] enemies;

    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] private int manaCount;

    [SerializeField] private int currentTurn;

    [SerializeField] private BattleHUD playerHUD;

    [SerializeField] private DeckManager deckManager;

    [SerializeField] private GameObject endTurnButton;

    [SerializeField] private TurnNotification turnNotification;

    [SerializeField] private GameObject notificationMenu;

    [SerializeField] private GameObject lockedCardPanel;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject backgroundCanvas;

    [SerializeField] private Dictionary<int, AbstractEvent[]> playerEventManager; // affects player
    [SerializeField] private Dictionary<int, AbstractEvent[]> enemyEventManager; // affects enemy


    private void Awake() {
        instance = this;
        AudioListener.volume = PlayerPrefs.GetFloat("volumeValue");
    }

    public static StageManager GetInstance() {
        return instance;
    }

    public int GetCurrentTurn() {
        return this.currentTurn;
    }

    public Dictionary<int, AbstractEvent[]> GetPlayerEventManager() {
        return this.playerEventManager;
    }

    public Dictionary<int, AbstractEvent[]> GetEnemyEventManager() {
        return this.enemyEventManager;
    }

    public BattleHUD GetPlayerHUD() {
        return this.playerHUD;
    }

    public int GetEnemyCount() {
        return this.enemyCount;
    }

    public void AddManaCount(int mana) {
        this.manaCount += mana;
    }

    public Player GetPlayer() {
        return this.player;
    }

    public Enemy[] GetEnemies() {
        return this.enemies;
    }

    public int GetManaCount() {
        return this.manaCount;
    }

    public void SetPlayerGO(GameObject playerGO) {
        this.playerGO = playerGO;
    }
    
    public GameObject GetPlayerGO() {
        return this.playerGO;
    }

    public Transform GetPlayerBattleStation() {
        return this.playerBattleStation;
    }

    public void SetPlayer(Player player) {
        this.player = player;
    }

    public void SetPlayerHUD(BattleHUD battlehud) {
        this.playerHUD = battlehud;
    }

    public void SetEnemies(Enemy[] enemies) {
        this.enemies = enemies;
    }

    public void SetEnemyCount(int i ) {
        this.enemyCount = i;
    }

    public DeckManager GetDeckManager() {
        return this.deckManager;
    }


    private void Update() {
        RectTransform rt = canvas.GetComponent<RectTransform>();
        float height = backgroundCanvas.GetComponent<RectTransform>().rect.height;
        float width = backgroundCanvas.GetComponent<RectTransform>().rect.width;
        Vector3 scale = backgroundCanvas.GetComponent<RectTransform>().localScale;
        rt.sizeDelta = new Vector2(width, height);
        rt.localScale = scale;
    }

    private void Start() {
        state = BattleState.START;
        InitialiseBattle();
    }


    public void InitialiseBattle() {
        playerEventManager = new Dictionary<int, AbstractEvent[]>();
        enemyEventManager = new Dictionary<int, AbstractEvent[]>();
        GameObject.Find("Stage Number").GetComponent<TextMeshProUGUI>().text = "Stage " + PlayerPrefs.GetInt("stage");
        this.manaCount = 3;
        RerenderManaCount(3);
        this.currentTurn = 0;
        deckManager.Initialise();
        GameObject.Find("GameManager").GetComponent<GameManager>().InitialiseStage();
        playerHUD.RemoveAllIcons();
        StageEventExecute();
        deckManager.DrawCard(5);
        foreach (Enemy enemy in enemies) {
            enemy.EnemyMoveNumberGenerator();
            enemy.RenderWarningIndicator();
        }
        state = BattleState.PLAYERTURN;
    }

    public IEnumerator DestroyEnemy(int enemyIndex) {
        enemies[enemyIndex] = null;
        enemyCount -= 1;
        if (enemyCount == 0) {
            foreach (Transform obj in GameObject.Find("Current Hand").transform) {
                GameObject.Destroy(obj.gameObject);
            }

            playerHUD.RemoveAllIcons();
            player.SetBaseShield(0);
            player.ResetAttackModifier();
            player.ChangeIsBroken(false);
            
            deckManager.GenerateNewCards();

            if (PlayerPrefs.GetInt("stage") != 6) {
                yield return new WaitForSeconds(0.8f);
                this.GetComponent<PopUpMenu>().PopUp();
                if (player is Warrior) {
                    Warrior temp = (Warrior) player;
                    temp.ChangeIsStrongWillpower(false);
                    temp.resetHitCount();
                } else if (player is Archer) {
                    Archer temp = (Archer) player;
                    temp.ChangeStickyArrowStatus(false);
                }
            } else {
                SceneManager.LoadScene("End Cutscene1");
            }
            
        }
    }
    

    public void OnEndTurnClick() {
        endTurnButton.SetActive(false);
        StartCoroutine(OnEndTurnCoroutine());
    }

    public IEnumerator OnEndTurnCoroutine() {

        foreach(Enemy enemy in enemies) {
            if (enemy != null) {
                enemy.SetBaseShield(0);
                enemy.GetComponentInParent<BattleHUD>().RemoveShieldIcon();
            }
        }        

        yield return StartCoroutine(ExecuteEventsInManager(enemyEventManager));

        state = BattleState.ENEMYTURN;
        
        yield return StartCoroutine(EndTurn());

        foreach (Enemy enemy in enemies) {
            if (enemy != null) {
                enemy.EnemyMoveNumberGenerator();
                enemy.GetComponentInParent<BattleHUD>().RemoveIndicator();
                enemy.RenderWarningIndicator();
            }
            
        }

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
        if (this.manaCount - card.GetManaCost() < 0) {

        } else {
            card.executeCard(player, enemies, enemyIndex);
            this.manaCount -= card.GetManaCost();
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

            //enemy turn
            turnNotification.ChangeText("Enemy Turn");
            turnNotification.GetBackgroundAnimator().SetTrigger("ChangeTurn");
            turnNotification.GetTextAnimator().SetTrigger("ChangeTurn");

            yield return new WaitForSeconds(2f);
            
            for (int i = 0; i< enemies.Length; i++) {
                if (enemies[i] != null) {
                    enemies[i].EnemyMove(player, enemies, i);
                    yield return new WaitForSeconds(1f);
                    if (player.GetHealth() <= 0) {
                        StartCoroutine(OnPlayerDeath());
                        yield break;
                    }
                }
            }

            yield return new WaitForSeconds(0.5f);        

            manaCount = 3; // resets mana for player
            
            if (player is Archer) { // resets evasion count of archer
                Archer temp = (Archer) player;
                temp.SetEvasionCount(0);
                playerHUD.RemoveDodgeIcon();
            }

            playerHUD.RemoveShieldIcon();
            player.SetBaseShield(0);
            player.ResetAttackModifier();
            playerHUD.RemoveEndureIcon();
            for (int i = 0; i < 5; i++) {
                playerHUD.gameObject.GetComponent<BattleHUD>().RemoveAttackUpIcon();
                yield return new WaitForSeconds(0.001f);
            }

            StartCoroutine(ExecuteEventsInManager(playerEventManager));

            RerenderManaCount(this.manaCount);


            currentTurn++;

            foreach(Enemy enemy in enemies) {
                if (enemy != null) {
                    enemy.SetAttackModifier(1);
                    // just in case go 2 attack up icon
                    for (int i = 0; i < 2; i++) {
                        enemy.gameObject.GetComponentInParent<BattleHUD>().RemoveAttackUpIcon();
                        yield return new WaitForSeconds(0.001f);
                    }
                }
            }

            // player turn
            turnNotification.ChangeText("Player Turn");
            turnNotification.GetBackgroundAnimator().SetTrigger("ChangeTurn");
            turnNotification.GetTextAnimator().SetTrigger("ChangeTurn");

            yield return new WaitForSeconds(2f);

            endTurnButton.SetActive(true);
            
            if (enemyCount > 0 && player.GetHealth() > 0) {
                deckManager.DrawCard(5);
            }
        }        
    }

    public IEnumerator OnPlayerDeath() {
        player.GetAnimator().SetTrigger("Dead");
        foreach (Enemy enemy in enemies) {
            if (enemy != null) {
                enemy.GetAnimator().SetTrigger("Dead");
            }
        }
        yield return new WaitForSeconds(0.8f);
        gameOverMenu.SetActive(true);
        endTurnButton.SetActive(true);
    }

    public void OnPauseGameClick() {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void StageEventExecute() {
        int eventNumber = 0;
        if (PlayerPrefs.HasKey("random event")) {
            eventNumber = PlayerPrefs.GetInt("random event");
        }
        // Event 1: lock cat
        if (eventNumber == 1) {
            deckManager.LockCard();
            GameObject lockedCard = Instantiate(deckManager.GetCurrentDeck().GetCardList()[deckManager.GetLockedCard()]);
            lockedCard.GetComponent<Cards>().DisableAllScripts();
            lockedCard.transform.SetParent(lockedCardPanel.transform, false);
            notificationMenu.SetActive(true);
        }
        // Event 2: Heal Cat
        else if (eventNumber == 2) {
            if (player.GetMaxHp() - player.GetHealth() <= 20) {
                player.SetHealth(player.GetMaxHp());
            } else {
                player.SetHealth(player.GetHealth() + 20);
            }
            PlayerPrefs.SetInt("health", player.GetHealth());
            playerHUD.SetHP(player.GetHealth());
        }
        // Event 3: Poison Cat
        else if (eventNumber == 3) {
            player.ChangeIsPoisoned(true);
            playerHUD.RenderEnemyPoisonIcon();

            AbstractEvent[] newEvent = {new PlayerPoisonDamageEvent(3, 0)};
            AbstractEvent[] resetEvent = {new PlayerPoisonEvent(false, 0)};

            if (enemyEventManager.ContainsKey(currentTurn)) {
                AbstractEvent[] currEvent = (AbstractEvent[])enemyEventManager[currentTurn];
                enemyEventManager[currentTurn] = currEvent.Concat(newEvent).ToArray();
            } else {
                enemyEventManager.Add(currentTurn, newEvent);
            }

            if (enemyEventManager.ContainsKey(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[])enemyEventManager[currentTurn + 1];
                enemyEventManager[currentTurn + 1] = currEvent.Concat(newEvent).ToArray();
            } else {
                enemyEventManager.Add(currentTurn + 1, newEvent);
            }

            if (playerEventManager.ContainsKey(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[])playerEventManager[currentTurn + 1];
                playerEventManager[currentTurn + 1] = currEvent.Concat(resetEvent).ToArray();
            } else {
                playerEventManager.Add(currentTurn + 1, resetEvent);
            }        
        }
        // Event 4: Recess Cat
        else if (eventNumber == 4) {
            int randomEnemy = Random.Range(0, enemies.Count());
            enemies[randomEnemy].ChangeIsImmobilised(true);
            enemies[randomEnemy].GetComponentInParent<BattleHUD>().RenderStunIcon();
            
            AbstractEvent[] newResetEvent = {new StunEvent(false, randomEnemy)};
            if (playerEventManager.ContainsKey(currentTurn)) {
                AbstractEvent[] currEvent = (AbstractEvent[])playerEventManager[currentTurn];
                playerEventManager[currentTurn] = currEvent.Concat(newResetEvent).ToArray();
            } else {
                playerEventManager.Add(currentTurn, newResetEvent);
            }
        }
    }

}
