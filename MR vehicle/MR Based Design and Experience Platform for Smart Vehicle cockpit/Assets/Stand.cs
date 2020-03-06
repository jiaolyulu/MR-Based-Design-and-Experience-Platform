using UnityEngine;
using System.Collections;

public class Stand : MonoBehaviour {
    public float smooth = 1f;
    private Quaternion targetRotation;
    // Use this for initialization
    void Start () {
        targetRotation = transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetRotation *= Quaternion.AngleAxis(90, Vector3.up);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5 * smooth * Time.deltaTime);
    }
}

