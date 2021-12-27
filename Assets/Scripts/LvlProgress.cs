using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvlProgress : MonoBehaviour
{
    public Trial Trial;
    public Transform Finish;
    public Slider Slider;
    public float AcceptableFinishPlayerDistance = 1f;

    private float StartingZ;
    private float MinimumReachedZ;

    void Start()
    {
        StartingZ = Trial.transform.position.z; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
