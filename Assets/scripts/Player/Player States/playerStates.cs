using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface playerStates
{
    void AdvanceFrame();

    void activate(stateCarryoverInfo sci);
    stateCarryoverInfo deactivate();
    void ButtonPress(string s);
    void directionGiven(float angle, float theta);
    void interactWith(GameObject go,string type);
    Vector3 movement();

}
