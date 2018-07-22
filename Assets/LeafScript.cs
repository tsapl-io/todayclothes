using UnityEngine;

public class LeafScript : MonoBehaviour {
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddForce(Random.Range(-100f, -200f), Random.Range(-550f, 500f), Random.Range(-500f, 500f));
        transform.Rotate(Random.Range(0f, 3f), Random.Range(0f, 3f), Random.Range(0f, 3f));
        if (transform.position.y < 0f) {
            Destroy(gameObject);
        }
	}
}
