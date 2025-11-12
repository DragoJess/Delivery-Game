using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    [SerializeField] DeliveryDestination[] Destinations;
    DeliveryListItem[] CurrentList;
    private void Awake()
    {
        Instance = this;
    }
    public void CheckDeliveryItem(DeliveryListItem Item)
    {
        Destinations[Item.TargetLocation].CheckPackage(Item);
    }    
}
[System.Serializable]
public struct DeliveryManifest
{
    public PackageData[] Packages;
}
public struct PackageData
{
    int StartLocation;
    int TargetLocation;
    bool isReturn;
}
