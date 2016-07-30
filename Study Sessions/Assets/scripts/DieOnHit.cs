using UnityEngine;
using System.Collections;

public class DieOnHit : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Hi");
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.GetComponent<Graze>() == null)
            Destroy(gameObject);
    }
}
