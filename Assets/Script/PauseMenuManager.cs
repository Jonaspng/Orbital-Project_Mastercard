using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour  {
 
    void Start ()  {
        this.gameObject.SetActive(false); 
    }

    public void OnReturnClick() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Menu");
    }

    public void OnResumeGameClick() {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }


}
