using UnityEngine;
using TMPro;

public class TurnNotification : MonoBehaviour {

    [SerializeField] private Animator backgroundAnimator;

    [SerializeField] private Animator textAnimator;

    public Animator GetBackgroundAnimator() {
        return this.backgroundAnimator;
    }

    public Animator GetTextAnimator() {
        return this.textAnimator;
    }

    public void ChangeText(string message) {
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = message;
    } 

}
