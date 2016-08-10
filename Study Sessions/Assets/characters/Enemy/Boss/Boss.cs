using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

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

    void OnTriggerEnter2D(Collider2D c)
    { 
        if (status == State.ALIVE)
        {
            Debug.Log("hi");
            if (c.tag == "Bullet")
                damage(1);
        }
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

    public Rigidbody2D getRigidBody()
    {
        return _rb;
    }

    public Vector2 getVelocity()
    {
        return getRigidBody().velocity;
    }
     
    /////////////////////////////
    //
    //    PROTECTED METHODS
    //
    /////////////////////////////

    protected void startHealthbar(bool createhud = false)
    {
        hud = findHUD(true);

        if (hud != null)
        {
            GameObject a = (GameObject)Instantiate(healthbar, Vector3.up, Quaternion.identity);
            a.GetComponent<BossHealthbar>().boss = this.gameObject;
            a.transform.SetParent(hud.transform);
        }
    }
    public GameObject findHUD(bool create = false)
    {
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud == null && create)
        {
            hud = (GameObject)Instantiate(new GameObject("HUD"), Vector3.up, Quaternion.identity);
            hud.tag = "HUD";
            hud.layer = 5;

            hud.AddComponent<RectTransform>();
            Canvas c = hud.AddComponent<Canvas>();
            CanvasScaler s = hud.AddComponent<CanvasScaler>();
            GraphicRaycaster g = hud.AddComponent<GraphicRaycaster>();

            c.targetDisplay = 1;
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            c.pixelPerfect = false;
            c.sortingOrder = 0;
            c.targetDisplay = 0;

            s.referenceResolution = new Vector2(1600, 900);
            s.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            s.matchWidthOrHeight = 0f;
            s.referencePixelsPerUnit = 100;

            g.ignoreReversedGraphics = false;
            g.blockingObjects = GraphicRaycaster.BlockingObjects.None;
        }
        return hud;
    }

    public void setRigidbody()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (_rb == null)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
            _rb.isKinematic = false;
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }

    public void setVelocity(Vector2 v)
    {
        _rb.velocity = v;
    }

    public void setVelocity(float x = 0, float y = 0)
    {
        setVelocity(new Vector2(x, y));
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
        resetShooters();
        afterLife();
    }

    abstract protected void afterLife();

    abstract public override void move();

    abstract public override void ready();

}
