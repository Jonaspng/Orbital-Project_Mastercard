using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class TextWriter : MonoBehaviour {

    private TextMeshProUGUI uiText;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;

    public GameObject nextButton;

    public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter) {
        this.uiText = uiText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        characterIndex = 0;
    }

    public void OnSkipButtonClick() {
        if (characterIndex <= textToWrite.Length - 1) {
            characterIndex = textToWrite.Length - 1;
            uiText.text = textToWrite.Substring(0, textToWrite.Length);
        }
    }


    private void Update() {
        if (uiText != null) {
            timer -= Time.deltaTime;
            while (timer <= 0f) {
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);
                text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                uiText.text = text;

                if (characterIndex >= textToWrite.Length) {
                    this.gameObject.GetComponent<AudioSource>().Pause();
                    uiText = null;
                    nextButton.SetActive(true);
                    return;
                }
            }
        }
    }
  
}
