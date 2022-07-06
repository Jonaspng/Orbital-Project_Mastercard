using UnityEngine;
using UnityEngine.SceneManagement;

public class LibrarySelectionManager : MonoBehaviour {

    void Start() {
        this.gameObject.SetActive(false);
    }

    public void OnCardsClick() {
        SceneManager.LoadScene("Cards");
    }

    public void OnHowToPlayClick() {
        SceneManager.LoadScene("Tut Page");

    }

    public void OnCloseClick() {
        this.gameObject.SetActive(false);
    }

    
}