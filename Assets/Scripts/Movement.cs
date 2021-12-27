using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float ForwardSpeed = 5;
    public float Sensitivity = 10;

    public int Length = 1;

    public TextMeshPro PointsText;

    private Camera mainCamera;
    private Rigidbody componentRigidbody;
    public TailController componentTailController;

    private Vector2 touchLastPos;
    private float sidewaysSpeed;

    
    private void Start()
    {
        mainCamera = Camera.main;
        componentRigidbody = GetComponent<Rigidbody>();
        //componentTail = GetComponent<Tail>();

        for (int i = 0; i < Length; i++) componentTailController.AddCircle();

        PointsText.SetText(Length.ToString());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            sidewaysSpeed = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = (Vector2)mainCamera.ScreenToViewportPoint(Input.mousePosition) - touchLastPos;
            sidewaysSpeed += delta.x * Sensitivity;
            touchLastPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            Length++;
            componentTailController.AddCircle();
            PointsText.SetText(Length.ToString());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Length--;
            componentTailController.RemoveCircle();
            PointsText.SetText(Length.ToString());
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(sidewaysSpeed) > 4) sidewaysSpeed = 4 * Mathf.Sign(sidewaysSpeed);
        componentRigidbody.velocity = new Vector3(sidewaysSpeed * 5, 0, ForwardSpeed);

        sidewaysSpeed = 0;
        
    }
}