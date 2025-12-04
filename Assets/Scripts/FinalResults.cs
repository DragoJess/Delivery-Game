using UnityEngine;

public class FinalResults : MonoBehaviour
{
    [SerializeField] DialogueSource GoodEnding, BadEnding, MidEnding;
    void Start()
    {
        if (StatTracker.Instance.Saved > 3)
            GoodEnding.Play();
        else if (StatTracker.Instance.WrongDelivery < 2)
            MidEnding.Play();
        else if (StatTracker.Instance.WrongDelivery > 3)
            BadEnding.Play();
    }
}
