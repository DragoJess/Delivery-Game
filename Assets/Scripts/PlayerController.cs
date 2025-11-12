using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Transform selfTransform;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SprintSpeed;
    float SprintDuration;
    [SerializeField] Vector3 CurrentDirection;
    [SerializeField] bool Sprinting;
    Package CurrentHolding;
    [SerializeField] Transform HoldPosition;
    private void Awake()
    {
        selfTransform = transform;
    }
    public void Move(InputAction.CallbackContext Context)
    {
        CurrentDirection = Context.ReadValue<Vector2>().normalized;
    }
    public void Sprint(InputAction.CallbackContext Context)
    {
        if(SprintDuration < 0)
            SprintDuration = .2f;
    }
    public void Pickup(InputAction.CallbackContext Context)
    {
        if(Context.performed)
            if (CurrentHolding == null)
            {
                RaycastHit2D Hit = Physics2D.CircleCast(selfTransform.position, .6f, Vector2.right);
            
                if(Hit)
                    if (Hit.transform.gameObject.tag == "Package")
                    {
                        CurrentHolding = Hit.transform.GetComponent<Package>();
                        Hit.transform.SetParent(HoldPosition);
                        Hit.transform.localPosition = Vector3.zero;
                    }

            }
            else
            {
                CurrentHolding.transform.SetParent(null);
                CurrentHolding.transform.position = selfTransform.position;
                CurrentHolding = null;
            }

    }
    private void Update()
    {
        selfTransform.position += CurrentDirection * (SprintDuration > 0 ? SprintSpeed : MoveSpeed) * Time.deltaTime;
        SprintDuration -= Time.deltaTime;
    }
}
