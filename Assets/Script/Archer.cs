using UnityEngine;

[System.Serializable]
public class Archer : Player {
 

    public Archer(int baseAttack, double attackModifier, int baseShield, double shieldModifier) 
    : base(70, baseAttack, attackModifier, baseShield, shieldModifier) { 
        //empty
    }


  
}
