using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintModel : MonoBehaviour
{
    ModelCardScrObj modelScrObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartPrint()
    {
        yield return new WaitForSeconds(modelScrObj.PrintingTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("3DCard"))
        {
            modelScrObj = other.GetComponent<ModelCard>().modelScrObj;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("3DCard"))
        {
            modelScrObj = null;
        }
    }
}
