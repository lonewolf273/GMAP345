using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

abstract public class Boss : Enemy {

    /////////////////////////////
    //
    //     Unique Variables
    //
    /////////////////////////////

    [SerializeField] protected List<float> bulletTimers;
    [SerializeField] protected List<float> maxTimers;
    [SerializeField] protected GameObject healthbar;
    [SerializeField] protected GameObject hud;

    protected Rigidbody2D _rb;


    ///////////////////////////////////////
    //
    //       DEFAULT UNITY FUNCTIONS
    //
    ///////////////////////////////////////

    void Start()
    {

        hp = hpMax;
        bulletShooter = new List<BulletShooter>();
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = gameObject.AddComponent<Rigidbody2D>();

        reset();
        setStatus(State.ALIVE);

    }

    void Update()
    {
        attack();
        move();
    }

    /////////////////////////////
    //
    //        INSPECTORS
    //
    /////////////////////////////
    public float getCurrentTimerAt(int i)
    {
        if (i >= bulletTimers.Count || i < 0)
            throw new Exception("invalid bullet timer");
        return bulletTimers[i];
    }
    public float getCurrentTimer(GameObject bullet)
    {
        int i = findBullet(bullet);
        if (i < 0)
            throw new Exception("invalid bullet timer");
        return getCurrentTimerAt(findBullet(bullet));
    }
     
    /////////////////////////////
    //
    //    PROTECTED METHODS
    //
    /////////////////////////////

    protected void startHealthbar(bool createhud = false)
    {
        hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud == null)
        {
            hud = (GameObject)Instantiate(hud, Vector3.up, Quaternion.identity);
        }
        if (hud != null)
        {
            GameObject a = (GameObject)Instantiate(healthbar, Vector3.up, Quaternion.identity);
            a.GetComponent<BossHealthbar>().boss = this.gameObject;
            a.transform.SetParent(hud.transform);
        }
    }
    /////////////////////////////
    //
    //     OVERRIDES
    //
    /////////////////////////////

    abstract public override void attack();

    public override void die()
    {
        setStatus(State.DEAD);
        reset();
        afterLife();
    }

    abstract protected void afterLife();

    abstract public override void move();

    abstract public override void ready();

}
