using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class BulletList
{
    public enum ShotType
    {
        STRAIGHT,
        CIRCLE,
        SPREAD
    }

    public GameObject bullet;

    [SerializeField] protected BulletShooter shooter;

    private float bulletTimer;

    [SerializeField]
    protected float maxTimer;

    [SerializeField]
    protected float bulletSpeed;

    //////////////////////////
    //
    //      CONSTRUCTORS
    //
    //////////////////////////

    public BulletList(GameObject b = null)
    {
        setBullet(b);
        setMaxTimer(1f);
        reset();
    }

    public BulletList(GameObject bullet = null, float timer = 1f, float speed = 1f)
    {
        setBullet(bullet);
        setMaxTimer(timer);
        reset();
        setBulletSpeed(speed);
    }

    //////////////////////////
    //
    //      INSPECTORS
    //
    //////////////////////////

    public GameObject getBullet() { return bullet; }

    public BulletShooter getShooter() { return shooter; }

    public float getCurrentTimer() { return bulletTimer; }

    public float getMaxTimer() { return maxTimer; }

    public bool checkAlarm() { return bulletTimer <= 0f; }

    public float getBulletSpeed() { return bulletSpeed; }

    //////////////////////////
    //
    //      MUTATORS
    //
    //////////////////////////
    public void setBullet(GameObject b)
    {
        bullet = b;
        shooter = new BulletShooter(b);
    }

    public void update(float t)
    {
        bulletTimer -= t;
    }

    public void reset()
    {
        bulletTimer = maxTimer;
    }

    public void setMaxTimer(float t)
    {
        maxTimer = t;
    }

    public void setBulletSpeed(float s)
    {
        bulletSpeed = s;
    }

    //////////////////////////
    //
    //      FACILITATORS
    //
    //////////////////////////
    public void shoot()
    {
        getShooter().shoot(getBulletSpeed());
    }
    public void shoot(Vector3 pos, float rotation)
    {
        getShooter().shoot(getBulletSpeed(), pos, rotation);
    }
    public void shoot(ShotType s, Vector3 pos, float rotation, int bulletNumber = 1, float spreadAngle = 10f)
    {
        switch (s)
        {
            case ShotType.STRAIGHT:
                getShooter().shoot(getBulletSpeed(), pos, rotation);
                break;
            case ShotType.CIRCLE:
                getShooter().shootCircle(getBulletSpeed(), pos, bulletNumber, (int)rotation);
                break;
            default:
                getShooter().shoot(getBulletSpeed(), pos, rotation);
                break;
        }
    }
}
