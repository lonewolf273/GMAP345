using UnityEngine;
using System.Collections;

public class PlayerShooter : MonoBehaviour {
    private int timer = -1;
    private int TIMER_FULL = 5;
    public float bulletSpeed = 25f;
    public GameObject bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.Return) && timer < 0f)
        {
            GameObject a = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            a.GetComponent<Bullet>().changeSpeed(bulletSpeed);
            Destroy(a, 1.5f);
            timer = TIMER_FULL;
        }

        timer--;
	}
}
