using UnityEngine;
using System.Collections;

public class RotateMove : MonoBehaviour {
    private Quaternion targetRotation;
    private Vector3 targetPosion;

    public AnimationCurve curve;
    public Vector3 distance;
    public float speed;

    private Vector3 startPos, toPos;
    private float timeStart;
    // Use this for initialization
    void Start () {
        targetRotation = transform.rotation;
        targetPosion = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetRotation = Quaternion.Euler(0, 175, 0);
            targetPosion = new Vector3(0.874f, 0.6830392f, -1.226403f);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 3 * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, targetPosion, 3 * Time.deltaTime);
  
        
    }
 
}
