using UnityEngine;

[System.Serializable]
public class Warrior : Player {


    public Warrior(int baseAttack, int attackModifier, int baseShield, int shieldModifier) 
    : base(100, baseAttack, attackModifier, baseShield, shieldModifier) { 
        //empty
    }


  
}
