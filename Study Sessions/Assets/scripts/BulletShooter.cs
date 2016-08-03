using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BulletShooter : MonoBehaviour {

    private GameObject bullet;
    private List<GameObject> bulletList;

    public BulletShooter(GameObject b)
    {
        setBullet(b);
        bulletList = new List<GameObject>();
    }
    

    public void shoot(float s, Vector3 p, float q) //shoot at speed s, at position p, and at quaternion q
    {
        GameObject i = bulletList.Find(checkDisabled);
        if (i == null)
        {
            GameObject a = (GameObject)Instantiate(bullet, p, Quaternion.identity);
            a.GetComponent<Bullet>().speed = s;
            a.GetComponent<Bullet>().setRotation(q);
            bulletList.Add(a);
        }
        else
        {
            i.SetActive(true);
            i.transform.position = p;
            i.GetComponent<Bullet>().speed = s;
            i.GetComponent<Bullet>().setRotation(q);
        }
    }

    public void shootCircle(float speed, Vector3 position, int amount, int offset = 0) //shoots amount of bullets at position at speed
    {
        for (int i = 0; i < amount; i++)
        {
            shoot(speed, position, 360f / amount * i + offset);
        }
    }

    bool checkDisabled(GameObject g)
    {
        return !(g.activeSelf);
    }

    public void setBullet(GameObject b)
    {
        bullet = b;
    }
}
