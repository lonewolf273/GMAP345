using UnityEngine;
using System.Collections;

public class BossGeneric : MonoBehaviour {
    private int healthMax = 100;
    public int health;
    public GameObject healthbar;
	// Use this for initialization
	void Start () {
        health = healthMax;
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud != null)
        {
            GameObject a = (GameObject)Instantiate(healthbar, Vector3.up, Quaternion.identity);
            //Debug.Log(a.name);
            a.GetComponent<BossHealthbar>().boss = this.gameObject;
            a.transform.SetParent(hud.transform);
        }
        //else Debug.Log("what");

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Health : " + health.ToString());
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Bullet")
            damage();
    }

    void damage()
    {
        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public float getHeatlhPercent()
    {
        return (float)health / healthMax;
    }
}
