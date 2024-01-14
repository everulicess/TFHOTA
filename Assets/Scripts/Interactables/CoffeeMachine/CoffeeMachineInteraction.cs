using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CoffeeMachineInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem coffeeParticleSystem;
    [SerializeField] GameObject cupPrefab;
    [SerializeField] Transform spawnPos;


    [Header("Settings")]
    public float pouringTime;


    GameObject preparingCup;

    bool isCup = false;
    private void Start()
    {
        coffeeParticleSystem.Stop();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetCoffee();
        }
        
    }
    public void GetCoffee()
    {
        //GameObject _cupPrefab = cupPrefab;
        //ParticleSystem _coffeeParticleSystem = coffeeParticleSystem;
        //check if there is a mug
        if (isCup) return;

        //spawn paper cup
        preparingCup = Instantiate(cupPrefab, spawnPos.position, transform.rotation);
        isCup = true;

        //start pouring coffe
        StartCoroutine(PourCoffee(coffeeParticleSystem));
    }
    public IEnumerator PourCoffee(ParticleSystem _coffeeParticleSystem)
    {
        //disable the mug to be grabbable
        preparingCup.GetComponent<XRGrabInteractable>().enabled = false;
        //Sounds for making coffee
        
        yield return new WaitForSeconds(2f);

        _coffeeParticleSystem.Play();
        yield return new WaitForSeconds(3f);

        StartCoroutine(preparingCup.GetComponent<CoffeeMug>().FillingCup(pouringTime));
        yield return new WaitForSeconds(pouringTime);

        _coffeeParticleSystem.Stop();
        preparingCup.GetComponent<XRGrabInteractable>().enabled = true;


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Cup")
        {
            isCup = true;
            //Debug.Log("THERE IS A CUP HERE I CAN'T PUT A NEW COFFEE");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Cup")
        {
            isCup = false;
            preparingCup = null;

        }
    }
}
