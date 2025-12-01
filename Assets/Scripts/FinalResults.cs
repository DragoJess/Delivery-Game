using UnityEngine;

public class FinalResults : MonoBehaviour
{
    [SerializeField] DialogueSource GoodEnding, BadEnding, MidEnding;
    void Start()
    {
        if (StatTracker.Instance.WrongDelivery < 2)
            MidEnding.Play();
        else if (StatTracker.Instance.WrongDelivery > 3)
            BadEnding.Play();
        else if (StatTracker.Instance.Saved > 4)
            GoodEnding.Play();
    }
}
