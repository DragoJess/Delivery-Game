using TMPro;
using UnityEngine;

public class DeliveryListItem : MonoBehaviour
{
    public int TargetLocation;
    public bool Delivered;
    public DeliveryState State;
    [SerializeField] TMP_Text StateText;
    public void CheckPackage()
    {
        DeliveryManager.Instance.CheckDeliveryItem(this);
    }
    public void UpdateStateDisplay()
    {
        StateText.text = State.ToString();
    }
}
public enum DeliveryState
{
    NotDelivered, Delivered, DeliveredWrong, Returned, Taken
}