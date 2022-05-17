using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour {
   
   public TextMeshProUGUI nameText;

   public Slider hpSlider;

   public void SetHUD(Unit unit) {
       nameText.text = unit.unitName;
       hpSlider.maxValue = unit.maxHp;
       hpSlider.value = unit.health;
   }


   public void SetHP(int hp) {
       hpSlider.value = hp;
   }
}
