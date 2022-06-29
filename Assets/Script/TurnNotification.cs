using UnityEngine;
using TMPro;

public class TurnNotification : MonoBehaviour {

    public Animator backgroundAnimator;

    public Animator textAnimator;


    public void ChangeText(string message) {
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
    } 

}
