using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "My Assets/3D Model")]
public class ModelCardScrObj : ScriptableObject
{
    [Header("Model Settings")]
    public string  ModelName;
    public GameObject ModelPrefab;
    public Sprite ModelIcon;
    [Header("Printing Settings")]
    public float PrintingTime;
    

    public void SetCardInfo(TextMeshProUGUI textObjectName, Image imageForSprite, GameObject modelToPrint )
    {
        textObjectName.text = ModelName;
        imageForSprite.sprite = ModelIcon;
        modelToPrint = ModelPrefab;
    }
}
