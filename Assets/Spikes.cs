using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    public Collider2D c2d;
    bool active = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ToggleCollider()
    {
        active = !active;
        c2d.enabled = active;
    }
}
