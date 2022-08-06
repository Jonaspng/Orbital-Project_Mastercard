using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour  {

    [SerializeField] private GameObject restartStageButton;

    [SerializeField] private GameObject returnToMainButton;

    private void Start ()  {
        this.gameObject.SetActive(false); 
    }

    public void OnRestartClick() {
        this.gameObject.SetActive(false);
        StageManager.GetInstance().InitialiseBattle();
    }

    public void OnReturnClick() {
       SceneManager.LoadScene("Start Menu");
    }

}
