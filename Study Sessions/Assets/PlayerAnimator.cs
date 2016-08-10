using UnityEngine;
using System.Collections;
using System;


public class PlayerAnimator : MonoBehaviour {

    public Animator anim;
    Player p;

	// Use this for initialization
	void Start () {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
            if (anim == null)
                anim = gameObject.AddComponent<Animator>();
        }

        p = transform.parent.gameObject.GetComponent<Player>();
        if (p == null)
            p = transform.parent.gameObject.AddComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("speed", p.getCurrentSpeed());
        if (p.getShot())
            anim.SetTrigger("shoot");
    }
}
