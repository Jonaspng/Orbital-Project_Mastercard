using UnityEngine;
using UnityEngine.SceneManagement;
public class IconIntroManager : MonoBehaviour {
    
    public GameObject popUpMenu;

    public GameObject iconCanvas;

    public GameObject indicatorCanvas;

    public GameObject nextButton;

    public GameObject previousButton;

    private int counter = 1;

    
    public void OnCloseClick() {
        popUpMenu.SetActive(false);
    }

    public void OnNextClick() {
        if (counter == 1) {
            indicatorCanvas.SetActive(false);
            iconCanvas.SetActive(true);
            nextButton.SetActive(false);
            previousButton.SetActive(true);
            counter += 1;
        } 
    }

    public void OnPreviousClick() {
        if (counter == 2) {
            indicatorCanvas.SetActive(true);
            iconCanvas.SetActive(false);
            nextButton.SetActive(true);
            previousButton.SetActive(false);
            counter -= 1;
        }
    }

    public void OnMainMenuClick() {
        SceneManager.LoadScene("Start Menu");
    }

}
