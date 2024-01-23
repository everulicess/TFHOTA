using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LogoCreator : MonoBehaviour
{
    [SerializeField]
    private LogoPresets logoPresets;

    public Transform display;

    private SpriteRenderer primary;
    private SpriteRenderer secondary;
    private SpriteRenderer text;

    public void CreateLogo(int[] choices)
    {
        CreateImage(logoPresets.backgroundImage).sortingOrder = -1;
        if (choices[0] != -1) { text = CreateImage(logoPresets.textFonts[choices[0]]); text.sortingOrder = 3; }
        if (choices[1] != -1) text.color = logoPresets.textColors[choices[1]];
        if (choices[2] != -1) { primary = CreateImage(logoPresets.primaryImages[choices[2]]); primary.sortingOrder = 1; }
        if (choices[3] != -1) primary.color = logoPresets.primaryColors[choices[3]];
        if (choices[4] != -1) { secondary = CreateImage(logoPresets.secondaryImages[choices[4]]); secondary.sortingOrder = 2; }
        if (choices[5] != -1) secondary.color = logoPresets.secondaryColors[choices[5]];
    }

    private SpriteRenderer CreateImage(Sprite _image)
    {
        GameObject _object = new GameObject();
        var image = _object.AddComponent<SpriteRenderer>();
        image.sprite = _image;
        _object.transform.SetParent(display);
        _object.transform.localScale = new Vector3(100f, 100f, 1f);
        _object.transform.localPosition = new Vector3(0, 0, 0);
        _object.transform.localRotation = Quaternion.Euler(0, 0, 0);

        return image;
    }

    public void ClearDisplay()
    {
        foreach(Transform child in display)
        {
            if (child != display && child.gameObject.name != "background")
            {
                Destroy(child.gameObject);
                primary = null;
                secondary = null;
                text = null;
            }      
        }
    }

    public void HideDisplay()
    {
        display.gameObject.SetActive(false);
    }

    public void ShowDisplay()
    {
        display.gameObject.SetActive(true);
    }
}
