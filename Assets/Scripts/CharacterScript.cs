using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterScript : MonoBehaviour
{
    private Rigidbody rb;
    private InputAction moveAction;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }


    void Update()
    {
        Vector2 moveValue=moveAction.ReadValue<Vector2>();

        //Projections:
        Vector3 f=Camera.main.transform.forward;
        f.y = 0.0f;
        if (f == Vector3.zero)
        {
            f = Camera.main.transform.up;
            f.y=0.0f;
        }
        f.Normalize();

        Vector3 r = Camera.main.transform.right;
        r.y = 0.0f;
        r.Normalize();

        rb.AddForce(250 * Time.deltaTime * //new Vector3(moveValue.x,0,moveValue.y));
            (
               r*moveValue.x+
               f*moveValue.y
            ));
      /*  Vector2 axisValue = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        if (moveValue != Vector2.zero)
        {
            Debug.Log(moveValue);
            Debug.Log(axisValue);
            Debug.Log("---");
        }*/
    }
}
