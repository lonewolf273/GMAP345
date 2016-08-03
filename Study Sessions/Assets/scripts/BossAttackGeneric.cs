using UnityEngine;
using System.Collections;

public class BossAttackGeneric : MonoBehaviour {

    private float timer = -1f;
    public float TIMER_FULL = 2f;
    public float bulletSpeed = 10f;
    private int offset = 0;

    public GameObject bullet;

    private BulletShooter shooter;

    // Use this for initialization
    void Start()
    {
        shooter = new BulletShooter(bullet);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0f)
        {

            shooter.shootCircle(bulletSpeed, transform.position, 15, offset);
            offset += 15;

            timer = TIMER_FULL;
        }

        timer -= Time.deltaTime;
    }
}
