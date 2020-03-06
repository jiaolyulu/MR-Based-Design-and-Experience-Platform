using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float smooth = 1f;
    private Quaternion targetRotation;
    private Quaternion thisRotation;
    public GameObject target;//the target object
    private float speedMod = 10.0f;
    private Vector3 point;
    void Start()
    {
        point = target.transform.position;
        transform.LookAt(point);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 2 * Time.deltaTime * speedMod);
    }
}
