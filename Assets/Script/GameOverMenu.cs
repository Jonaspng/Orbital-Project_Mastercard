using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour  {

    public GameObject restartStageButton;

    public GameObject returnToMainButton;

 
    void Start ()  {
        this.gameObject.SetActive(false); 
    }

         
    public void OnRestartClick() {
        this.gameObject.SetActive(false);
        StageManager.instance.InitialiseBattle();
       
    }


    public void OnReturnClick() {
       SceneManager.LoadScene("Start Menu");
    }

    // public void RenderConfirmButton() {
    //     confirmButton.SetActive(true);
    // }
}
