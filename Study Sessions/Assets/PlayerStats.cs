using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    public int health;
    private float invunTimer = -1f;
    public const float INVUN_TIMER = 3f;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        invunTimer -= Time.deltaTime;
	}
    void damage()
    {
        if (health == 0)
            Destroy(gameObject);
        if (invunTimer < 0)
            health -= 1;
        invunTimer = INVUN_TIMER; 
    }
}
