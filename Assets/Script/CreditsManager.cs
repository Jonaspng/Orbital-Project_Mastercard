using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour {

    public void OnBackClick() {
        SceneManager.LoadScene("Start Menu");
    }

}
