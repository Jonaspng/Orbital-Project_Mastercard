using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public Image frameImage;
    public Image artworkImage;
    public TextMeshProUGUI manaText;


    // Start is called before the first frame update
    void Start()
    {
     nameText.text = card.cardName;
     descriptionText.text = card.description;
     frameImage.sprite = card.frame;
     artworkImage.sprite = card.artwork;
     manaText.text = card.manaCost.ToString();   
    }
}