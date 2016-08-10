using UnityEngine;
using System.Collections;

public class HalfNote : Bullet
{
    [SerializeField]
    float boomerangSpeed;
    bool pos;
    float rotationSpeed = 12f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
        reset();
    }

    public new void reset()
    {
        transform.rotation = Quaternion.identity;
        pos = _rb.velocity.x >= 0;
    }

    public new void changeSpeed(float s)
    {
        speed = s;
        pos = s >= 0;
    }

    void Update()
    {
        if (pos) speed -= boomerangSpeed;
        else speed += boomerangSpeed;
        _rb.velocity = (transform.rotation * Vector2.up).normalized * speed;
        if (checkOutside())
        {
            if (deathCheck)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }


    //    [SerializeField]
    //    new Vector2 speed;
    //    bool posX, posY;

    //    [SerializeField]
    //    float boomerangSpeed; //comebackspeed in per second

    //    Rigidbody2D _r;

    //    [SerializeField]
    //    float rotationSpeed = 12f;

    //	// Use this for initialization
    //	void Start () {
    //        _r = gameObject.GetComponent<Rigidbody2D>();
    //        if (_r == null)
    //        {
    //            _r = gameObject.AddComponent<Rigidbody2D>();
    //            if (_r == null)
    //                Destroy(gameObject);
    //        }
    //        setPos();
    //	}

    //	// Update is called once per frame
    //	void Update () {
    //        move();
    //        float xUpdate = Mathf.Abs(getBoomerangSpeed() * Time.deltaTime);
    //        float yUpdate = Mathf.Abs(getBoomerangSpeed() * Time.deltaTime);
    //        if (posX) xUpdate *= -1;
    //        if (posY) yUpdate *= -1;
    //        speed += new Vector2(xUpdate, yUpdate);
    //        //transform.Rotate(0, 0, rotationSpeed);
    //	}

    //    public new void reset()
    //    {
    //        setPos();
    //    }

    //    public Vector2 getSpeed() { return speed; }
    //    public float getBoomerangSpeed() { return boomerangSpeed; }

    //    public void setSpeed(Vector2 v)
    //    {
    //        speed = v;
    //        setPos();
    //    } 
    //    public new void changeSpeed(float f)
    //    {
    //        setSpeed(speed.normalized * f);
    //    }
    //    public new void setRotation(float a)
    //    {
    //        Debug.Log(a);
    //        speed = Quaternion.Euler(0, 0, a) * getSpeed().normalized;
    //    }
    //    void setPos()
    //    {
    //        posX = getSpeed().x >= 0;
    //        posY = getSpeed().y >= 0;
    //    }

    //    public void setBoomerangSpeed(float b) { boomerangSpeed = b; }
    //    void move() { _rb.velocity = getSpeed(); }
    //}
}