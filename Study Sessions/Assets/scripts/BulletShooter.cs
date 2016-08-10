using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BulletShooter {

    // VARIABLES
    private GameObject bullet;
    private List<GameObject> bulletList;

    
    // CONSTRUCTORS
    public BulletShooter()
    {
        bullet = null;
        bulletList = new List<GameObject>();
    }

    public BulletShooter(GameObject b)
    {
        setBullet(b);
        bulletList = new List<GameObject>();
    }

    // INSPECTORS
    bool checkDisabled(GameObject g) //checks if the bullet is disabled
    {
        return !(g.activeSelf);
    }

    public GameObject getBullet()
    {
        return bullet;
    }
    bool checkDisabled(int i) //checks if the bullet at index i is disabled
    {
        return checkDisabled(bulletList[i]);
    }
    // MUTATORS
    public void setBullet(GameObject b)
    {
        bullet = b;
    }
    public void resetList(GameObject b)
    {
        kill();
    }

    // FACILITATORS
    public void shoot(float s)
    {
        shoot(s, new Vector3(0, 0, 0), 0);
    }
    public void shoot(float s, Vector3 p, float q) //shoot at speed s, at position p, and at quaternion q
    {
        GameObject i = bulletList.Find(checkDisabled);
        if (i == null)
        {
            GameObject a = (GameObject)MonoBehaviour.Instantiate(bullet, p, Quaternion.identity);
            a.GetComponent<Bullet>().reset();
            a.GetComponent<Bullet>().changeSpeed(s);
            a.GetComponent<Bullet>().setRotation(q);
            bulletList.Add(a);
        }
        else
        {
            i.SetActive(true);
            i.GetComponent<Bullet>().reset();
            i.transform.position = p;
            i.GetComponent<Bullet>().changeSpeed(s);
            i.GetComponent<Bullet>().setRotation(q);
        }
    }

    // SHOT TYPES
    public void shootCircle(float speed, Vector3 position, int amount, int offset = 0) //shoots amount of bullets at position at speed
    {
        for (int i = 0; i < amount; i++)
        {
            shoot(speed, position, 360f / amount * i + offset);
        }
    }


    // KILL CODE
    public void kill()
    {
        foreach (var i in bulletList)
        {
            if (i.activeSelf == true)
                i.GetComponent<Bullet>().setDeath(true);
            else
            {
                i.SetActive(true);
                MonoBehaviour.Destroy(i);
            }
        }
        bulletList.Clear();
    }
}
