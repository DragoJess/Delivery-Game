using TMPro;
using UnityEngine;
using System.Collections;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] TMP_Text DayNumber, PackagesDelivered, PackagesLost, Rank;
    public void OpenPanel()
    {
        DayNumber.text = $"Day{GameCycleManager.Instance.Day}";
        var Report = DeliveryManager.Instance.GetPackageReport();
        PackagesDelivered.text = $"Packages Delivered: {Report.Correct + Report.Wrong + Report.Saved}";
        PackagesLost.text = $"Incorrect Deliveries: {Report.Wrong + Report.Saved}";

        if (Report.Wrong == 0)
            Rank.text = "A";
        else if (Report.Wrong <= 1)
            Rank.text = "B";
        else if (Report.Wrong <= 2)
            Rank.text = "C";
        else if (Report.Wrong <= 3)
            Rank.text = "D";
        else
            Rank.text = "Fired";
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
