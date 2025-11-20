using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance;

    public Package[] Packages;
    [SerializeField] List<TMP_Text> PackageStatuses = new List<TMP_Text>();
    [SerializeField] GameObject DeliveryChecklistItem;
    [SerializeField] Transform DeliveryChecklistContent;
    [SerializeField] UnityEvent OnCompletedDay = new UnityEvent();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Packages = FindObjectsByType<Package>(FindObjectsSortMode.InstanceID);
        foreach (var package in Packages)
            PackageStatuses.Add(Instantiate(DeliveryChecklistItem, DeliveryChecklistContent).GetComponent<TMP_Text>());

        UpdateDeliveryChecklist();
        MissionStartScreen.Instance.ShowTodaysPackages();
    }
    public void UpdateDeliveryChecklist()
    {
        for (int i = 0; i < Packages.Length; i++)
            PackageStatuses[i].text = $"{Packages[i].Destination} - {Packages[i].ConvertStatus()}";

        bool AllDelivered = false;
        foreach (var package in Packages)
        {
            AllDelivered = (package.ConvertStatus() == PackageStatus.Delivered || package.ConvertStatus() == PackageStatus.Returned);
            if (!AllDelivered)
                break;
        }
        if (AllDelivered)
            OnCompletedDay.Invoke();


    }
    public PackageReport GetPackageReport()
    {
        var Report = new PackageReport();
        foreach(var package in Packages)
        {
            switch(package.Status)
            {
                case PackageStatus.Delivered: Report.Correct++; break;
                case PackageStatus.DeliveredWrong: Report.Wrong++; break;
                case PackageStatus.Saved: Report.Saved++; break;
                case PackageStatus.ReturnedCorrectly: Report.Correct++; break;
                case PackageStatus.ReturnedWrong: Report.Correct++; break;
                default: Report.Wrong++; break;

            }
        }
        return Report;
    }
}
public class PackageReport
{
    public int Correct;
    public int Wrong;
    public int Saved;
}
