using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooter : MonoBehaviour {
    private int timer = -1;
    private int TIMER_FULL = 5;
    public float bulletSpeed;

    public GameObject bullet;

    private BulletShooter shooter;

	// Use this for initialization
	void Start () {
        shooter = new BulletShooter(bullet);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Return) && timer < 0f)
        {

            shooter.shoot(bulletSpeed, transform.position, 0);

            timer = TIMER_FULL;
        }

        timer--;
	}
}
