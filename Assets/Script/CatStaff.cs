using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStaff : Enemy {
    public CatStaff(double attackModifier, double shieldModifier)
    : base(30, attackModifier, shieldModifier) {
        
    }
}
