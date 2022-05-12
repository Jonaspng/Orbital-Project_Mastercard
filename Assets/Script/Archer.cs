using UnityEngine;

[System.Serializable]
public class Archer : Player {

    public int evasionCount; 

    public Archer(int baseAttack, double attackModifier, 
    int baseShield, double shieldModifier, int evasionCount) 
    : base(70, baseAttack, attackModifier, baseShield, shieldModifier) { 
        this.evasionCount = evasionCount;
    }

    public override void changeEvasionCount(int evasionCount) {
        this.evasionCount = evasionCount;
    }


  
}
