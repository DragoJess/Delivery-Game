using UnityEngine;

public class Package : MonoBehaviour
{
    public string Destination;
    public string Description;
    public PackageStatus Status;
    [SerializeField] LayerMask LayerSnapPoints;

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
}
public enum PackageStatus
{
    NotDelivered, Delivered, DeliveredWrong, Saved
}
