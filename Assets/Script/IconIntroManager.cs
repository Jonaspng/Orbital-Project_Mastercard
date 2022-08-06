using UnityEngine;
using UnityEngine.SceneManagement;
public class IconIntroManager : MonoBehaviour {
    
    [SerializeField] private GameObject popUpMenu;

    [SerializeField] private GameObject iconCanvas;

    [SerializeField] private GameObject indicatorCanvas;

    [SerializeField] private GameObject nextButton;

    [SerializeField] private GameObject previousButton;

    [SerializeField] private int counter = 1;

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
