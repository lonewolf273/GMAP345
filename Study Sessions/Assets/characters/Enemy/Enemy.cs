using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
        ENEMY CLASS:
            - Abstract Class
            - Properties
                1.  bullet - the list of bullets that you can shoot
                2.  bulletShooter - the list of bullet shooters that the bullets are being shot from
                3.  moveSpeed - movement speed
                4.  bulletSpeed - how fast bullets shoot
                5.  hp - current health
            - Methods
*/
public abstract class Enemy : MonoBehaviour {

    protected enum State
    {
        READY,
        ALIVE,
        DEAD
    };

    protected State status;

    [SerializeField]
    protected float moveSpeed;


    [SerializeField]
    protected float hpMax; //Maximum HP

    [SerializeField]
    protected List<BulletList> bullets;

    protected float hp; //health




    
    /*
     *  Inspectors
     */

    protected BulletList getBulletList(int i)
    {
        if (i < 0 || i >= bullets.Count)
            throw new System.Exception("Err: " + i.ToString() + " out of range");
        return bullets[i];
    }

    protected BulletList getBulletList(GameObject b)
    {
        return getBulletList(findBullet(b));
    }
    public float getHealthPercent()
    {
        return getHp() / getHpMax();
    }

    public float getMoveSpeed()
    {
        return moveSpeed;
    }
    public float getBulletSpeed(int i = 0)
    {
        return getBulletList(i).getBulletSpeed();
    }
    public float getHp()
    {
        return hp;
    }
    public float getHpMax()
    {
        return hpMax;
    }
    public GameObject getBulletAt(int i)
    {
        if (bullets.Count <= i) return null;
        return bullets[i].getBullet();
    }
    public int findBullet(GameObject b)
    {
        for (var i = 0; i < bullets.Count; i++)
        {
            if (b == getBulletList(i).getBullet())
                return i;
        }
        return -1;
    }

    protected State getStatus()
    {
        return status;
    }
    public bool aliveCheck()
    {
        return status != State.DEAD;
    }
    public float getHeatlhPercent()
    {
        return getHp() / getHpMax();
    }
    /*
     * Mutators
     */
    public void setMoveSpeed(float m)
    {
        moveSpeed = Mathf.Abs(m);
    }

    public void setBulletSpeed(int i, float s)
    {
        bullets[i].setBulletSpeed(s);
    }

    public void damage(float d)
    {
        hp -= d;
        if (hp > hpMax) hp = hpMax;
        if (hp <= 0) die();
    }

    public void reset()
    {
        hp = hpMax;
        resetShooters();
    }

    public void addBullet(GameObject b)
    {
        if (findBullet(b) >= 0) return;
        bullets.Add(new BulletList(b, 1, 1));
    }
    protected void resetShooters()
    {
        if (bullets.Count > 0)
            foreach (var b in bullets)
                b.reset();
    }
    protected void setStatus(State s)
    {
        status = s;
    }
    protected void resetAllBullets()
    {
        foreach (var i in bullets)
        {
            i.reset();
        }
    }

    protected void updateAll()
    {
        foreach (var i in bullets)
        {
            i.update(Time.deltaTime);
        }
    }
    /*
     * Facilitators
    */
    abstract public void move();
    abstract public void attack();
    abstract public void die();
    abstract public void ready();

}
