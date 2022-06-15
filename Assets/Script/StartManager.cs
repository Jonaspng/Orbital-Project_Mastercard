using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject gameSelectionMenu;  
    public void OnStartClick() {
        gameSelectionMenu.SetActive(true);
    }

    public void OnCreditsClick() {
        SceneManager.LoadScene("Credits");
    }
    public void OnCloseClick() {
        gameSelectionMenu.SetActive(false);
    }
}
