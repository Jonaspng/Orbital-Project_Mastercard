using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour {
    public TextMeshProUGUI nameText;

    public Slider hpSlider;

    public GameObject iconsBar;

    public GameObject shieldIcon;

    public GameObject poisonIcon;

    public GameObject brokenShieldIcon;

    public GameObject burnedIcon;

    public GameObject stunIcon;

    public GameObject attackUpIcon;

    public GameObject attackDownIcon;

    public GameObject dodgeIcon;

    public GameObject smokeBombIcon;

    public GameObject mysticShieldIcon;

    public GameObject reflectIcon;

    public bool isShieldIconOn;

    public int maxHp;

    public void SetHUD(Unit unit) {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.maxHp;
        this.maxHp = unit.maxHp;
        hpSlider.value = unit.health;
        this.transform.Find("Health bar").GetComponentInChildren<TextMeshProUGUI>().text = unit.health + "/" + unit.maxHp;
    }

    public void SetHP(int hp) {
        hpSlider.value = hp;
        this.transform.Find("Health bar").GetComponentInChildren<TextMeshProUGUI>().text = hp + "/" + maxHp;
    }

    public void RenderPlayerShieldIcon(int shield) {
        int baseShield = StageManager.instance.player.baseShield;
        if (iconsBar.transform.Find("Shield Icon(Clone)") == null) {
            GameObject icon = Instantiate(shieldIcon, iconsBar.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().text = shield.ToString();
        } else {
            iconsBar.transform.Find("Shield Icon(Clone)").GetComponentInChildren<TextMeshProUGUI>().text = (baseShield + shield).ToString();
        }
    }

    public void RenderEnemyShieldIcon(int shield, int enemyIndex) {
        int baseShield = StageManager.instance.enemies[enemyIndex].baseShield;
        if (iconsBar.transform.Find("Shield Icon(Clone)") == null) {
            GameObject icon = Instantiate(shieldIcon, iconsBar.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().text = shield.ToString();
        } else {
            iconsBar.transform.Find("Shield Icon(Clone)").GetComponentInChildren<TextMeshProUGUI>().text = (baseShield + shield).ToString();
        }
    }

    public void RemoveShieldIcon() {
        Transform icon = iconsBar.transform.Find("Shield Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
   }

    public void RenderEnemyPoisonIcon() {
        Instantiate(poisonIcon, iconsBar.transform);
    }

    public void RemovePoisonIcon() {
        Transform icon = iconsBar.transform.Find("Poison Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }


    public void RenderEnemyBurnIcon() {
        Instantiate(burnedIcon, iconsBar.transform);
    }

    public void RemoveBurnIcon() {
        Transform icon = iconsBar.transform.Find("Burned Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderBrokenIcon() {
        Instantiate(brokenShieldIcon, iconsBar.transform);
    }

    public void RemoveBrokenIcon() {
        Transform icon  = iconsBar.transform.Find("Broken Shield Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }        
    }


    public void RemoveAllIcons() {
        foreach(Transform icon in iconsBar.transform) {
            GameObject.Destroy(icon.gameObject);
        } 
    }


    public void RenderStunIcon() {
        Instantiate(stunIcon, iconsBar.transform);
    }

    public void RemoveStunIcon() {
        Transform icon  = iconsBar.transform.Find("Stun Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderAttackUpIcon() {
        Instantiate(attackUpIcon, iconsBar.transform);
    }

    public void RemoveAttackUpIcon() {
        Transform icon  = iconsBar.transform.Find("Attack Up Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderAttackDownIcon() {
        Instantiate(attackDownIcon, iconsBar.transform);
    }

    public void RemoveAttackDownIcon() {
        Transform icon  = iconsBar.transform.Find("Attack Down Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderDodgeIcon() {
        Instantiate(dodgeIcon, iconsBar.transform);
    }

    public void RemoveDodgeIcon() {
        Transform icon = iconsBar.transform.Find("Dodge Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
        
    }

    public void RenderSmokeBombIcon() {
        Instantiate(smokeBombIcon, iconsBar.transform);
    }

    public void RemoveSmokeBombIcon() {
        Transform icon = iconsBar.transform.Find("Smoke Bomb Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderMysticShieldIcon() {
        Instantiate(mysticShieldIcon, iconsBar.transform);
    }

    public void RemoveMysticShieldIcon() {
        Transform icon = iconsBar.transform.Find("Mystic Shield Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderReflectIcon() {
        Instantiate(reflectIcon, iconsBar.transform);
    }

    public void RemoveReflectIcon() {
        Transform icon = iconsBar.transform.Find("Reflect Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }


   

}
