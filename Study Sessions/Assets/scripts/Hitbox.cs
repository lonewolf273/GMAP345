using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {
    private Player p;
    private SpriteRenderer s;
    void Start()
    {
        p = this.GetComponentInParent<Player>();
        s = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (p.getFocus())
            setOpacity(1f);
        else
            setOpacity(0f);
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }

    void setOpacity(float o)
    {
        float diff = s.color.a - o;
        if (diff < 0f)
        {
            if (diff < -0.1f)
                diff = -0.1f;
        }
        else if (diff > 0f)
        {
            if (diff > 0.1f)
                diff = 0.1f;
        }
        s.color = new Color(1, 1, 1, s.color.a - diff);
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
