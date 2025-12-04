using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    public string Destination;
    public string Description;
    public PackageStatus Status;
    [SerializeField] LayerMask LayerSnapPoints, LayerDestinations;
    SpriteRenderer sprite;
    Collider2D col;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    public void Hide()
    {
        col.enabled = false;
        StartCoroutine(FadeSprite());
    }
    IEnumerator FadeSprite()
    {
        yield return new WaitForSeconds(.4f);
        sprite.enabled = false;
    }
    public void CheckSnapPosition()
    {
        RaycastHit2D Hit = Physics2D.CircleCast(transform.position, .5f, Vector2.right, 0, LayerSnapPoints);

        if (Hit)
        {
            transform.SetParent(Hit.transform);
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.SetParent(null);
            transform.localPosition -= new Vector3(0, 0.67f);
        }

    }
    public void TryDeliver()
    {
        var Hit = Physics2D.CircleCast(transform.position, .7f, Vector2.right, 0, LayerDestinations);

        if (Hit)
            Hit.transform.GetComponent<DeliveryDestination>().Deliver(this);
    }
    public PackageStatus ConvertStatus()
    {
        switch (Status)
        {
            case PackageStatus.NotDelivered: return PackageStatus.NotDelivered;
            case PackageStatus.Delivered: return PackageStatus.Delivered;
            case PackageStatus.DeliveredWrong: return PackageStatus.Delivered;
            case PackageStatus.Saved: return PackageStatus.Delivered;
            case PackageStatus.ReturnedCorrectly: return PackageStatus.Returned;
            case PackageStatus.ReturnedWrong: return PackageStatus.Returned;
            default: return PackageStatus.NotDelivered;
        }
    }
}
public enum PackageStatus
{
    NotDelivered, Delivered, DeliveredWrong, Saved, ReturnedCorrectly, ReturnedWrong, Returned
}
