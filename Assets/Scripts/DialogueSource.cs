using UnityEngine;

public class DialogueSource : MonoBehaviour
{
    [SerializeField] Dialogue[] Content;

    public void Play()
    {
        DialogueSystem.Instance.PlayDialogue(Content);
    }
}
