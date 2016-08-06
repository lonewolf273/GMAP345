using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
using UnityEngine.UI;
=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code

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
<<<<<<< HEAD

=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code

    ///////////////////////////////////////
    //
    //       DEFAULT UNITY FUNCTIONS
    //
    ///////////////////////////////////////

<<<<<<< HEAD
=======
    ///////////////////////////////////////
    //
    //       DEFAULT UNITY FUNCTIONS
    //
    ///////////////////////////////////////

>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    void Start()
    {

        hp = hpMax;
        bulletShooter = new List<BulletShooter>();
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = gameObject.AddComponent<Rigidbody2D>();

        reset();
        setStatus(State.ALIVE);
<<<<<<< HEAD
=======

>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    }

    void Update()
    {
        attack();
        move();
<<<<<<< HEAD
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
=======
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    }
    public GameObject findHUD(bool create = false)
    {
        GameObject hud = GameObject.FindGameObjectWithTag("HUD");
        if (hud == null && create)
        {
            hud = (GameObject)Instantiate(new GameObject("HUD"), Vector3.up, Quaternion.identity);
            hud.tag = "HUD";
            hud.layer = 5;

<<<<<<< HEAD
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
=======
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
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
    }
     
    /////////////////////////////
    //
    //    PROTECTED METHODS
    //
    /////////////////////////////

<<<<<<< HEAD
    public void setVelocity(float x = 0, float y = 0)
    {
        setVelocity(new Vector2(x, y));
=======
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
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
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
<<<<<<< HEAD
        resetShooters();
=======
        reset();
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
        afterLife();
    }

    abstract protected void afterLife();

    abstract public override void move();

    abstract public override void ready();

}
