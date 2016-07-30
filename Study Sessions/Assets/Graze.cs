using UnityEngine;
using System.Collections;

public class Graze : MonoBehaviour {
    private int count;
	// Use this for initialization
	void Start () {
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerStay2D(Collider2D c) //checks the bullets
    {
        count += 1;
        Debug.Log(count.ToString());
    }
}
