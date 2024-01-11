using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMug : MonoBehaviour
{
    new string name = "Cup";
    [SerializeField]GameObject coffeeObject;

    Material mat;
    float fill = -1f;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.name = name;
        mat = coffeeObject.GetComponent<Renderer>().material;
        mat.SetFloat("_Fill", -0.9f); ;
        Debug.LogError("doing the cup thing");
    }
    //public void FillCup(float _timeToFillCup)
    //{
    //    StartCoroutine()
    //}
    
    public IEnumerator FillingCup(float _timeToFillCup)
    {
        Debug.LogError("is Filling The Cup NOW");
        while (fill<1f)
        {
            fill += (Time.deltaTime / (_timeToFillCup+3f));
            Debug.LogError(fill);
            mat.SetFloat("_Fill", fill);
            yield return new WaitForSeconds(0.002f);

        }

    }
    private float UpdateFill()
    {
        
        
        return fill;
    }
}
