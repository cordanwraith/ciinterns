using UnityEngine;
using System.Collections;

public class SingletonDemo : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        SingletonExample.GetInstance().DoSomethingWithState();
	}
}
