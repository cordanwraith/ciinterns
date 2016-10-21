using UnityEngine;
using System.Collections;

public class SingletonExample : MonoBehaviour {

    protected static SingletonExample instance;

    private int someValue = 0;

    public static SingletonExample GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void DoSomethingWithState()
    {
        //Debug.Log(someValue++);
    }
}
