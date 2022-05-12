using UnityEngine;

[System.Serializable]
public class Mage : Player {


    public Mage(int baseAttack, double attackModifier, int baseShield, double shieldModifier) 
    : base(80, baseAttack, attackModifier, baseShield, shieldModifier) { 
        //empty
    }


  
}
