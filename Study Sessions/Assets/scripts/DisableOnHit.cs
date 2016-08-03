using UnityEngine;
using System.Collections;

public class DisableOnHit : MonoBehaviour {

    // For disabling bullets and not killing them


    void OnCollisionEnter2D(Collision2D c)
    {
        Debug.Log("Hi");
        disable();
    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.GetComponent<Graze>() == null)
            disable();
        if (c.GetComponent<Damage>() != null)
            disable();
    }

    void disable()
    {
        gameObject.SetActive(false);
    }
}
