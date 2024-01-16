using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModelCard : MonoBehaviour
{
    public ModelCardScrObj modelScrObj;
    [SerializeField] Image iconImage;
    [SerializeField] TextMeshProUGUI modelName;
    [SerializeField] GameObject modelToPrint;
    // Start is called before the first frame update
    void Awake()
    {
        modelScrObj.SetCardInfo(modelName, iconImage, modelToPrint);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
