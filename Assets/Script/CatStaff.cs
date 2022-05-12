using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStaff : Enemy {
    public CatStaff(int baseAttack, int attackModifier, int baseShield, int shieldModifier)
    : base(30, baseAttack, attackModifier, baseShield, shieldModifier) {
        
    }
}
