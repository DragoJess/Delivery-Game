using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] Animator TextAnim;
    [SerializeField] TMP_Text TextText;
    private void Awake()
    {
        Instance = this;
    }

    public void Message(string Message)
    {
        TextAnim.SetTrigger("Play");
        TextText.text = Message;
    }
}
