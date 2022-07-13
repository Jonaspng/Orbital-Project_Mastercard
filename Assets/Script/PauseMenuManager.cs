using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour  {

    [SerializeField] private Slider volumeSlider;
    
    private void Awake() {
        if (PlayerPrefs.HasKey("volumeValue")) {
            float volume = PlayerPrefs.GetFloat("volumeValue");
            volumeSlider.value = volume; 
        } else {
            volumeSlider.value = 1.0f;
        }
    }

    private void Update() {
        PlayerPrefs.SetFloat("volumeValue", volumeSlider.value);
        AudioListener.volume = volumeSlider.value;
    }

 
    private void Start ()  {
        this.gameObject.SetActive(false); 
    }

    public void OnReturnClick() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Menu");
    }

    public void OnResumeGameClick() {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

}
