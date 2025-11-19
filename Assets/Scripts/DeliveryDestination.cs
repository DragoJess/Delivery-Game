using TMPro;
using UnityEngine;

public class DeliveryDestination : MonoBehaviour
{
    [SerializeField] string LocationName;
    [SerializeField] SpecialLocation Special;
    [SerializeField] TMP_Text InteractText;
    public void Deliver(Package package)
    {
        if(package.Destination == LocationName)
        {
            package.Status = PackageStatus.Delivered;
        }
        else
        {
            switch(Special)
            {
                case SpecialLocation.ReturnCenter:
                    package.Status = PackageStatus.DeliveredWrong;
                    break;
                case SpecialLocation.PlayerHouse:
                    package.Status = PackageStatus.Saved;
                    break;
                default:
                    package.Status = PackageStatus.DeliveredWrong;
                    break;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collisions)
    {
        InteractText.text = LocationName;
    }
    private void OnTriggerExit2D(Collider2D collisions)
    {
        InteractText.text = string.Empty;
    }
    enum SpecialLocation
    {
        No, ReturnCenter, PlayerHouse
    }
}
