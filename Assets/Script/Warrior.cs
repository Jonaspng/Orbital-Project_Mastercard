using UnityEngine;

[System.Serializable]
public class Warrior : Player {


    public Warrior(int baseAttack, double attackModifier, int baseShield, double shieldModifier) 
    : base(100, baseAttack, attackModifier, baseShield, shieldModifier) { 
        //empty
    }


  
}
