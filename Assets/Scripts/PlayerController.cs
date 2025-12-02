using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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

    private bool _moving;
    private Coroutine _moveRoutine;
    [SerializeField] private AudioClip footstepSound;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip putDownSound;
    [SerializeField] private AudioClip teleportSound;
     
    [SerializeField] LayerMask LayerPackages, LayerTeleporters;
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
            {
                StartCoroutine(TeleportSequence(Hit.transform.GetComponent<Teleporter>()));
                SoundManager.Instance.PlaySound(teleportSound, transform, 0.4f);
            }
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
                    CurrentHolding.Status = PackageStatus.NotDelivered;
                    Hit.transform.SetParent(HoldPosition);
                    Hit.transform.localPosition = Vector3.zero;
                    SoundManager.Instance.PlaySound(pickUpSound, transform, 0.5f);
                    DeliveryManager.Instance.UpdateDeliveryChecklist();
                }    

            }
            else
            {
                RaycastHit2D Hit = Physics2D.CircleCast(transform.position, .4f, Vector2.right, 0, LayerPackages);

                if (Hit) {
                    Hit.transform.GetComponent<DeliveryDestination>();
                    SoundManager.Instance.PlaySound(putDownSound, transform, 0.5f);
                }
                CurrentHolding.CheckSnapPosition();
                CurrentHolding.TryDeliver();
                CurrentHolding = null;

            }

    }
    private void Update()
    {
         
        self.linearVelocity = (CurrentDirection * (SprintDuration > 0 ? SprintSpeed : MoveSpeed) * Time.deltaTime);
        if (self.linearVelocity.magnitude > 0 && !_moving)
        {
            _moving = true;
            _moveRoutine = StartCoroutine(SoundManager.Instance.PlayRepeatingSound(footstepSound, transform, 0.5f, 0.4f));
        }
        else if (self.linearVelocity.magnitude == 0)
        {
            if (_moveRoutine != null) StopCoroutine(_moveRoutine);
            _moving = false;
        }

        

        SprintDuration -= Time.deltaTime;
    }
    
    
}
