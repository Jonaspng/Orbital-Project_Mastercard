using UnityEngine;
using TMPro;

public class TurnNotification : MonoBehaviour {

    public Animator animator;


    public void ChangeText(string message) {
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
    } 

}
