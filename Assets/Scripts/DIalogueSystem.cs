using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;
    [SerializeField] TMP_Text Name, Content;
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
        Anim.SetBool("Open", true);
        foreach(var Line in Dialogue)
        {
            Name.text = Line.Name;
            Content.text = Line.Content;
            yield return new WaitForSeconds(2 + Line.Content.Length * .02f);
        }
        Anim.SetBool("Open", false);
    }
}
[Serializable]
public struct Dialogue
{
    public string Name;
    public string Content;
}