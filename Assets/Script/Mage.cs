using UnityEngine;

[System.Serializable]
public class Mage : Player {


    public Mage(int baseAttack, int attackModifier, int baseShield, int shieldModifier) 
    : base(80, baseAttack, attackModifier, baseShield, shieldModifier) { 
        //empty
    }


  
}
