using UnityEngine;
using System.Collections;

public class EighthNote : Bullet {
    [SerializeField]
    private float rotationSpeed; //rotationspeed in angle amount per second
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, rotationSpeed);
	}
}
