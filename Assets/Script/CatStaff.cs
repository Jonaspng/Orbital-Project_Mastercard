using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStaff : Enemy {
    public CatStaff(int baseAttack, double attackModifier, int baseShield, double shieldModifier)
    : base(30, baseAttack, attackModifier, baseShield, shieldModifier) {
        
    }
}
