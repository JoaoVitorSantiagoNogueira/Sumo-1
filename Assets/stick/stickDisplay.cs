using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stickDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    float centerX, centerY, angle, theta, ThetaMax, ThetaMin,sensibility, anglePieces;
    public bool angleClamp;

    public GameObject displayImage, backgroundObject;

    public float noAngles;

    Vector3 mouseI, mouseF;

    void Start()
    {
        centerX = transform.position.x; 
        centerY = transform.position.y;
        ThetaMax= 120.0f;
        ThetaMin=120.0f;
        angle = 0;
        mouseI= Input.mousePosition;
        sensibility=0.8f;
        anglePieces =(360/noAngles)/(180f/Mathf.PI);

        backgroundObject.transform.position = this.transform.position;
        displayImage.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouseF=Input.mousePosition;
        angle =Mathf.Atan2((mouseI-mouseF).x,(mouseF-mouseI).y);
        
        if (angleClamp)
            if ( Mathf.Abs(angle) % anglePieces<anglePieces/2.0f)
            {
                angle = angle - angle % anglePieces;
            }
            else{
                if(angle>0)
                    angle = angle + anglePieces - (angle % anglePieces);
                else   
                    angle = angle - anglePieces - (angle % anglePieces);
            }

        theta= Vector3.Distance(mouseI,mouseF)*sensibility;
        if(theta>ThetaMax)
            theta=ThetaMax;
        if(theta<ThetaMin)
            theta=0;
        
        displayImage.transform.position =  new Vector3 (-Mathf.Sin(angle)*theta*0.5f + centerX, Mathf.Cos(angle)*theta*0.5f +centerY, 0.0f);
        inputController.instance.sendPlayerDirections(angle* Mathf.Rad2Deg, theta/ThetaMax);

        if (Input.GetKeyDown("space"))
        {
            inputController.instance.sendJumpInput();
        }
    }
}
