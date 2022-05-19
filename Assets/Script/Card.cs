using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
[System.Serializable]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite frame;
    public Sprite artwork;
    public int manaCost;


}
