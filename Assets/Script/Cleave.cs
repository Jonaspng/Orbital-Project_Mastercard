using UnityEngine;

public class Cleave : MonoBehaviour {

    public Cards cleaveCard;
    // Start is called before the first frame update
    void Start() {
        cleaveCard = new AttackCards("Cleave", 8, 1, 0, true, 1);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(cleaveCard);
        }        
    }
}
