using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] GameObject Package;
    [SerializeField] Transform Container;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < StatTracker.Instance.Saved; i++)
        {
            Instantiate(Package, Container);
        }
    }
}
