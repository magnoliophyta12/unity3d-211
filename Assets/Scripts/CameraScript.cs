using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Transform character;
    private Vector3 cameraAngles;
    private Vector3 r;
    private InputAction lookAction;
    private float sensitivityH = 10.0f;
    private float sensitivityV = -3.0f;
    private float minVerticalAngle = 35.0f; 
    private float maxVerticalAngle = 75.0f;
    private float minFpvDistance = 0.9f;
    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraAngles = this.transform.eulerAngles;
        character = GameObject.Find("Character").transform;
        r=this.transform.position-character.position;

    }


    void Update()
    {
        Vector2 wheel = Input.mouseScrollDelta;
        if (wheel.y != 0)
        {
            if (r.magnitude > minFpvDistance)
            {
                float rr = r.magnitude * (1 - wheel.y / 10);
                if (rr <= minFpvDistance) 
                {
                    r *= 0.01f;
                }
                else
                {
                    r *= (1 - wheel.y / 10);
                }
            }
            else
            {
                if (wheel.y < 0)
                {
                    r *= 100f;
                }
            }

            r *= 1 - wheel.y / 10;
        }
        Vector2 lookValue=lookAction.ReadValue<Vector2>();
        if(lookValue != Vector2.zero)
        {
            cameraAngles.x += lookValue.y * Time.deltaTime * sensitivityH;
            cameraAngles.y += lookValue.x * Time.deltaTime * sensitivityH;

            cameraAngles.x = Mathf.Clamp(cameraAngles.x, minVerticalAngle, maxVerticalAngle);

            this.transform.eulerAngles = cameraAngles;

        }
        this.transform.position = character.position+Quaternion.Euler(0,cameraAngles.y,0) * r;
    }
}
