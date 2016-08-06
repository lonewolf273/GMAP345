using UnityEngine;
using System.Collections;
<<<<<<< HEAD
using System;

public class Generic : Boss {

    private float deathTimer = 2f;
    private bool atk = true;
    private int offset = 0;
    
    ///////////////////////////////////////
    //
    //       DEFAULT UNITY FUNCTIONS
    //
    ///////////////////////////////////////

    // Use this for initialization
    void Start()
    {
        setStatus(State.ALIVE);
        setRigidbody();
        startHealthbar(true);
        reset();
        resetShooters();
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (status)
        {
            case State.DEAD:
                deathTimer -= Time.deltaTime;
                transform.localScale = new Vector3(deathTimer / 2f, deathTimer / 2f, 0);
                if (deathTimer < 0f)
                    Destroy(gameObject);
                break;
            default:
                getBulletList(0).update(Time.deltaTime);
                attack();
                break;
        }
    }

    public override void attack()
    {
        if (getBulletList(0).checkAlarm())
        {
            getBulletList(0).shoot(BulletList.ShotType.CIRCLE, transform.position, offset, 15);
            offset += 15;
            getBulletList(0).resetTimer();
        }
    }

    public override void move()
    {
        
    }

    public override void ready()
    {
        throw new NotImplementedException();
    }

    protected override void afterLife()
    {

    }


=======

public class Generic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
>>>>>>> bde8623... Now has controller support. Streamlined players. Working on general boss scripts. Got rid of old player code
}
