using System.Collections;
using UnityEngine;

public class MissionStartScreen : MonoBehaviour
{
    public static MissionStartScreen Instance;
    [SerializeField] DeliverySlip[] Slips;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioClip startSound;

    private void Awake()
    {
        Instance = this;
    }
    public void ShowTodaysPackages()
    {
        foreach(var slip in Slips)
            slip.gameObject.SetActive(false);
        StartCoroutine(ShowSlipsAsync());
    }
    IEnumerator ShowSlipsAsync()
    {
        for (int i = 0; i < DeliveryManager.Instance.Packages.Length; i++)
        {
            var Package = DeliveryManager.Instance.Packages[i];
            Slips[i].gameObject.SetActive(true);
            Slips[i].SetInfo(Package.Destination, Package.Description);
            yield return new WaitForSeconds(Random.Range(0f, .2f));
        }
    }
    public void CloseScreen()
    {
        StartCoroutine(CloseWithDelay());
        SoundManager.Instance.PlaySound(startSound, transform, 0.2f);
    }
    IEnumerator CloseWithDelay()
    {
        yield return new WaitForSeconds(.16f);
        gameObject.SetActive(false);
        //BGM.time = 0f;
        BGM.mute = false;
    }
}
