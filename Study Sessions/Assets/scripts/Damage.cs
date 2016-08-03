using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
    private PlayerStats p;

    void Start()
    {
        p = this.GetComponentInParent<PlayerStats>();
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log(c.tag);
        if (c.tag == "EnemyBullet")
            p.damage();
        else
            Debug.Log("hi");
    }
}
