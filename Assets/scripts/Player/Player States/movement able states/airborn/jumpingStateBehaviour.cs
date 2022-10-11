using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingStateBehaviour : movementAbleStates
{
    float jumpStrength = 30;
    RaycastHit a;
    Vector3 gravityPullSpeed = Vector3.zero;

    playerAnimator pa;
    playerController pc;

    public jumpingStateBehaviour(playerMovementHandler pmh, playerAnimator pa, playerController pc): base(pmh)
    {
        this.pa = pa;
        this.pc = pc;
        forces = new Vector3[2];
    }


    public override void AdvanceFrame()
    {
        gravityPullSpeed = pmh.calcGravityPullSpeed(gravityPullSpeed);
        if (collisionChecker.checkForCollision(pc.getBounds("down"), gravityPullSpeed, gravityPullSpeed.magnitude*Time.deltaTime, "Ground", out a))
        {   
            gravityPullSpeed = Vector3.down * a.distance/Time.deltaTime;
            playerEventHandler.instance.changeStateCommand("standing");
        }
        forces[0] = gravityPullSpeed;
        forces[1] = carryOverSpeed;
        base.AdvanceFrame();//resulting speed equals the sum of all other speeds 
    }

    public override void activate(stateCarryoverInfo sci)
    {
        carryOverSpeed = sci.momentum;
        gravityPullSpeed = pmh.jump(jumpStrength);
        pa.jumpAnimation();
    }

    public override stateCarryoverInfo deactivate()
    {
        stateCarryoverInfo sci = new stateCarryoverInfo(resulting);
        resulting = Vector3.zero;
        return sci;
    }

    public override void ButtonPress(string s)
    {
        return;
    }

    public override void interactWith(GameObject go,string type)
    {

    }
}
