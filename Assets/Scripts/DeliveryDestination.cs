using TMPro;
using UnityEngine;

public class DeliveryDestination : MonoBehaviour
{
    [SerializeField] string LocationName;
    [SerializeField] SpecialLocation Special;
    [SerializeField] TMP_Text InteractText;
    [SerializeField] TMP_Text NameText;

    private void Awake()
    {
        NameText.text = LocationName;
    }
    public void Deliver(Package package)
    {
        if(Special == SpecialLocation.ReturnCenter)
        {
            if (package.Destination == LocationName)
                package.Status = PackageStatus.ReturnedCorrectly;
            else
                package.Status = PackageStatus.ReturnedWrong;
        }
        else
        {
            if (package.Destination == LocationName)
            {
                package.Status = PackageStatus.Delivered;
            }
            else
            {
                switch (Special)
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
        DeliveryManager.Instance.UpdateDeliveryChecklist();
        package.Hide();
    }
    private void OnTriggerEnter2D(Collider2D collisions)
    {
        InteractText.text = "Space";
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
