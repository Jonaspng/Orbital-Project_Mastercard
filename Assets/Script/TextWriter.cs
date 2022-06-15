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
        characterIndex = textToWrite.Length - 1;
        uiText.text = textToWrite.Substring(0, textToWrite.Length);
    }


    private void Update() {
        if (uiText != null) {
            timer -= Time.deltaTime;
            if (timer <= 0f) {
                timer += timePerCharacter;
                characterIndex++;
                uiText.text = textToWrite.Substring(0, characterIndex);

                if (characterIndex >= textToWrite.Length) {
                    uiText = null;
                    nextButton.SetActive(true);
                    return;
                }
            }
        }
    }
  
}
