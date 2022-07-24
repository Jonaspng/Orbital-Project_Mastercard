using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private Slider hpSlider;

    [SerializeField] private GameObject iconsBar;

    [SerializeField] private GameObject prewarnIndicator;

    [SerializeField] private GameObject shieldIcon;

    [SerializeField] private GameObject shieldAllIcon;

    [SerializeField] private GameObject poisonIcon;

    [SerializeField] private GameObject brokenShieldIcon;

    [SerializeField] private GameObject burnedIcon;

    [SerializeField] private GameObject stunIcon;

    [SerializeField] private GameObject attackIndicator;

    [SerializeField] private GameObject attackUpIcon;

    [SerializeField] private GameObject allAttackUpIcon;

    [SerializeField] private GameObject attackDownIcon;

    [SerializeField] private GameObject dodgeIcon;

    [SerializeField] private GameObject smokeBombIcon;

    [SerializeField] private GameObject mysticShieldIcon;

    [SerializeField] private GameObject reflectIcon;

    [SerializeField] private GameObject EndureIcon;

    [SerializeField] private GameObject SummonIcon;

    [SerializeField] private bool isShieldIconOn;

    [SerializeField] private bool isBrokenShieldIconOn;

    [SerializeField] private int maxHp;

    public void SetHUD(Unit unit) {
        nameText.text = unit.GetUnitName();
        hpSlider.maxValue = unit.GetMaxHp();
        this.maxHp = unit.GetMaxHp();
        hpSlider.value = unit.GetHealth();
        this.transform.Find("Health bar").GetComponentInChildren<TextMeshProUGUI>().text = unit.GetHealth() + "/" + unit.GetMaxHp();
    }

    public void SetHP(int hp) {
        hpSlider.value = hp;
        this.transform.Find("Health bar").GetComponentInChildren<TextMeshProUGUI>().text = hp + "/" + maxHp;
    }

    public void RenderPlayerShieldIcon(int shield) {
        int baseShield = StageManager.GetInstance().GetPlayer().GetBaseShield();
        if (iconsBar.transform.Find("Shield Icon(Clone)") == null) {
            GameObject icon = Instantiate(shieldIcon, iconsBar.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().text = shield.ToString();
        } else {
            iconsBar.transform.Find("Shield Icon(Clone)").GetComponentInChildren<TextMeshProUGUI>().text = (baseShield + shield).ToString();
        }
    }

    public void RenderEnemyShieldIcon(int enemyIndex) {
        int baseShield = StageManager.GetInstance().GetEnemies()[enemyIndex].GetBaseShield();
        if (iconsBar.transform.Find("Shield Icon(Clone)") == null) {
            GameObject icon = Instantiate(shieldIcon, iconsBar.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().text = baseShield.ToString();
        } else {
            iconsBar.transform.Find("Shield Icon(Clone)").GetComponentInChildren<TextMeshProUGUI>().text = (baseShield).ToString();
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
        if (!isBrokenShieldIconOn) {
            Instantiate(brokenShieldIcon, iconsBar.transform);
            isBrokenShieldIconOn = true;
        }
        
    }

    public void RemoveBrokenIcon() {
        Transform icon  = iconsBar.transform.Find("Broken Shield Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
            isBrokenShieldIconOn = false;
        }        
    }


    public void RemoveAllIcons() {
        isBrokenShieldIconOn = false;
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

    public void RenderEndureIcon() {
        Instantiate(EndureIcon, iconsBar.transform);
    }

    public void RemoveEndureIcon() {
        Transform icon = iconsBar.transform.Find("Endure Icon(Clone)");
        if (icon != null) {
            GameObject.Destroy(icon.gameObject);
        }
    }

    public void RenderAttackIndicator() {
        GameObject attack = Instantiate(attackIndicator, prewarnIndicator.transform);
        attack.GetComponentInChildren<TextMeshProUGUI>().text = this.gameObject.GetComponentInChildren<Enemy>().GetFullDamage().ToString();
    }

    public void RenderShieldIndicator() {
        GameObject shield = Instantiate(shieldIcon, prewarnIndicator.transform);      
        shield.GetComponentInChildren<TextMeshProUGUI>().text = "6";
    }

    public void RenderBrokenIndicator() {
        GameObject broken = Instantiate(brokenShieldIcon, prewarnIndicator.transform);
        broken.GetComponentInChildren<TextMeshProUGUI>().text = this.gameObject.GetComponentInChildren<Enemy>().GetFullDamage().ToString();
    }

    public void RenderShieldAllIndicator() {
        GameObject shield = Instantiate(shieldAllIcon, prewarnIndicator.transform);
        shield.GetComponentInChildren<TextMeshProUGUI>().text = "6";
    }

    public void RenderAttackUpAllIndicator() {
        Instantiate(allAttackUpIcon, prewarnIndicator.transform);
    }

    public void RenderSummonIndicator() {
        Instantiate(SummonIcon, prewarnIndicator.transform);
    }

    public void RemoveIndicator() {
        foreach(Transform indicator in prewarnIndicator.transform) {
            Destroy(indicator.gameObject);
        }
    }

}
