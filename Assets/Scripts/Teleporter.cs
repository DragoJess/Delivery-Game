using System.Collections;
using TMPro;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    static string E = "E";
    [SerializeField] Transform Destination;
    [SerializeField] TMP_Text InteractText;
    public void Teleport()
    {
        PlayerController.Instance.transform.position = Destination.position;
    }
    private void OnTriggerEnter2D(Collider2D collisions)
    {
        InteractText.text = E;
    }
    private void OnTriggerExit2D(Collider2D collisions)
    {
        InteractText.text = string.Empty;
    }
}
