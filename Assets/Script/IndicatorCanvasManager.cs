using UnityEngine;
using TMPro;

public class IndicatorCanvasManager : MonoBehaviour {
    public GameObject popUpMenu;

    public TextMeshProUGUI iconName;

    public TextMeshProUGUI iconDescription;

    public void OnAttackClick() {
        iconName.text = "Attack Indicator";
        iconDescription.text = "The enemy will attack on the next turn.<color=#00000000> testing 12234fafafafafasf";
        popUpMenu.SetActive(true);
    }

    public void OnAllAttackUpClick() {
        iconName.text = "All Attack Up Indicator";
        iconDescription.text = "This enemy will give a 25% damage buff to all enemies on the next turn.<color=#00000000> testing 12234fafafafafasf";
        popUpMenu.SetActive(true);
    }

    public void OnShieldClick() {
        iconName.text = "Shield Indicator";
        iconDescription.text = "This enemy will apply shield on the next turn.<color=#00000000> testing 12234fafafafafasf";
        popUpMenu.SetActive(true);
    }

    public void OnBrokenClick() {
        iconName.text = "Broken Indicator";
        iconDescription.text = "This enemy will apply attack and inflict broken status on the player on the next turn.<color=#00000000> testing 12234fafafafafasf";
        popUpMenu.SetActive(true);
    }

    public void OnAllShieldClick() {
        iconName.text = "Shield All Indicator";
        iconDescription.text = "This enemy will apply shield for all enemies on the next turn.<color=#00000000> testing 12234fafafafafasf";
        popUpMenu.SetActive(true);
    }
    
    public void OnSummonClick() {
        iconName.text = "Summon Indicator";
        iconDescription.text = "This enemy will summon a new enemy on the next turn.<color=#00000000> testing 12234faf afafaf asf asfasfafafafa";
        popUpMenu.SetActive(true);
    }

}
