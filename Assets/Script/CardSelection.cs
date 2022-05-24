using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour {
    public Material outline;


    private void Start() {
        outline = Resources.Load<Material>("Image_contour_inside");
    }

    public void OnMouseDown() {
        this.GetComponentInChildren<Image>().material = outline;
        StageManager.instance.deckManager.currentDeckID.AddCardID(this.GetComponent<Cards>().id);     
    }

}
