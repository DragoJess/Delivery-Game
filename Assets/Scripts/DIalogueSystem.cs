using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;
    [SerializeField] TMP_Text Name, Content;
    [SerializeField] AudioSource TalkingSound;

    [SerializeField]
    UnityEvent OnDialogueFinish = new UnityEvent();
    Animator Anim;

    private void Awake()
    {
        Instance = this;
        Anim = GetComponent<Animator>();
    }
    public void PlayDialogue(Dialogue[] Dialogue)
    {
        StartCoroutine(PlayDialogueAsync(Dialogue));
    }
    IEnumerator PlayDialogueAsync(Dialogue[] Dialogue)
    {
        TalkingSound.mute = false;
        Anim.SetBool("Open", true);
        foreach(var Line in Dialogue)
        {
            Name.text = Line.Name;
            Content.text = Line.Content;
            yield return new WaitForSeconds(2 + Line.Content.Length * .02f);
        }
        Anim.SetBool("Open", false);
        TalkingSound.mute = true;
        OnDialogueFinish.Invoke();
    }
}
[Serializable]
public struct Dialogue
{
    public string Name;
    public string Content;
}