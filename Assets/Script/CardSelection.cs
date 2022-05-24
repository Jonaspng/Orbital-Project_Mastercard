using UnityEngine;
using UnityEngine.UI;

public class CardSelection : MonoBehaviour {

    public Material outline = Resources.Load<Material>("Image_contour_inside");

    public void OnMouseDown() {
        this.GetComponent<Image>().material = outline;
       
    }

}
