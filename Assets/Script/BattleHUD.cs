using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour {
   
   public TextMeshProUGUI nameText;

   public Slider hpSlider;

   public GameObject iconsBar;

   public GameObject shieldIcon;

   public GameObject poisonIcon;

   public bool isShieldIconOn;

   public void SetHUD(Unit unit) {
       nameText.text = unit.unitName;
       hpSlider.maxValue = unit.maxHp;
       hpSlider.value = unit.health;
   }


   public void SetHP(int hp) {
       hpSlider.value = hp;
   }

   public void RenderPlayerShieldIcon(int shield) {
       int baseShield = StageManager.instance.player.baseShield;
       if (!isShieldIconOn) {
           GameObject icon = Instantiate(shieldIcon, iconsBar.transform);
           icon.GetComponentInChildren<TextMeshProUGUI>().text = shield.ToString();
           isShieldIconOn = true;
       } else {
           iconsBar.transform.Find("Shield Icon(Clone)").GetComponentInChildren<TextMeshProUGUI>().text = (baseShield + shield).ToString();
       }
   }

   public void RenderEnemyShieldIcon(int shield, int enemyIndex) {
       int baseShield = StageManager.instance.enemies[enemyIndex].baseShield;
       if (!isShieldIconOn) {
           GameObject icon = Instantiate(shieldIcon, iconsBar.transform);
           icon.GetComponentInChildren<TextMeshProUGUI>().text = shield.ToString();
           isShieldIconOn = true;
       } else {
           iconsBar.transform.Find("Shield Icon(Clone)").GetComponentInChildren<TextMeshProUGUI>().text = (baseShield + shield).ToString();
       }
   }

   public void RemoveShieldIcon() {
       if (isShieldIconOn) {
           GameObject.Destroy(iconsBar.transform.Find("Shield Icon(Clone)").gameObject);
           isShieldIconOn = false;
       }
       
   }

   public void RenderEnemyPoisonIcon(int enemyIndex) {
        GameObject icon = Instantiate(poisonIcon, iconsBar.transform);
   }

   public void RemovePoisonIcon() {
        GameObject.Destroy(iconsBar.transform.Find("Poison Icon(Clone)").gameObject);
   }

}
