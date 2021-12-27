using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public Transform PreviousTail;
    public float MovementPerFrame = 0.01f;
    private float DampV;
    public float DampT = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = PreviousTail.position.x - transform.position.x;
        
        transform.position = new Vector3(
            Mathf.SmoothDamp(transform.position.x, PreviousTail.position.x, ref DampV, DampT),
            transform.position.y,
            transform.position.z);
    }
}
