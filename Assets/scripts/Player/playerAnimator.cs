using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimator : MonoBehaviour
{
    Renderer colorRenderer;

    void Start()
    {
        colorRenderer = gameObject.GetComponent<Renderer>();
    }


    public void jumpAnimation()
    {
        colorRenderer.material.color = Color.blue;
    }

    public void runningAnimation()
    {
        colorRenderer.material.color = Color.white;
    }

}
