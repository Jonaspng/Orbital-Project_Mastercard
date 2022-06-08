using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EventStageManager : MonoBehaviour {

    public static EventStageManager instance;

    public TextMeshProUGUI eventDescription;

    public GameObject approachButton;

    public GameObject walkAwayButton;

    public GameObject nextStageButton;
    
    private void Awake() {
        instance = this;
    }

    private void Start() {
        Initialise();
    }

    public void Initialise() {
        eventDescription.text = "You see a hooded cat in the distance ....";
        approachButton.SetActive(true);
        walkAwayButton.SetActive(true);
        nextStageButton.SetActive(false);
    }

    public void OnWalkAwayClick() {
        SceneManager.LoadScene("Stage 1");
    }

    public void OnApproachClick() {
        approachButton.SetActive(false);
        walkAwayButton.SetActive(false);
        int eventNumber = 3;
        //Random.Range(1, 5);
        // Event 1: Lock Cat
        if (eventNumber == 1) {
            eventDescription.text = "You met a Lock Cat that locked one of your card for the next stage!";

        }
        // Event 2: Heal Cat
        else if (eventNumber == 2) {
            eventDescription.text = "You met a Healer Cat that healed you by 20 HP!";
            
        }
        // Event 3: Poison Cat
        else if (eventNumber == 3) {
            eventDescription.text = "You met a Poison cat that poisoned you for the next 2 turns!";
        }
        // Event 4: Recess Cat
        else {
            eventDescription.text = "You met a Recess cat that stunned 1 of your next stage enemy for 1 turn!";

        }
        PlayerPrefs.SetInt("random event", eventNumber);
        nextStageButton.SetActive(true);
    }



   
}
