using System.Collections;
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
        mat.SetFloat("_Fill", -1.5f); ;
    }
    
    public IEnumerator FillingCup(float _timeToFillCup)
    {
        //Debug.LogError("is Filling The Cup NOW");
        while (fill<0.70f)
        {
            fill += (Time.deltaTime / (_timeToFillCup+3f));
            mat.SetFloat("_Fill", fill);
            yield return new WaitForSeconds(0.002f);

        }

    }
}
