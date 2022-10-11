using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(playerMovementHandler))]
[RequireComponent (typeof(playerAnimator))]
public class playerController : MonoBehaviour
{
    int PlayerNumber;
    public float traction, accelerationSpeed, jumpStrength, speedCap;
    playerMovementHandler pmh;
    playerAnimator pa;


    enum states {                                                               // don't mess with state numbers, WILL break numstates
    standing, running, jumping, skipping, crouching, ducking, dashing,          //regular movement states
    flinching, tripping, skidding,                                              //vulnerable states
    swimming, diving,                                                           //off stage states
    J_winding, Landing, C_winding, A_winding, A_recovering,                     //wind-up and recovering states
    grappling, grappled,                                                        //grab states
    numstates                                                                   //total number of states
    }     

    playerStates[] stateOptions= new playerStates[(int) states.numstates-1];

    playerStates current;


    // Start is called before the first frame update
    void Start()
    {
        pmh =  gameObject.GetComponent(typeof(playerMovementHandler)) as playerMovementHandler;
        pa  =  gameObject.GetComponent(typeof(playerAnimator))        as playerAnimator;

        stateOptions[(int) states.standing] = new standingStateBehaviour(pmh, pa, this, traction, accelerationSpeed, jumpStrength, speedCap);
        stateOptions[(int) states.jumping]  = new jumpingStateBehaviour(pmh, pa, this);

        current = stateOptions[(int) states.jumping];

        inputController.instance.onSendPlayerDirections += setMovementInfo;
        inputController.instance.onSendJumpInput        += jump;
        playerEventHandler.instance.onChangeStateCommand+= changeState;
    }

    void Destroy()
    {
        inputController.instance.onSendPlayerDirections -= setMovementInfo;
        inputController.instance.onSendJumpInput        -= jump;
    }

    void Update()
    {
        current.AdvanceFrame();
    }

    public void setMovementInfo(float angle, float intensity)
    {
        current.directionGiven(angle, intensity);
    }

    public void jump()
    {
        current.ButtonPress("jump");
    }

    public Vector3 getBounds(string dir)
    {
        if (dir=="down")
            return transform.position+Vector3.down*GetComponent<MeshFilter>().sharedMesh.bounds.max.y* (transform.localScale.y);
        if (dir=="center")
            return transform.position;
        return Vector3.zero;
    }

    public void changeState(string stateName)
    {
        int newState = -1; //default wrong state aka, don't change anything

        if (stateName == "standing")  newState = (int) states.standing; ///if new state is standing, stand

        if (stateName == "jumping")   newState =(int) states.jumping;   ///if new state is jumping, jump

        if (newState!=-1)
        {
            stateCarryoverInfo sci;
            sci = current.deactivate();
            current = stateOptions [newState];
            current.activate(sci);
        }
    }
}
