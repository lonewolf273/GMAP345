using UnityEngine;
using System.Collections;

public class InstrumentAnimator : MonoBehaviour {
    Animator a;
    Boss b;
    bool die = false;
	// Use this for initialization
	void Start () {
        a = gameObject.GetComponent<Animator>();
        if (a == null)
            throw new System.Exception("Error: No animator found!");
        b = gameObject.transform.parent.GetComponent<Boss>();
        if (b == null)
            throw new System.Exception("error: no boss found!");
        die = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (b.getHp() <= 0 && die == false)
        {
            die = true;
            a.SetTrigger("die");
        }

	}
    public void defeated()
    {
        Destroy(transform.parent.gameObject);
    }
}
