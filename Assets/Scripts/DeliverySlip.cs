using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliverySlip : MonoBehaviour
{
    [SerializeField] TMP_Text Address, Description;
    Image sprite;
    private void Awake()
    {
        sprite = GetComponent<Image>();
    }
    public void SetInfo(string Address, string Description)
    {
        sprite.color *= Random.Range(.85f, 1.15f);
        this.Address.text = Address;
        this.Description.text = Description;
    }
}
