using System.IO;
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

    public string currentCharacter;

    public int currentHp;

    public Sprite warriorSelection;

    public Sprite archerSelection;

    public Sprite mageSelection;

    public GameObject confirmButton;

    public string deckPath;
    private void Start() {
        deckPath = $"{Application.persistentDataPath}/deckID.json";
        OnSwordClick();
        GameObject.Find("Warrior Button").GetComponent<Button>().Select();
    }

    public void OnSwordClick() {
        backgroundImage.GetComponent<Image>().sprite = warriorSelection;
        currentCharacter = "Warrior";
        currentHp = 60;
        characterName.GetComponent<TextMeshProUGUI>().color = Color.white;
        hp.GetComponent<TextMeshProUGUI>().color = Color.white;
        description.GetComponent<TextMeshProUGUI>().color = Color.white;
        characterName.GetComponent<TextMeshProUGUI>().text = "Warrior";
        hp.GetComponent<TextMeshProUGUI>().text = "HP: 60/60";
        description.GetComponent<TextMeshProUGUI>().text = "High Health \nLow DPS \nSpecial Perk: Can Debuff Enemies";
    }

    public void OnArrowClick() {
        backgroundImage.GetComponent<Image>().sprite = archerSelection;
        currentCharacter = "Archer";
        currentHp = 40;
        characterName.GetComponent<TextMeshProUGUI>().color = Color.black;
        hp.GetComponent<TextMeshProUGUI>().color = Color.black;
        description.GetComponent<TextMeshProUGUI>().color = Color.black;
        characterName.GetComponent<TextMeshProUGUI>().text = "Archer";
        hp.GetComponent<TextMeshProUGUI>().text = "HP: 40/40";
        description.GetComponent<TextMeshProUGUI>().text = "Low Health \nNormal DPS \nSpecial Perk: Can Evade Attacks";
    }

    public void OnStaffClick() {
        backgroundImage.GetComponent<Image>().sprite = mageSelection;
        currentCharacter = "Mage";
        currentHp = 50;
        characterName.GetComponent<TextMeshProUGUI>().color = Color.black;
        hp.GetComponent<TextMeshProUGUI>().color = Color.black;
        description.GetComponent<TextMeshProUGUI>().color = Color.black;
        characterName.GetComponent<TextMeshProUGUI>().text = "Mage";
        hp.GetComponent<TextMeshProUGUI>().text = "HP: 50/50";
        description.GetComponent<TextMeshProUGUI>().text = "Normal Health \nHigh DPS \nSpecial Perk: Can Gain Mana";
    }


    public void OnConfirmClick() {
        PlayerPrefs.SetString("character", currentCharacter);
        PlayerPrefs.SetInt("health", currentHp);
        File.Delete(deckPath);
        PlayerPrefs.SetInt("stage", 1);
        PlayerPrefs.SetInt("random event", 0);
        SceneManager.LoadScene("Stage 1");
    }

    public void OnBackClick() {
        SceneManager.LoadScene("Start Menu");
    }
}
