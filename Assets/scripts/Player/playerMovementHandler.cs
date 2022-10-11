using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class playerMovementHandler : MonoBehaviour
{
    //carry over everything and break the undesired speed away
    public Vector3 calcCarryOverSpeed(Vector3 inputDirection, Vector3 previous, float traction, bool moving)
    {
        Vector3 breakSpeed, undesiredSpeed;
        float tractionLoss = Mathf.Cos(traction*90*Mathf.Deg2Rad); //multiplier used by things thar are lessend by tration

        previous.y = 0f;

        if (Vector3.Dot(previous, inputDirection)<=0||!moving)
        undesiredSpeed = previous;
        else
        undesiredSpeed = previous - Vector3.Project(previous, inputDirection);

        breakSpeed = undesiredSpeed - groundDrag(undesiredSpeed, traction, 0.2f);

        Debug.DrawRay(transform.position, breakSpeed);

        return previous-breakSpeed;
    }

    public Vector3 calcAcceleration (float intensity, float accSpeed, float traction, float buildUp, Vector3 inputDirection)
    {
        return accSpeed  * intensity  * traction * inputDirection *Time.deltaTime * (buildUp+0.1f);
    }

    public Vector3 airDrag(Vector3 speed, float speedCap, float acceleration) //time2stop determined empirically, bigger = longer
    {
        Vector3 ignoreY = new Vector3 (speed.x, 0f, speed.z); ///ignore y speed for calculations
        float bias = 3;

        if (ignoreY.magnitude<0.0003*Time.deltaTime)
        return -ignoreY;
  
        
        float deaccelerationRate = (float) Math.Pow(Math.Pow(10,(ignoreY.magnitude/speedCap)-1), bias); //normalized deacceleraton in relation to max speed
        deaccelerationRate *= acceleration;                                           //apply to propper acceleration                                      
        return -ignoreY.normalized*deaccelerationRate;                                //return a force in the direction opposite to the speed

    }

    public Vector3 groundDrag(Vector3 speed, float traction, float time2Stop) //time2stop determined empirically, bigger = longer
    {
        return speed*(time2Stop - traction*Time.deltaTime)/time2Stop; 
    }

    public Vector3 calcGravityPullSpeed (Vector3 gravityPullSpeed)
    {
        float slowDownStart=3f;
        float slowDownEnd  =2f;
        float fallAccelerationRegular = 1f;
        float fallAccelerationReduced = 0.2f;
        float t;


        if (gravityPullSpeed.magnitude>slowDownStart) //normal fall speed
        return gravityPullSpeed + Vector3.down*fallAccelerationRegular;

        else if (gravityPullSpeed.magnitude<slowDownEnd)//floatiness at the apex of the jump
        return gravityPullSpeed + Vector3.down*fallAccelerationReduced;

        else
        {
        t = (gravityPullSpeed.magnitude-slowDownEnd)/slowDownStart;//transition
        return gravityPullSpeed + Vector3.down*(fallAccelerationRegular*t + fallAccelerationReduced*(1-t));
        }
    }

    public void move (Vector3 movementDirection)
    {
        transform.position += movementDirection; //uptades position
    }

    public void reorient(Vector3 facingDirection)
    {
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, facingDirection); //makes player face the right way
    }

    public Vector3 jump(float jumpStrength)
    {
        return Vector3.up*jumpStrength ;
    }
}