using System;
using UnityEngine;

public class playerEventHandler : Singleton<playerEventHandler>
{
    public event Action <String> onChangeStateCommand;
    public void changeStateCommand(String stateName) //tells which state should be assumed
    {
        if (onChangeStateCommand!=null)
        {
            onChangeStateCommand(stateName);
        }
    }
}
