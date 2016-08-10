using UnityEngine;
using System.Collections;

public class WholeNote : Bullet {

    [SerializeField]
    private float scaleFactor;

    [SerializeField]
    private float rotationFactor;

    [SerializeField]
    BulletList shoot;

    [SerializeField]
    private bool clockwise;

    private float timer;
    private bool live = true;
    private float delay;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(1, 1, 1);
        shoot.reset();
        shoot.resetTimer();
        if (deathCheck == true)
            shoot.reset();
	}
	// Update is called once per frame
	void Update () {
        move();
        movement();
        if (delay < 0 && live)
        {
            shoot.update(Time.deltaTime);
            attack();
        }
        if (gameObject.activeSelf == false)
            transform.localScale = (new Vector3(1, 1, 1));
        else delay -= Time.deltaTime;
	}
    public new void kil()
    {
        shoot.getShooter().kill();
        Destroy(gameObject);
    }
    public bool getClockwise() { return clockwise; }
    public float getScaleFactor() { return scaleFactor; }
    public float getRotationFactor() { return rotationFactor; }

    public void setClockwise(bool c) { clockwise = c; }
    public void setScaleFactor(float s) { scaleFactor = s; }
    public void setRotationFactor(float r) { rotationFactor = r; }

    public new void reset()
    {
        transform.rotation = new Quaternion();
        transform.localScale = new Vector3(1, 1, 1);
        Debug.Log(transform.localScale);
        shoot.reset();
    }

    public void attack()
    {
        shoot.shoot(BulletList.ShotType.CIRCLE, transform.position, transform.eulerAngles.z, 4);
    }

    public void setFuse(float maxTimer, float d = -1)
    {
        if (maxTimer < 0) return;
        live = true;
        if (delay < 0) d = maxTimer;
        shoot.setMaxTimer(maxTimer);
        delay = d;
    }

    void movement()
    {
        transform.localScale = new Vector3(transform.localScale.x + scaleFactor, transform.localScale.y + scaleFactor, transform.localScale.z);
    }
}
