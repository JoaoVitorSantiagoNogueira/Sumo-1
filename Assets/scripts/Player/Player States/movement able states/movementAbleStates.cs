using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class movementAbleStates : playerStates
{
    protected playerMovementHandler pmh;
    protected Vector3 resulting = Vector3.zero, carryOverSpeed = Vector3.zero, inputDirection, facingDirection;
    protected Vector3[] forces;
    protected float intensity, buildUp = 0;

    public movementAbleStates(playerMovementHandler pmh)
    {
        this.pmh = pmh;
    }

    public virtual void AdvanceFrame()
    {
        resulting = sum(forces);

        facingDirection = inputDirection;
        pmh.reorient (facingDirection);               //reorients player to look in the right direction
        pmh.move(resulting * Time.deltaTime);         //move player in direction
    }

    Vector3 sum(Vector3[] vectors)
    {
        Vector3 r= Vector3.zero;
        for (int i=0; i<vectors.Length;i++)
        {
            r += vectors[i];
        }
        return r;
    }

    public abstract void activate(stateCarryoverInfo sci);
    public abstract void ButtonPress(string s);
    public void directionGiven(float angle, float intensity)
    {
        inputDirection = Quaternion.Euler(0,-angle,0)*Vector3.forward;
        this.intensity = intensity;
        buildUp +=1*Time.deltaTime;
    }
    public abstract void interactWith(GameObject go,string type);

    public abstract stateCarryoverInfo deactivate();
    public Vector3 movement()
    {
        return resulting;
    }
}
