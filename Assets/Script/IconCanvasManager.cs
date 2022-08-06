using UnityEngine;
using TMPro;

public class IconCanvasManager : MonoBehaviour {
    
    [SerializeField] private GameObject popUpMenu;

    [SerializeField] private TextMeshProUGUI iconName;

    [SerializeField] private TextMeshProUGUI iconDescription;

    public void OnAttackUpClick() {
        iconName.text = "Attack Up Icon";
        iconDescription.text = "Player/Enemy deals 25% more damage. <color=#00000000> testing 12234affafdsafafdasfdsa";
        popUpMenu.SetActive(true);
    }

    public void OnAttackDownClick() {
        iconName.text = "Attack Down Icon";
        iconDescription.text = "Player/Enemy Deals 25% less damage. <color=#00000000> testing 12234fafafafafasf";
        popUpMenu.SetActive(true);
    }

    public void OnShieldClick() {
        iconName.text = "Shield Icon";
        iconDescription.text = "Reduce incoming damage by amount written on Shield Icon. Lasts for one turn.";
        popUpMenu.SetActive(true);
    }

    public void OnBrokenClick() {
        iconName.text = "Broken Icon";
        iconDescription.text = "All incoming attack deals 25% more damage. Enemy broken attack on player will last 2 turns. Effect not stackable, but effect period is stackable in different turns.";
        popUpMenu.SetActive(true);
    }

    public void OnDodgeClick() {
        iconName.text = "Dodge Icon";
        iconDescription.text = "Dodge next attack. Lasts for one turn. If enemy does not attack within that turn, the dodge status will be cleared.";
        popUpMenu.SetActive(true);
    }

    public void OnSmokeBombClick() {
        iconName.text = "Smoke Bomb Icon";
        iconDescription.text = "Player have a 50% chance of dodging the next attack. Effect will last one turn.";
        popUpMenu.SetActive(true);
    }

    public void OnStunClick() {
        iconName.text = "Stun Icon";
        iconDescription.text = "Enemy will not move in the next turn irregardless of the preattack indicator.";
        popUpMenu.SetActive(true);
    }

    public void OnPoisionClick() {
        iconName.text = "Poison Icon";
        iconDescription.text = "Player/Enemy will take 3 damage per turn for 2 turns. Effect cannot be blocked by shield.";
        popUpMenu.SetActive(true);
    }

    public void OnFireClick() {
        iconName.text = "Fire Icon";
        iconDescription.text = "Player/Enemy will take 3 damage per turn for 2 turns. Effect cannot be blocked by shield.";
        popUpMenu.SetActive(true);
    }

    public void OnReflectClick() {
        iconName.text = "Reflect Icon";
        iconDescription.text = "Reflect 75% of incoming damage. Lasts for 1 turn.";
        popUpMenu.SetActive(true);
    }

    public void OnMysticShieldClick() {
        iconName.text = "Mystic Shield Icon";
        iconDescription.text = "Reduce incoming attack by 25%. Lasts for 1 turn.";
        popUpMenu.SetActive(true);
    }

    public void OnEndureClick() {
        iconName.text = "Endure Icon";
        iconDescription.text = "If attack kills player, player will be revived with one health. Lasts for 1 turn.";
        popUpMenu.SetActive(true);
    }

}
