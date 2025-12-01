using UnityEngine;

public class StatTracker : MonoBehaviour
{
    public static StatTracker Instance;

    public int CorrectDelivery;
    public int WrongDelivery;
    public int Returns;
    public int Saved;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
