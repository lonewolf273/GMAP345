using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;
    private Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
        _rb = (Rigidbody2D)gameObject.GetComponent(typeof(Rigidbody2D));
	}
	
	// Update is called once per frame
	void Update () {
        _rb.velocity = getSpeed().normalized * speed;
    }
    
    Vector2 getSpeed()
    {
        return new Vector2(Mathf.Sin(transform.rotation.x), Mathf.Cos(transform.rotation.x));
    }

    public void changeSpeed(float s)
    {
        speed = s;
    }
}
