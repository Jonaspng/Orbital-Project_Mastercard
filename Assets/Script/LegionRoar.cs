using UnityEngine;

public class LegionRoar : MonoBehaviour {

    public Cards legionRoarCard;
    // Start is called before the first frame update
    void Start() {
        legionRoarCard = new SkillCards("Legion's Roar", 8, 1, 0, 1, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(legionRoarCard);
        }        
    }
}
