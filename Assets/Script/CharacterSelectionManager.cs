using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour {

    public string characterChosen;

    public GameObject confirmButton;

    private void Start() {
        confirmButton.SetActive(false);
    }

    public void OnSwordClick() {
        PlayerPrefs.SetString("character", "Warrior");
        confirmButton.SetActive(true);
        PlayerPrefs.SetInt("health", 60);
    }

    public void OnArrowClick() {
        PlayerPrefs.SetString("character", "Archer");
        confirmButton.SetActive(true);
        PlayerPrefs.SetInt("health", 40);
    }

    public void OnStaffClick() {
        PlayerPrefs.SetString("character", "Mage");
        confirmButton.SetActive(true);
        PlayerPrefs.SetInt("health", 50);
    }


    public void OnConfirmClick() {
        SceneManager.LoadScene("Stage 1");

    }
}
