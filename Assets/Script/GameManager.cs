using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

class GameManager : MonoBehaviour {

    [SerializeField] private static GameManager instance;

    [SerializeField] private int stageNumber = 1;

    [SerializeField] private string playerType;

    [SerializeField] private GameObject[] enemyPrefabList;

    [SerializeField] private GameObject[] playerPrefabList;

    [SerializeField] private GameObject enemyPanel;

    [SerializeField] private Image ManaImage;

    [SerializeField] private Sprite ManaWarrior;

    [SerializeField] private Sprite ManaArcher;

    [SerializeField] private Sprite ManaMage;


    private void Awake() {
        instance = this;
    }

    public int GetStageNumber() {
        return this.stageNumber;
    }

    public static GameManager GetInstance() {
        return instance;
    }

    public void InitialiseStage() {
        if (PlayerPrefs.HasKey("stage")) {
            this.stageNumber = PlayerPrefs.GetInt("stage");
        }
        InitialisePlayer();
        InitialiseEnemies();
    }

    public void InitialisePlayer() {
        StageManager stage = StageManager.GetInstance();

        if (stage.GetPlayer() == null || PlayerPrefs.GetInt("health") <= 0) {
            playerType = PlayerPrefs.GetString("character");
            if (playerType == "Warrior") {
                stage.SetPlayerGO(Instantiate(playerPrefabList[0], stage.GetPlayerBattleStation()));
                ManaImage.sprite = ManaWarrior;
            } else if (playerType == "Archer") {
                stage.SetPlayerGO(Instantiate(playerPrefabList[1], stage.GetPlayerBattleStation()));
                ManaImage.sprite = ManaArcher;
            } else {
                stage.SetPlayerGO(Instantiate(playerPrefabList[2], stage.GetPlayerBattleStation()));
                ManaImage.sprite = ManaMage;
            }
            Player player = stage.GetPlayerGO().GetComponentInChildren<Player>();
            if (PlayerPrefs.HasKey("health")) {
                player.SetHealth(PlayerPrefs.GetInt("health"));
            }            
            stage.SetPlayer(player);
            stage.SetPlayerHUD(player.GetComponentInParent<BattleHUD>());
            stage.GetPlayerHUD().SetHUD(player);
        }
        
    }

    public void InitialiseEnemies() {
        StageManager stage = StageManager.GetInstance();
        if (stageNumber == 1) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[0]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.SetEnemyIndex(0);

            Enemy[] temp = {enemy1};
            stage.SetEnemies(temp);
            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);

            stage.SetEnemyCount(1);

        } else if (stageNumber == 2) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[0]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.SetEnemyIndex(0);

            GameObject enemy2GO = Instantiate(enemyPrefabList[0]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.SetEnemyIndex(1);

            Enemy[] temp = {enemy1, enemy2};
            stage.SetEnemies(temp);

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);

            stage.SetEnemyCount(2);

        } else if (stageNumber == 3) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[1]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.SetEnemyIndex(0);

            GameObject enemy2GO = Instantiate(enemyPrefabList[0]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.SetEnemyIndex(1);

            GameObject enemy3GO = Instantiate(enemyPrefabList[0]);
            enemy3GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy3 = enemy3GO.GetComponentInChildren<Enemy>();
            enemy3.SetEnemyIndex(2);

            Enemy[] temp = {enemy1, enemy2, enemy3};
            stage.SetEnemies(temp);

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);
            enemy3GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy3);

            stage.SetEnemyCount(3);
        } else if (stageNumber == 4) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[1]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.SetEnemyIndex(0);

            GameObject enemy2GO = Instantiate(enemyPrefabList[1]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.SetEnemyIndex(1);

            GameObject enemy3GO = Instantiate(enemyPrefabList[0]);
            enemy3GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy3 = enemy3GO.GetComponentInChildren<Enemy>();
            enemy3.SetEnemyIndex(2);

            GameObject enemy4GO = Instantiate(enemyPrefabList[0]);
            enemy4GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy4 = enemy4GO.GetComponentInChildren<Enemy>();
            enemy4.SetEnemyIndex(3);

            Enemy[] temp = {enemy1, enemy2, enemy3, enemy4};
            stage.SetEnemies(temp);

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);
            enemy3GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy3);
            enemy4GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy4);

            stage.SetEnemyCount(4);
        } else if (stageNumber == 5) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[0]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.SetEnemyIndex(0);

            GameObject enemy2GO = Instantiate(enemyPrefabList[1]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.SetEnemyIndex(1);

            GameObject enemy3GO = Instantiate(enemyPrefabList[1]);
            enemy3GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy3 = enemy3GO.GetComponentInChildren<Enemy>();
            enemy3.SetEnemyIndex(2);

            GameObject enemy4GO = Instantiate(enemyPrefabList[2]);
            enemy4GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy4 = enemy4GO.GetComponentInChildren<Enemy>();
            enemy4.SetEnemyIndex(3);

            Enemy[] temp = {enemy1, enemy2, enemy3, enemy4};
            stage.SetEnemies(temp);

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);
            enemy3GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy3);
            enemy4GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy4);

            stage.SetEnemyCount(4);
        } else if (stageNumber == 6) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[3]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.SetEnemyIndex(0);

            Enemy[] temp = {enemy1};
            stage.SetEnemies(temp);

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            stage.SetEnemyCount(1); 
        }
    }


    public void SpawnEnemies() {
        StageManager stage = StageManager.GetInstance();
        int randomIndex = Random.Range(0, 3);
        GameObject enemy1GO = Instantiate(enemyPrefabList[randomIndex]);
        enemy1GO.transform.SetParent(enemyPanel.transform, false);
        Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();

        enemy1.SetEnemyIndex(stage.GetEnemies().Length);

        enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);

        Enemy[] temp = {enemy1};
        stage.SetEnemies(stage.GetEnemies().Concat(temp).ToArray());

        int currentTurn = StageManager.GetInstance().GetCurrentTurn();
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.GetInstance().GetPlayerEventManager();
        AbstractEvent[] newResetEvent = {new StunEvent(false, enemy1.GetEnemyIndex())};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newResetEvent);
        }
        enemy1.ChangeIsImmobilised(true);

        stage.SetEnemyCount(stage.GetEnemyCount() + 1);
    }

}