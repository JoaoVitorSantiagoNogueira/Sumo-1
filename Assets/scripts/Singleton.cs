using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    public static T instance {get; private set;}
 
    void Awake (){
        if (instance != null){
             Debug.LogError("A instance already exists");
             Destroy(gameObject); //Or GameObject as appropriate
             return;
        }
        else 
            {instance = (T)this;}
    }
}
