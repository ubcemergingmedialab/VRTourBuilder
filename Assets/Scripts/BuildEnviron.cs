using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildEnviron : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Instantiate(EnvironManager.instance.environ);
        Destroy(EnvironManager.instance.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
