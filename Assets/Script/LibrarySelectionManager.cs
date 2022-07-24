using UnityEngine;
using UnityEngine.SceneManagement;

public class LibrarySelectionManager : MonoBehaviour {

    public void OnCardsClick() {
        SceneManager.LoadScene("Cards");
    }

    public void OnIconsClick() {
        SceneManager.LoadScene("Icons Intro");
    }

    public void OnHowToPlayClick() {
        SceneManager.LoadScene("Tut Page");

    }

    public void OnCloseClick() {
        this.gameObject.SetActive(false);
    }

    
}