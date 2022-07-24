using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {
    [SerializeField] private string cardName;
    [SerializeField] private Sprite frame;
    [SerializeField] private int manaCost;
    
    public Sprite GetFrame() {
        return this.frame;
    }

    public string GetCardName() {
        return this.cardName;
    }

    public int GetManaCost() {
        return this.manaCost;
    }
}
