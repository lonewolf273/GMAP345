using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

/*
 *      PLAYER CLASS:
 *          -Enemy Class with some variants
 *              - sets movement and shooting based on movement
 *              - game depends on all of the players staying alive
 * 
 */

public class Player : Enemy {

    ///////////////////////////////////////
    //
    //          Unique variables
    //
    ///////////////////////////////////////
    [SerializeField]
    protected float focusMultiplier;
    [SerializeField]
    protected float[] startX;

    [SerializeField]
    protected float[] startY;

    [SerializeField]
    protected int playerNumber;


    Rigidbody2D _rb;

    KeyboardController keyboard = new KeyboardController();
    Controller controller = new Controller();

    [SerializeField]
    protected bool autoshoot;

    protected bool focusCheck;

    [SerializeField]
    protected GameObject healthbar;

    protected GameObject protected_healthbar = null;

    protected bool shot;

    ///////////////////////////////////////
    //
    //       DEFAULT UNITY FUNCTIONS
    //
    ///////////////////////////////////////

    void Start()
    {

        hp = hpMax;
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = gameObject.AddComponent<Rigidbody2D>();
        getBulletList(0).setMaxTimer(0.1f);
        reset();
        resetAllBullets();
        setStatus(State.ALIVE);
        createHealthbar();

    }

    void Update()
    {
        setShot(false);
        updateAll();
        attack();
        move();
        //Debug.Log(i.ToString());
        //if (Input.GetKey(KeyCode.DownArrow)) Debug.Log("hi");
    }

    ///////////////////////////////////////
    //
    //          INSPECTORS
    //
    ///////////////////////////////////////
    public float getFocusNumber()
    {
        return focusMultiplier;
    }
    public bool getFocus()
    {
        return focusCheck;
    }
    public float getMaxTimer()
    {
        return getBulletList(0).getMaxTimer();
    }
    public bool getShot() { return shot; }
    public Vector2 getVelocity()
    {
        if (_rb == null) return new Vector2();
        return _rb.velocity;
    }
    public int getNumber()
    {
        if (playerNumber < 0 || playerNumber > 2)
            throw new Exception("Invalid player number: " + playerNumber.ToString());
        if (playerNumber <= 1) return 1;
        return 2;
    }

    public string getPlayerNumber()
    {
        if (playerNumber < 0 || playerNumber > 2)
            throw new Exception("Invalid player number: " + playerNumber.ToString());
        if (playerNumber <= 1) return "1";
        return "2";
    }

    public string getModifier() //gets the modifier for the specific player that's needed to play the game
    {
        return "Player " + getPlayerNumber();
    }

    public float getCurrentSpeed()
    {
        float x = getVelocity().x;
        float y = getVelocity().y;
        return Mathf.Sqrt(x * x + y * y);
    }

    ///////////////////////////////////////
    //
    //          MUTATORS
    //
    ///////////////////////////////////////
    public void setShot(bool b) { shot = b; }
    public void setFocusNumber(float f)
    {
        focusMultiplier = f;
    }
    public void setFocus(bool b)
    {
        focusCheck = b;
    }
    public void setPlayerNumber(int i)
    {
        if (i < 1 || i > 2)
            throw new Exception("Invalid player number: " + i.ToString());
        else
            playerNumber = i;
    }

    public void createHealthbar()
    {
        
        float x = startX[getNumber() - 1];
        float y = startY[getNumber() - 1];

        if (getNumber() == 2)
            x = Camera.main.ViewportToScreenPoint(new Vector3(1, 1, 1)).x;


        GameObject hud = findHUD(true);
        if (hud)
        {
            protected_healthbar = (GameObject)Instantiate(healthbar, new Vector2(x, y), Quaternion.identity);
            protected_healthbar.GetComponent<BossHealthbar>().boss = this.gameObject;
            protected_healthbar.transform.SetParent(hud.transform);
            protected_healthbar.GetComponent<RectTransform>().position = new Vector3(x, y);
            if (getNumber() == 2)
            {
                RectTransform r = protected_healthbar.GetComponent<RectTransform>();
                r.pivot = new Vector2(1, 0);
                //r.transform.localScale = new Vector3(2f, 2f);
            }

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


            c.renderMode = RenderMode.ScreenSpaceOverlay;
            c.pixelPerfect = false;
            c.sortingOrder = 0;
            c.targetDisplay = 0;

            s.referenceResolution = new Vector2(1600f, 900f);
            s.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            s.matchWidthOrHeight = 0f;
            s.referencePixelsPerUnit = 100;

            g.ignoreReversedGraphics = false;
            g.blockingObjects = GraphicRaycaster.BlockingObjects.None;
        }
        return hud;
    }
    ///////////////////////////////////////
    //
    //          FACILITATORS
    //
    ///////////////////////////////////////

    public override void attack()
    {

        bool shootCheck = autoshoot  || keyboard.PlayerCheck(getNumber(), Controls.SHOOT) || Input.GetButton(getModifier() + " Fire");
        if (shootCheck)
        {
            setShot(getBulletList(0).shoot(transform.position, transform.rotation.z));
        }
    }

    public override void die()
    {
        setStatus(State.DEAD);
        reset();
        Destroy(gameObject);
    }

    public override void ready()
    {
        setStatus(State.ALIVE);
    }


    public override void move()
    {
        float moverX, moverY;
        if (keyboard.PlayerCheck(getNumber(), Controls.DOWN))
            moverY = -1f;
        else if (keyboard.PlayerCheck(getNumber(), Controls.UP))
            moverY = 1f;
        else moverY = 0f;

        if (keyboard.PlayerCheck(getNumber(), Controls.LEFT))
            moverX = -1f;
        else if (keyboard.PlayerCheck(getNumber(), Controls.RIGHT))
            moverX = 1f;
        else moverX = 0f;

        if (moverX == 0f)
            moverX = controller.PlayerAxis(getNumber(), Controls.RIGHT);
        if (moverY == 0f)
            moverY = controller.PlayerAxis(getNumber(), Controls.DOWN);

        moverX *= getMoveSpeed();
        moverY *= getMoveSpeed();

        float mod = 1f;

        if (keyboard.PlayerCheck(getNumber(), Controls.FOCUS) || controller.PlayerCheck(getNumber(), Controls.FOCUS))
            setFocus(true);
        else
            setFocus(false);

        if (getFocus())
            mod = getFocusNumber();

        _rb.velocity = avoidOff(new Vector2(moverX * mod, moverY * mod));
    }

    Vector2 avoidOff(Vector2 v)
    {
        float padding = 0.1f;
        v = v * Time.deltaTime;
        //Debug.Log(Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 1)).ToString() + transform.position.ToString() + v.ToString());
        if (Camera.main.ViewportToWorldPoint(new Vector3(0 + padding / 2, 0, 0)).x >= transform.position.x + v.x)
            v.x = 0;
        else if (Camera.main.ViewportToWorldPoint(new Vector3(1 - padding / 2, 1, 1)).x <= transform.position.x + v.x)
            v.x = 0;


        if (Camera.main.ViewportToWorldPoint(new Vector3(0, 0 + padding, 0)).y >= transform.position.y + v.y || Camera.main.ViewportToWorldPoint(new Vector3(1, 1 - padding, 1)).y <= transform.position.y + v.y)
            v.y = 0;

        return v / Time.deltaTime;
    }
}
