using UnityEngine;

public class Control : MonoBehaviour
{
    public Transform Snake;
    public float Sensitivity;
    private Vector3 _previousMousePosition;

    public float BorderLeft = -2.5f;
    public float BorderRight = 2.5f;
    
    void Update()
    {
        //output to log the position change
        //Debug.Log(transform.position);
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            Snake.Translate(delta.x * Sensitivity, 0, 0);
            Camera.main.transform.Translate(delta.x * Sensitivity, 0, 0);

        }

        _previousMousePosition = Input.mousePosition;

        if (Snake.position.x < BorderLeft)
        {
            Snake.position = new Vector3(BorderLeft, Snake.position.y, Snake.position.z);
            Camera.main.transform.position = new Vector3(BorderLeft, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (Snake.position.x > BorderRight)
        {
            Snake.position = new Vector3(BorderRight, Snake.position.y, Snake.position.z);
            Camera.main.transform.position = new Vector3(BorderRight, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }

    }
    
}