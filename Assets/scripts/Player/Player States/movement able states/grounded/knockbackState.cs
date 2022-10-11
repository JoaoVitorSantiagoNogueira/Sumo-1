using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knockbackState : movementAbleStates
{
    playerAnimator pa;
    playerController pc;

    float accelerationSpeed, speedCap, jumpStrength, traction;

    knockbackState (playerMovementHandler pmh, playerAnimator pa, playerController pc, float traction,
                    float accelerationSpeed, float jumpStrength, float speedCap) : base (pmh)
    {
        this.pa = pa;
        this.pc = pc;
        this.accelerationSpeed = accelerationSpeed;
        this.jumpStrength = jumpStrength;
        this.speedCap = speedCap;
        this.traction = traction;

        forces = new Vector3[3];
    }

    public override void ButtonPress(string button)
    {
        if (button == "jump")
            jump();
        return;
    }

    public override void activate(stateCarryoverInfo sci)
    {
        throw new System.NotImplementedException();
    }

    public override stateCarryoverInfo deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void AdvanceFrame()
    {
        base.AdvanceFrame();
    }

    public override void interactWith(GameObject go, string type)
    {
        throw new System.NotImplementedException();
    }

    void jump()
    {
        throw new System.NotImplementedException();
    }
}
