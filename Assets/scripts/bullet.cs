using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    Rigidbody2D rb2d;
    public float fuerzaLanz = 0.02f ;
    public GameObject ptoLanzamiento;
    bool enUso;
    
	// Use this for initialization
	void Start () {
        transform.position = ptoLanzamiento.transform.position;
    
    GetComponent<Rigidbody2D>().AddForce(transform.right * fuerzaLanz);
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
