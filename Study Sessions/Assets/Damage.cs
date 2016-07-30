using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "EnemyBullet")
            Destroy(this.transform.parent.gameObject);
        else
            Debug.Log("hi");
    }
}
