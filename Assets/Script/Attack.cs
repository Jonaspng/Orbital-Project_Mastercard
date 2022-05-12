using UnityEngine;

public class Attack : MonoBehaviour {

    public Cards attackCard;
    // Start is called before the first frame update
    void Start() {
        attackCard = new AttackCards("Attack", 6, 1, 0);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(attackCard);
        }        
    }
}
