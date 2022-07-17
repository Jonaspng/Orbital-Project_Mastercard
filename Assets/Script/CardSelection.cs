using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour {
    [SerializeField] private Material outline;

    public void SetOutline(Material ol) {
        this.outline = ol;
    }

    public void OnMouseDown() {
        GameObject.Find("StageManager").GetComponent<PopUpMenu>().RenderConfirmButton();
        foreach (Transform obj in GameObject.Find("NewCards").transform) {
            obj.gameObject.GetComponentInChildren<Image>().material = null;
        }
        this.GetComponentInChildren<Image>().material = outline;
        GameObject.Find("StageManager").GetComponent<PopUpMenu>().SetNewCardID(this.GetComponent<Cards>().GetId());    
    }

}
