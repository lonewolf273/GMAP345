using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    protected float speed;
    protected Rigidbody2D _rb;
    protected bool deathCheck = false;

    // Use this for initialization
    void Start () {
        _rb = (Rigidbody2D)gameObject.GetComponent(typeof(Rigidbody2D));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, angle);
        move(); 

        //transform.Rotate(0, 0, 100 * Time.deltaTime);
        
    }
    public void move()
    {
        _rb.velocity = (transform.rotation * Vector2.up).normalized * speed;
        if (checkOutside())
        {
            if (deathCheck)
                kill();
            else
                gameObject.SetActive(false);
        }
    }
    public void kill()
    {
        Destroy(gameObject);
    }

    public void reset()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    public void setScale(float f)
    {
        transform.localScale = new Vector3(f, f, 1);
    }
    protected bool checkOutside()
    {
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));
        Vector3 botLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));

        if (topRight.y < transform.position.y) return true;
        if (topRight.x < transform.position.x) return true;
        if (botLeft.y > transform.position.y) return true;
        if (botLeft.x > transform.position.x) return true;
        return false;
    }
    Vector2 getSpeed()
    {
        return new Vector2(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z));
    }

    public void changeSpeed(float s)
    {
        speed = s;
    }

    public void setRotation(float a)
    {
        //_rb.AddForce(new Vector2(0, speed));
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.Rotate(0, 0, a);
        //transform.rotation.Set(0, 0, angle, 0);
    }
    public void setDeath(bool d)
    {
        deathCheck = d;
    }
}
