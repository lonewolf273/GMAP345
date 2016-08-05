using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
    protected int playerNumber;

    private float timer = -1f;
    [SerializeField] protected float bulletTimer = 0.1f;

    Rigidbody2D _rb;

    KeyboardController keyboard = new KeyboardController();
    Controller controller = new Controller();

    ///////////////////////////////////////
    //
    //       DEFAULT UNITY FUNCTIONS
    //
    ///////////////////////////////////////
    
    void Start()
    {
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

    ///////////////////////////////////////
    //
    //          INSPECTORS
    //
    ///////////////////////////////////////
    public float getMaxTimer()
    {
        return bulletTimer;
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

    ///////////////////////////////////////
    //
    //          MUTATORS
    //
    ///////////////////////////////////////
    public void setPlayerNumber(int i)
    {
        if (i < 1 || i > 2)
            throw new Exception("Invalid player number: " + i.ToString());
        else
            playerNumber = i;
    }

    ///////////////////////////////////////
    //
    //          FACILITATORS
    //
    ///////////////////////////////////////

    public override void attack()
    {
        if (Input.GetButton(getModifier() + " Fire") && timer < 0f)
        {
            
            getBulletShooterAt(getBulletAt(0)).shoot(bulletSpeed, transform.position, transform.rotation.z);
            timer = getMaxTimer();
        }
        timer -= Time.deltaTime;
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
            moverY = controller.PlayerAxis(getNumber(), Controls.UP);

        moverX *= getMoveSpeed();
        moverY *= getMoveSpeed();

        float mod = 1f;

        if (Input.GetButton(getModifier() + " Focus"))
            mod = 0.4f;
        _rb.velocity = new Vector2(moverX * mod, moverY * mod);
    }
}
