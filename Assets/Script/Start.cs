using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour {
    
    public void StartButton() {
        SceneManager.LoadScene("Character Selection");
    }
}
