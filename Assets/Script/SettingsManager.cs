using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour  {

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

    public void OnCloseClick() {
        this.gameObject.SetActive(false);
    }

}
