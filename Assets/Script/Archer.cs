using UnityEngine;

[System.Serializable]
public class Archer : Player {
 

    public Archer(int baseAttack, int attackModifier, int baseShield, int shieldModifier) 
    : base(70, baseAttack, attackModifier, baseShield, shieldModifier) { 
        //empty
    }


  
}
