using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //used for ordering

public class collisionChecker : Singleton <collisionChecker>
{
    static public bool checkForCollision( Vector3 origin, Vector3 direction, float maxDistance, string tag, out RaycastHit Result) //given a distance, origin point, direction and desired tag return object if it was hit
    {
        
        Ray ray = new Ray (origin, direction);

        Result = new RaycastHit();

        RaycastHit[] objects;

        objects = Physics.RaycastAll(ray, maxDistance).OrderBy(h=>h.distance).ToArray();
        if (objects!= null)
            for (int i = 0; i < objects.Length ; i++)
            {
                if (objects[i].collider.tag == tag)
                {
                    Result = objects [i];
                    return true;
                }
            }
        return false;
    }
}
