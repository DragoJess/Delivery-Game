using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    [SerializeField] Package[] Packages;
    private void Awake()
    {
        Instance = this;
    }
}
