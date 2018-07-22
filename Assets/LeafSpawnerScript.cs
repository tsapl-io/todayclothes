using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawnerScript : MonoBehaviour {
    float time;
    public bool LeafWind;
    public GameObject Leaf;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > Random.Range(0.05f,0.5f) && LeafWind) {
            Instantiate(Leaf, new Vector3(5f, 4f, 0f), Quaternion.identity);
            time = 0f;
        }
	}
}
