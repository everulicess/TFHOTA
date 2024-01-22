using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

[System.Serializable]
public class DialogueFragment
{
    public string name;
    [TextArea (3,10)]
    public string text;

    public Sound dialogueAudio;
}
