using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour {

    public string characterChosen;

    public GameObject backgroundImage;
    
    public GameObject characterName;

    public GameObject hp;

    public GameObject description;

    public Sprite warriorSelection;

    public Sprite archerSelection;

    public Sprite mageSelection;

    public GameObject confirmButton;

    private void Start() {
        OnSwordClick();
        GameObject.Find("Warrior Button").GetComponent<Button>().Select();
    }

    public void OnSwordClick() {
        backgroundImage.GetComponent<Image>().sprite = warriorSelection;
        PlayerPrefs.SetString("character", "Warrior");
        PlayerPrefs.SetInt("health", 60);
        characterName.GetComponent<TextMeshProUGUI>().color = Color.white;
        hp.GetComponent<TextMeshProUGUI>().color = Color.white;
        description.GetComponent<TextMeshProUGUI>().color = Color.white;
        characterName.GetComponent<TextMeshProUGUI>().text = "Warrior";
        hp.GetComponent<TextMeshProUGUI>().text = "HP: 60/60";
        description.GetComponent<TextMeshProUGUI>().text = "High Health \nLow DPS \nSpecial Perk: Can Debuff Enemies";
    }

    public void OnArrowClick() {
        backgroundImage.GetComponent<Image>().sprite = archerSelection;
        PlayerPrefs.SetString("character", "Archer");
        PlayerPrefs.SetInt("health", 40);
        characterName.GetComponent<TextMeshProUGUI>().color = Color.black;
        hp.GetComponent<TextMeshProUGUI>().color = Color.black;
        description.GetComponent<TextMeshProUGUI>().color = Color.black;
        characterName.GetComponent<TextMeshProUGUI>().text = "Archer";
        hp.GetComponent<TextMeshProUGUI>().text = "HP: 40/40";
        description.GetComponent<TextMeshProUGUI>().text = "Low Health \nNormal DPS \nSpecial Perk: Can Evade Attacks";
    }

    public void OnStaffClick() {
        PlayerPrefs.SetString("character", "Mage");;
        PlayerPrefs.SetInt("health", 50);
        characterName.GetComponent<TextMeshProUGUI>().color = Color.black;
        hp.GetComponent<TextMeshProUGUI>().color = Color.black;
        description.GetComponent<TextMeshProUGUI>().color = Color.black;
        characterName.GetComponent<TextMeshProUGUI>().text = "Mage";
        hp.GetComponent<TextMeshProUGUI>().text = "HP: 50/50";
        description.GetComponent<TextMeshProUGUI>().text = "Normal Health \nHigh DPS \nSpecial Perk: Can Gain Mana";
    }


    public void OnConfirmClick() {
        SceneManager.LoadScene("Stage 1");
    }

    public void OnBackClick() {
        SceneManager.LoadScene("Start Menu");
    }
}
