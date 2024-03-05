using UnityEngine;

[System.Serializable]
public class DialogueFragment
{
    public string name;
    [TextArea (3,10)]
    public string text;

    public Sound dialogueAudio;
}
