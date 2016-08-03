using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;
    private Rigidbody2D _rb;
    private bool deathCheck = false;

    // Use this for initialization
    void Start () {
        _rb = (Rigidbody2D)gameObject.GetComponent(typeof(Rigidbody2D));
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, angle);
        _rb.velocity = (transform.rotation * Vector2.up).normalized * speed;
        if (checkOutside())
        {
            if (deathCheck)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
        //transform.Rotate(0, 0, 100 * Time.deltaTime);
        
    }

    bool checkOutside()
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
