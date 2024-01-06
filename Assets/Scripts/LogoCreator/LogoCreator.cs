using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class LogoCreator : MonoBehaviour
{
    [SerializeField]
    private Sprite backgroundImage;

    [SerializeField]
    private Sprite[] primaryImages;

    [SerializeField]
    private Sprite[] secondaryImages;

    [SerializeField]
    private GameObject[] texts;

    [SerializeField]
    private TMP_FontAsset[] fonts;

    [SerializeField]
    private int[] _choices = new int[] { 2, 0, 2 };

    private Transform display;

    private void Start()
    {
        display = transform;
        CreateLogo(_choices);
    }

    public void CreateLogo(int[] choices)
    {
        CreateImage(backgroundImage);
        CreateImage(primaryImages[choices[0]]);
        CreateImage(secondaryImages[choices[1]]);

        var Text = Instantiate(texts[choices[2]], display).GetComponent<TMP_Text>();

        if(fonts.Length > 0)
        {
            Text.font = fonts[choices[3]];
        }
    }

    private GameObject CreateImage(Sprite _image)
    {
        GameObject _object = new GameObject();
        var image = _object.AddComponent<Image>();
        image.sprite = _image;
        _object.transform.SetParent(display);
        _object.transform.localScale = new Vector3(10, 10, 1);
        _object.transform.localPosition = new Vector3(0, 0, 0);

        return _object;
    }
}
