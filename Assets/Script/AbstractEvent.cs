using UnityEngine;

abstract class AbstractEvent {

    /*
    1) OverTime damage event -- Depends on who is the target
    2) Add mana
    */
    public int numberOfTurns;

    public AbstractEvent(int numberOfTurns) {
        this.numberOfTurns = numberOfTurns;
    }

    public abstract void executeEvent();


    


}