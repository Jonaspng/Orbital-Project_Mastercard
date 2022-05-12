using UnityEngine;

public class Bash : MonoBehaviour {

    public Cards bashCard;
    // Start is called before the first frame update
    void Start() {
        bashCard = new AttackCards("Bash", 8, 1, 0, false, 2);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(bashCard);
        }        
    }
}
