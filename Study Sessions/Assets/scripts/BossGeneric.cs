using UnityEngine;
using System.Collections;

public class BossGeneric : MonoBehaviour {

    [SerializeField] protected int healthMax = 100;
    int health;
    [SerializeField] protected GameObject healthbar;
    [SerializeField] BossAttackGeneric boss;

    enum State
    {
        ALIVE,
        DEAD
    }
    float deathTimer = 2f;

    State status = State.ALIVE; //start with being alive
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
        if (status == State.DEAD)
        {
            deathTimer -= Time.deltaTime;
            transform.localScale = new Vector3(deathTimer/2f, deathTimer/2f, 0);
        }

        if (deathTimer < 0f)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (status == State.ALIVE)
        {
            if (c.tag == "Bullet")
                damage();
        }
    }

    void damage()
    {
        health--;

        if (health <= 0)
        {
            die();
        }
    }

    public void die()
    {
        GetComponent<BossAttackGeneric>().kill();
        status = State.DEAD;
    }

    public float getHeatlhPercent()
    {
        return (float)health / healthMax;
    }
}
