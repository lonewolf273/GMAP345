using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMover : MonoBehaviour {

    public float speed;
    private Rigidbody2D _rb;
    protected Dictionary<string, KeyCode> controls =
        new Dictionary<string, KeyCode>()
    {
            {"up", KeyCode.W},
            {"down", KeyCode.S},
            {"left", KeyCode.A},
            {"right", KeyCode.D},
            {"focus", KeyCode.RightShift},
    };

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 newVel;
        float s = speed;

        if (Input.GetKey(controls["focus"]))
            s *= 0.4f;

        if (Input.GetKey(controls["up"]))
            newVel.y = s;
        else if (Input.GetKey(controls["down"]))
            newVel.y = -s;
        else newVel.y = 0;

        if (Input.GetKey(controls["left"]))
            newVel.x = -s;
        else if (Input.GetKey(controls["right"]))
            newVel.x = s;
        else newVel.x = 0;

        _rb.velocity = newVel;
	}
}
