using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour {

    [SerializeField] private string characterChosen;

    [SerializeField] private GameObject backgroundImage;
    
    [SerializeField] private GameObject characterName;

    [SerializeField] private GameObject hp;

    [SerializeField] private GameObject description;

    [SerializeField] private string currentCharacter;

    [SerializeField] private int currentHp;

    [SerializeField] private Sprite warriorSelection;

    [SerializeField] private Sprite archerSelection;

    [SerializeField] private Sprite mageSelection;

    [SerializeField] private GameObject warriorButton;

    [SerializeField] private GameObject archerButton;

    [SerializeField] private GameObject mageButton;

    [SerializeField] private Material buttonOutline;

    [SerializeField] private GameObject confirmButton;

    [SerializeField] private string deckPath;
    private void Start() {
        deckPath = $"{Application.persistentDataPath}/deckID.json";
        OnSwordClick();
        GameObject.Find("Warrior Button").GetComponent<Button>().Select();
    }

    public void OnSwordClick() {
        warriorButton.GetComponent<Image>().material = buttonOutline;
        archerButton.GetComponent<Image>().material = null;
        mageButton.GetComponent<Image>().material = null;
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
        warriorButton.GetComponent<Image>().material = null;
        archerButton.GetComponent<Image>().material = buttonOutline;
        mageButton.GetComponent<Image>().material = null;
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
        warriorButton.GetComponent<Image>().material = null;
        archerButton.GetComponent<Image>().material = null;
        mageButton.GetComponent<Image>().material = buttonOutline;
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
