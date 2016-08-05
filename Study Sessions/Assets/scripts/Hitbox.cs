using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {
    private Player p;

    void Start()
    {
        p = this.GetComponentInParent<Player>();
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        //Debug.Log(c.tag);
        if (c.tag == "EnemyBullet")
            p.damage(1);
        else
            Debug.Log("hi");
    }
}
