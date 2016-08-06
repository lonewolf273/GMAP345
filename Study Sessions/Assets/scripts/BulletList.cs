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
<<<<<<< HEAD
        shooter = new BulletShooter(b);
=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
        setMaxTimer(1f);
        reset();
    }

    public BulletList(GameObject bullet = null, float timer = 1f, float speed = 1f)
    {
        setBullet(bullet);
<<<<<<< HEAD
        shooter = new BulletShooter(bullet);
=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
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

<<<<<<< HEAD
    protected void setShooter()
    {
        if (bullet == null)
            throw new Exception("invalid: missing bullet");
        else
            shooter = new BulletShooter(bullet);
    }

=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    public void update(float t)
    {
        bulletTimer -= t;
    }

    public void reset()
    {
<<<<<<< HEAD
        if (getShooter() != null)
            getShooter().kill();
        else
            shooter = new BulletShooter(bullet);
=======
        bulletTimer = maxTimer;
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    }

    public void setMaxTimer(float t)
    {
        maxTimer = t;
    }

    public void setBulletSpeed(float s)
    {
        bulletSpeed = s;
    }

<<<<<<< HEAD
    public void resetTimer()
    {
        bulletTimer = maxTimer;
    }

=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    //////////////////////////
    //
    //      FACILITATORS
    //
    //////////////////////////
    public void shoot()
    {
<<<<<<< HEAD
        shoot(ShotType.STRAIGHT, new Vector3(0, 0, 0), 0);
    }

    public void shoot(Vector3 pos, float rotation)
    {
        shoot(ShotType.STRAIGHT, pos, rotation);
    }

    public void shoot(ShotType s, Vector3 pos, float rotation, int bulletNumber = 1, float spreadAngle = 10f)
    {
        if (getShooter() == null)
            setShooter();
=======
        getShooter().shoot(getBulletSpeed());
    }
    public void shoot(Vector3 pos, float rotation)
    {
        getShooter().shoot(getBulletSpeed(), pos, rotation);
    }
    public void shoot(ShotType s, Vector3 pos, float rotation, int bulletNumber = 1, float spreadAngle = 10f)
    {
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
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
