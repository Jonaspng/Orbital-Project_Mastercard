using UnityEngine;

public class Testing : MonoBehaviour {
    
    public void ArrangeCards() {
        int numberOfCards = this.transform.childCount;
        float totalLength = 6/5 * numberOfCards;
        int totalRotation = 60/5 * numberOfCards;
        float startLength = -totalLength / 2;
        float startRotation = totalRotation / 2;
        float lengthIncr;
        float rotationIncr;
        if (numberOfCards == 1) {
            lengthIncr = 0;
            rotationIncr = 0;
            startRotation = 0;
            startLength = 0;
        } else {
            lengthIncr = totalLength / (numberOfCards- 1);
            rotationIncr = totalRotation / (numberOfCards - 1);
        }        
        int middle = numberOfCards / 2 + 1;
        int index = 1;
        float yShift = 1;
        foreach(Transform card in this.transform) {
            if (index == 1 || index == numberOfCards) {
                yShift = 0;
            } else if (index == middle && numberOfCards % 2 !=0 && numberOfCards != 3){
                yShift = 1f;
            } else if (index == middle && numberOfCards % 2 !=0 && numberOfCards == 3) {
                yShift = 0.4f;
            }
            else {
                yShift = 0.8f;
            }
            card.Translate(new Vector3(startLength, yShift, 0f));
            card.rotation = Quaternion.Euler(0, 0, startRotation);
            startRotation -= rotationIncr;
            startLength += lengthIncr;
            index++;
        }
    }

    public void ReArrangeCards() {
        foreach(Transform card in this.transform) {
            card.localPosition = new Vector3(0f, 0f, 0f);
            card.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        ArrangeCards();
    }
}
