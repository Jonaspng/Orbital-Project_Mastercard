using UnityEngine;
public class PopUpMenu : MonoBehaviour  {
    public GameObject cardSelectionMenu;
 
    void Start ()  {
        cardSelectionMenu.SetActive(false); 
    }
         
    public void PopUp()  { 
        cardSelectionMenu.SetActive (!cardSelectionMenu.activeSelf); 
    }
}
