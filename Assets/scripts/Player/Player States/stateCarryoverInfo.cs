using UnityEngine;

public class stateCarryoverInfo
{   public Vector3 momentum;

    public stateCarryoverInfo(Vector3 m)
    {
        momentum = m;
    }
    public void setMomentum(Vector3 m)
    {
        momentum = m;
    }
    public Vector3 getMomentum()
    {
        return momentum;
    }
}
