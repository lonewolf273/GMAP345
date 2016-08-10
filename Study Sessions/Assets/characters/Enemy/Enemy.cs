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
    protected List<GameObject> bulletList;

    protected List<BulletShooter> bulletShooter;

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
        if (bulletList.Count <= i) return null;
        return bulletList[i];
    }
    public int findBullet(GameObject b)
    {
        for (var i = 0; i < bulletList.Count; i++)
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

        bulletShooter.Add(new BulletShooter(b));
        bulletList.Add(b);
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

    /*
     * Facilitators
    */
    abstract public void move();
    abstract public void attack();
    abstract public void die();
    abstract public void ready();

}
