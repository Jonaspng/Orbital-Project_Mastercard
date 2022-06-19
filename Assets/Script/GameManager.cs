using UnityEngine;
using System.Linq;

class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int stageNumber = 1;

    public string playerType;

    public GameObject[] enemyPrefabList;
    //sword cat, staff cat, armour cat, boss cat

    public GameObject[] playerPrefabList;

    public GameObject enemyPanel;


    private void Awake() {
        instance = this;
    }


    public void InitialiseStage() {
        if (PlayerPrefs.HasKey("stage")) {
            this.stageNumber = PlayerPrefs.GetInt("stage");
        }
        InitialisePlayer();
        InitialiseEnemies();
    }


    public void InitialisePlayer() {
        StageManager stage = StageManager.instance;

        if (stage.player == null || PlayerPrefs.GetInt("health") <= 0) {
            playerType = PlayerPrefs.GetString("character");
            if (playerType == "Warrior") {
                stage.playerGO = Instantiate(playerPrefabList[0], stage.playerBattleStation);
            } else if (playerType == "Archer") {
                stage.playerGO = Instantiate(playerPrefabList[1], stage.playerBattleStation);
            } else {
                stage.playerGO = Instantiate(playerPrefabList[2], stage.playerBattleStation);
            }
            Player player = stage.playerGO.GetComponentInChildren<Player>();
            if (PlayerPrefs.HasKey("health")) {
                player.health = PlayerPrefs.GetInt("health");
            }            
            stage.player = player;
            stage.playerHUD = player.GetComponentInParent<BattleHUD>();
            stage.playerHUD.SetHUD(player);
        }
        
    }


    public void InitialiseEnemies() {
        StageManager stage = StageManager.instance;
        if (stageNumber == 1) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[0]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.enemyIndex = 0;

            Enemy[] temp = {enemy1};
            stage.enemies = temp;
            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);

            stage.enemyCount = 1;

        } else if (stageNumber == 2) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[0]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.enemyIndex = 0;

            GameObject enemy2GO = Instantiate(enemyPrefabList[0]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.enemyIndex = 1;

            Enemy[] temp = {enemy1, enemy2};
            stage.enemies = temp;

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);

            stage.enemyCount = 2;

        } else if (stageNumber == 3) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[1]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.enemyIndex = 0;

            GameObject enemy2GO = Instantiate(enemyPrefabList[0]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.enemyIndex = 1;

            GameObject enemy3GO = Instantiate(enemyPrefabList[0]);
            enemy3GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy3 = enemy3GO.GetComponentInChildren<Enemy>();
            enemy3.enemyIndex = 2;

            Enemy[] temp = {enemy1, enemy2, enemy3};
            stage.enemies = temp;

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);
            enemy3GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy3);

            stage.enemyCount = 3;
        } else if (stageNumber == 4) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[1]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.enemyIndex = 0;

            GameObject enemy2GO = Instantiate(enemyPrefabList[1]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.enemyIndex = 1;

            GameObject enemy3GO = Instantiate(enemyPrefabList[0]);
            enemy3GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy3 = enemy3GO.GetComponentInChildren<Enemy>();
            enemy3.enemyIndex = 2;

            GameObject enemy4GO = Instantiate(enemyPrefabList[0]);
            enemy4GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy4 = enemy4GO.GetComponentInChildren<Enemy>();
            enemy4.enemyIndex = 3;

            Enemy[] temp = {enemy1, enemy2, enemy3, enemy4};
            stage.enemies = temp;

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);
            enemy3GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy3);
            enemy4GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy4);

            stage.enemyCount = 4;
        } else if (stageNumber == 5) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[0]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.enemyIndex = 0;

            GameObject enemy2GO = Instantiate(enemyPrefabList[1]);
            enemy2GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy2 = enemy2GO.GetComponentInChildren<Enemy>();
            enemy2.enemyIndex = 1;

            GameObject enemy3GO = Instantiate(enemyPrefabList[1]);
            enemy3GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy3 = enemy3GO.GetComponentInChildren<Enemy>();
            enemy3.enemyIndex = 2;

            GameObject enemy4GO = Instantiate(enemyPrefabList[2]);
            enemy4GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy4 = enemy4GO.GetComponentInChildren<Enemy>();
            enemy4.enemyIndex = 3;

            Enemy[] temp = {enemy1, enemy2, enemy3, enemy4};
            stage.enemies = temp;

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            enemy2GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy2);
            enemy3GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy3);
            enemy4GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy4);

            stage.enemyCount = 4;
        } else if (stageNumber == 6) {
            GameObject enemy1GO = Instantiate(enemyPrefabList[3]);
            enemy1GO.transform.SetParent(enemyPanel.transform, false);
            Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();
            enemy1.enemyIndex = 0;

            Enemy[] temp = {enemy1};
            stage.enemies = temp;

            enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);
            stage.enemyCount = 1; 
        }
    }


    public void SpawnEnemies() {
        StageManager stage = StageManager.instance;
        int randomIndex = Random.Range(0, 3);
        GameObject enemy1GO = Instantiate(enemyPrefabList[randomIndex]);
        enemy1GO.transform.SetParent(enemyPanel.transform, false);
        Enemy enemy1 = enemy1GO.GetComponentInChildren<Enemy>();

        enemy1.enemyIndex = stage.enemies.Length;

        enemy1GO.GetComponentInChildren<BattleHUD>().SetHUD(enemy1);

        Enemy[] temp = {enemy1};
        stage.enemies = stage.enemies.Concat(temp).ToArray();

        stage.enemyCount += 1;
    }

}