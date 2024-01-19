using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PrintModel : MonoBehaviour
{
    ModelCardScrObj modelScrObj;
    GameObject card;
    [SerializeField]Transform printPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(StartPrint());
        }
    }
    public IEnumerator StartPrint()
    {
        modelScrObj = card.GetComponent<ModelCard>().modelScrObj;
        card.GetComponent<XRGrabInteractable>().enabled = false;

        var objectToPrint = modelScrObj.ModelPrefab;
        var printingTime = modelScrObj.PrintingTime;
        //play preapring for printing sounds
        yield return new WaitForSeconds(3f);

        Instantiate(objectToPrint, printPosition.position,printPosition.rotation);
        yield return new WaitForSeconds(printingTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("3DCard"))
        {
            Debug.Log($"Card has been placed: ");
            card = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("3DCard"))
        {
            card = null;
            modelScrObj = null;
        }
    }
}
