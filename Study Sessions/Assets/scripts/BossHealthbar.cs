using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealthbar : MonoBehaviour {
    public GameObject boss;
    private BossGeneric h;
    private Enemy p;
    private Image s;
	// Use this for initialization
	void Start () {
        s = GetComponent<Image>();
        h = boss.GetComponent<BossGeneric>();
        if (h == null)
        {
            p = boss.GetComponent<Enemy>();
            s.fillAmount = 1f;
        }
        else
            s.fillAmount = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (h != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(boss.transform.position);
            setFill(h.getHeatlhPercent());
        }
        else
        {
            setFill(p.getHeatlhPercent());
        }

        if (s.fillAmount <= 0f || boss == null || boss.activeSelf == false)
            Destroy(gameObject);

        s.color = new Color(getRed(), getGreen(), getBlue());
    }

 

    void followBoss()
    {
        transform.position = Camera.main.WorldToScreenPoint(boss.transform.position);
    }
    // yellow: 1, 1, 0
    void setFill(float a)
    {
        float diff = s.fillAmount - a;
        diff = Mathf.Round(diff * 100);
        if (diff < 0) //fill amount < a
            s.fillAmount += 0.01f;
        else if (diff > 0) //fillamount > a
            s.fillAmount -= 0.01f;
        else s.fillAmount = a;
    }

    float getBlue()
    {
        return 0f;
    }

    float getRed()
    {
        if (s.fillAmount >= 0.5f)
            return (1f - s.fillAmount) * 2;
        return 1f;
    }

    float getGreen()
    {
        if (s.fillAmount >= 0.5f) return 1f;
        return (s.fillAmount) * 2;
    }
}
