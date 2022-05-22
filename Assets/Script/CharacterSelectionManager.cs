using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour {

    public string characterChosen;

    public void OnSwordClick() {
        PlayerPrefs.SetString("character", "Warrior");
    }

    public void OnArrowClick() {
        PlayerPrefs.SetString("character", "Archer"); 
    }

    public void OnStaffClick() {
        PlayerPrefs.SetString("character", "Mage");
    }


    public void OnConfirmClick() {
        SceneManager.LoadScene("Stage 1");

    }
}
