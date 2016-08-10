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
        shooter = new BulletShooter(b);
        setMaxTimer(1f);
        reset();
    }

    public BulletList(GameObject bullet = null, float timer = 1f, float speed = 1f)
    {
        setBullet(bullet);
        shooter = new BulletShooter(bullet);
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

    protected void setShooter()
    {
        if (bullet == null)
            throw new Exception("invalid: missing bullet");
        else
            shooter = new BulletShooter(bullet);
    }

    public void update(float t)
    {
        bulletTimer -= t;
    }

    public void reset()
    {
        if (getShooter() != null)
            getShooter().kill();
        else
            shooter = new BulletShooter(bullet);
    }

    public void setMaxTimer(float t)
    {
        maxTimer = t;
    }

    public void setBulletSpeed(float s)
    {
        bulletSpeed = s;
    }

    public void resetTimer()
    {
        bulletTimer = maxTimer;
    }

    //////////////////////////
    //
    //      FACILITATORS
    //
    //////////////////////////
    public bool shoot()
    {
        return shoot(ShotType.STRAIGHT, new Vector3(0, 0, 0), 0);
    }

    public bool shoot(Vector3 pos, float rotation)
    {
        return shoot(ShotType.STRAIGHT, pos, rotation);
    }

    public bool shoot(ShotType s, Vector3 pos, float rotation, int bulletNumber = 1, float spreadAngle = 10f)
    {
        if (getCurrentTimer() < 0)
        {
            if (getShooter() == null)
                setShooter();
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
            resetTimer();
            return true;
        }
        return false;
    }

}
