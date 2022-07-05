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
        Application.OpenURL("https://docs.google.com/presentation/d/1b52Y2mCH7Td_oWUPccRgyYxF42fbpnZFgVSGS9y12NI/edit?usp=sharing");

    }

    public void OnCloseClick() {
        this.gameObject.SetActive(false);
    }

    
}