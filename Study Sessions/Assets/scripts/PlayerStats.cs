using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    private int health;
    public int healthMax;
    private float invunTimer = -1f;
    public const float INVUN_TIMER = 3f;
    // Use this for initialization
	void Start () {
        health = healthMax;
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void damage()
    {
        if (health == 0)
            Destroy(gameObject);
        health -= 1;
    }
    public float getHeatlhPercent()
    {
        return (float)health / healthMax;
    }
}
