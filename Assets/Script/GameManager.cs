using UnityEngine;


class GameManager : MonoBehaviour {

    public int stageNumber = 1;

    public string playerType;

    public GameObject[] enemyPrefabList;
    //sword cat, staff cat, armour cat, boss cat

    public GameObject[] playerPrefabList;

    public GameObject enemyPanel;



    private void Start() {
            
    }


    public void InitialiseStage() {
        InitialisePlayer();
        InitialiseEnemies();
    }



    public void InitialisePlayer() {
        StageManager stage = StageManager.instance;
        if (stageNumber == 1 || stage.player.getHealth() <= 0) {
            playerType = PlayerPrefs.GetString("character");
            if (playerType == "Warrior") {
                stage.playerGO = Instantiate(playerPrefabList[0], stage.playerBattleStation);
            } else if (playerType == "Archer") {
                stage.playerGO = Instantiate(playerPrefabList[1], stage.playerBattleStation);
            } else {
                stage.playerGO = Instantiate(playerPrefabList[2], stage.playerBattleStation);
            }
        
            Player player = stage.playerGO.GetComponent<Player>();
            stage.player = player;
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

        }
    }

}