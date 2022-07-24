using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour {
    [SerializeField] private Card card;

    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Image frameImage;

    [SerializeField] private TextMeshProUGUI manaText;


    // Start is called before the first frame update
    void Start() {
     nameText.text = card.GetCardName();
     descriptionText.text = this.GetComponent<Cards>().GetDescription();
     frameImage.sprite = card.GetFrame();
     manaText.text = card.GetManaCost().ToString();   
    }
}
