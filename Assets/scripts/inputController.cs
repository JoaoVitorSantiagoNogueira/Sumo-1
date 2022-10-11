using System;

public class inputController : Singleton <inputController>
{
    public event Action <float, float> onSendPlayerDirections;
    public void sendPlayerDirections(float angle, float intensity) //tells ray tracer to trace and find object
    {
        if (onSendPlayerDirections!=null)
        {
            onSendPlayerDirections(angle, intensity);
        }
    }

    public event Action onSendJumpInput;
    public void sendJumpInput()
    {
        if (onSendJumpInput != null)
        {
            onSendJumpInput();
        }
    }

}