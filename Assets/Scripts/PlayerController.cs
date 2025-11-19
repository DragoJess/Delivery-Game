using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    Rigidbody2D self;
    [SerializeField] float MoveSpeed;
    [SerializeField] float SprintSpeed;
    float SprintDuration;
    [SerializeField] Vector3 CurrentDirection;
    [SerializeField] bool Sprinting;
    Package CurrentHolding;
    [SerializeField] Transform HoldPosition;

    [SerializeField] LayerMask LayerPackages, LayerTeleporters, LayerDestinations;
    [SerializeField] Animator SceneChangeAnim;
    [SerializeField] Animator PackageInfo;
    [SerializeField] TMP_Text PackageAddress, PackageDescription;
    private void Awake()
    {
        self = GetComponent<Rigidbody2D>();
        Instance = this;
    }
    public void Move(InputAction.CallbackContext Context)
    {
        CurrentDirection = Context.ReadValue<Vector2>().normalized;
    }
    public void Sprint(InputAction.CallbackContext Context)
    {
        if (Context.performed)
            if (SprintDuration < 0)
                SprintDuration = .2f;
    }
    public void Teleport(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            RaycastHit2D Hit = Physics2D.CircleCast(transform.position, .4f, Vector2.right, 0, LayerTeleporters);
            if (Hit)
                StartCoroutine(TeleportSequence(Hit.transform.GetComponent<Teleporter>()));
        }
    }
    IEnumerator TeleportSequence(Teleporter teleporter)
    {
        SceneChangeAnim.SetTrigger("Play");
        yield return new WaitForSeconds(.16f);
        teleporter.Teleport();
    }
    public void CheckPackageInfo(InputAction.CallbackContext Context)
    {
        if (Context.performed)
        {
            if(CurrentHolding != null)
            {
                PackageInfo.SetBool("Open", !PackageInfo.GetBool("Open"));
                PackageAddress.text = CurrentHolding.Destination;
                PackageDescription.text = CurrentHolding.Description;
            }

        }
    }
    public void Pickup(InputAction.CallbackContext Context)
    {
        if(Context.performed)
            if (CurrentHolding == null)
            {
                RaycastHit2D Hit = Physics2D.CircleCast(transform.position, .4f, Vector2.right, 0, LayerPackages);
            
                if(Hit)
                {
                    CurrentHolding = Hit.transform.GetComponent<Package>();
                    Hit.transform.SetParent(HoldPosition);
                    Hit.transform.localPosition = Vector3.zero;
                }    

            }
            else
            {
                RaycastHit2D Hit = Physics2D.CircleCast(transform.position, .4f, Vector2.right, 0, LayerPackages);

                if (Hit)
                    Hit.transform.GetComponent<DeliveryDestination>();
                CurrentHolding.CheckSnapPosition();
                CurrentHolding = null;
            }

    }
    private void Update()
    {
        self.linearVelocity = (CurrentDirection * (SprintDuration > 0 ? SprintSpeed : MoveSpeed) * Time.deltaTime);
        SprintDuration -= Time.deltaTime;
    }
}
