using UnityEngine;

public class DeliveryDestination : MonoBehaviour
{
    [SerializeField] DestinationType Type;
    public void CheckPackage(DeliveryListItem Item)
    {
        RaycastHit2D Hit = Physics2D.CircleCast(transform.position, 2, Vector2.right);

        if (Hit)
            if (Hit.transform.gameObject.tag == "Package")
            {
                UIManager.Instance.Message("Delivery Successful");
                if (Hit.transform.GetComponent<Package>().Destination == Item.TargetLocation)
                    Item.State = Type == DestinationType.Warehouse ? DeliveryState.Returned : DeliveryState.Delivered;
                else
                    Item.State = Type == DestinationType.PlayerHouse ? DeliveryState.Taken : DeliveryState.DeliveredWrong;

                Item.UpdateStateDisplay();
                    Destroy(Hit.transform.gameObject);
            }

    }
}
public enum DestinationType
{
    Customer, Warehouse, PlayerHouse
}
