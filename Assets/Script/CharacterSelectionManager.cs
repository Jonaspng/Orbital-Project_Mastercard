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
    }

    public void OnArrowClick() {
        PlayerPrefs.SetString("character", "Archer");
        confirmButton.SetActive(true);
    }

    public void OnStaffClick() {
        PlayerPrefs.SetString("character", "Mage");
        confirmButton.SetActive(true);
    }


    public void OnConfirmClick() {
        SceneManager.LoadScene("Stage 1");

    }
}
