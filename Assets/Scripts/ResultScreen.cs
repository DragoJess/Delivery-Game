using TMPro;
using UnityEngine;
using System.Collections;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] TMP_Text DayNumber, PackagesDelivered, PackagesLost, Rank;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioClip clipboardSound;
    public void OpenPanel()
    {
        BGM.mute = true;
        SoundManager.Instance.PlaySound(clipboardSound, transform, 0.8f);
        DayNumber.text = $"Day{GameCycleManager.Instance.Day}";
        var Report = DeliveryManager.Instance.GetPackageReport();
        PackagesDelivered.text = $"Packages Delivered: {Report.Correct + Report.Wrong + Report.Saved}";
        PackagesLost.text = $"Incorrect Deliveries: {Report.Wrong + Report.Saved}";
        StatTracker.Instance.CorrectDelivery += Report.Correct;
        StatTracker.Instance.WrongDelivery += Report.Wrong;
        StatTracker.Instance.Saved += Report.Saved;

        if (Report.Wrong + Report.Saved == 0)
            Rank.text = "A";
        else if (Report.Wrong + Report.Saved <= 1)
            Rank.text = "B";
        else if (Report.Wrong + Report.Saved <= 2)
            Rank.text = "C";
        else if (Report.Wrong + Report.Saved <= 3)
            Rank.text = "D";
        else
            Rank.text = "F";
    }
    public void CloseScreen()
    {
        StartCoroutine(CloseWithDelay());
    }
    IEnumerator CloseWithDelay()
    {
        yield return new WaitForSeconds(.16f);
        gameObject.SetActive(false);
    }
}
