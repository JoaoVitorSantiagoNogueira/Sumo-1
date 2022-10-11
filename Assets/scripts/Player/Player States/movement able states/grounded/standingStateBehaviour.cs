using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class standingStateBehaviour : movementAbleStates
{
    RaycastHit a;
    hitbox hb;
    playerAnimator pa;
    playerController pc;

    float traction, accelerationSpeed, speedCap;

    Vector3 pushSpeed = Vector3.zero, acceleration = Vector3.zero;
    
    public standingStateBehaviour(playerMovementHandler pmh, playerAnimator pa, playerController pc, float traction, float accelerationSpeed, float jumpStrength, float speedCap): base(pmh)
    {
        this.pa = pa;
        this.pc = pc;

        this.traction = traction;
        this.accelerationSpeed = accelerationSpeed;
        this.speedCap = speedCap;

        forces = new Vector3[2];
    }

    public override void AdvanceFrame()
    {
        carryOverSpeed         =    pmh.calcCarryOverSpeed(inputDirection, resulting, traction, (intensity!=0));
        acceleration           =    pmh.calcAcceleration(intensity, accelerationSpeed, traction, buildUp, inputDirection);
    
        if (collisionChecker.checkForCollision(pc.getBounds("center"), resulting, resulting.magnitude*Time.deltaTime, "Hitbox", out a))
        {
            //current=states.standing; knock back
            //pa.runningAnimation();  knock back animation
            hb = a.transform.gameObject.GetComponent(typeof(hitbox)) as hitbox;
            pushSpeed = Quaternion.Euler(0,hb.ground_theta,0)*Vector3.right*hb.strength;
            resulting = pushSpeed;
        }
    
        if (intensity==0)
        {
            buildUp = 0;
        }

        //resulting += pmh.airDrag (resulting, speedCap, acceleration.magnitude);

        forces[0]= carryOverSpeed;
        forces[1]= acceleration;
        base.AdvanceFrame(); //resulting speed equals the sum of all other speed
    }

    public override void activate(stateCarryoverInfo sci)
    {
        pa.runningAnimation();
        carryOverSpeed = sci.getMomentum();
    }

    public override stateCarryoverInfo deactivate()
    {
        stateCarryoverInfo sci = new stateCarryoverInfo(resulting);
        return sci;
    }

    void jump()
    {
        stateCarryoverInfo sc = new stateCarryoverInfo(resulting);
        playerEventHandler.instance.changeStateCommand("jumping");
    }

    public override void ButtonPress(string s)
    {
        if (s=="jump")
            jump();
        return;
    }

    public override void interactWith(GameObject go,string type) {}
}
