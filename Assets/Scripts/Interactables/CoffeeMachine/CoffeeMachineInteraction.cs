using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject coffee;
    [SerializeField] GameObject cup;

    [Header("Settings")]
    public float pouringTime;


    bool isCup = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetCoffee(cup, coffee);
        }
    }

    public void GetCoffee(GameObject _cup, GameObject _coffee)
    {
        //check if there is a mug
        if (isCup) return;

        //spawn paper cup
        Instantiate(_cup,this.gameObject.transform.position,this.transform.rotation, this.transform);

        //start pouring coffe
        Debug.Log("POURING COFFEE");
    }
}
