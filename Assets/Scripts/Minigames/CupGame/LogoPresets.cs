using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LogoPreset", fileName = "new LogoPreset")]
public class LogoPresets : ScriptableObject
{
    public Sprite backgroundImage;
    public Sprite[] primaryImages;
    public Color[] primaryColors;
    public Sprite[] secondaryImages;
    public Color[] secondaryColors;
    public Sprite[] textFonts;
    public Color[] textColors;

}
