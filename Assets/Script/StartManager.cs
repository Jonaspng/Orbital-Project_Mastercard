using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject gameSelectionMenu;

    public GameObject libraryMenu;
    public void OnStartClick() {
        gameSelectionMenu.SetActive(true);
    }

    public void OnCreditsClick() {
        SceneManager.LoadScene("Credits");
    }

    public void OnLibraryClick() {
        libraryMenu.SetActive(true);
    }
}
