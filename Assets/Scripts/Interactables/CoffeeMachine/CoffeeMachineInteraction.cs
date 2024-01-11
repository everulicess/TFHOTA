using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachineInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem coffee;
    [SerializeField] GameObject cupPrefab;
    [SerializeField] Transform spawnPos;


    [Header("Settings")]
    public float pouringTime;


    GameObject preparingCup;

    bool isCup = false;
    private void Start()
    {
        coffee.Stop();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GetCoffee(cupPrefab, coffee);
        }
        
    }
    public void GetCoffee(GameObject _cupPrefab, ParticleSystem _coffeeParticleSystem)
    {
        //check if there is a mug
        if (isCup) return;

        //spawn paper cup
        preparingCup = Instantiate(_cupPrefab, spawnPos.position, transform.rotation);
        isCup = true;

        //start pouring coffe
        StartCoroutine(PourCoffee(_coffeeParticleSystem));
    }
    public IEnumerator PourCoffee(ParticleSystem _coffeeParticleSystem)
    {
        Debug.Log("PREPARING COFFEE");
        
        yield return new WaitForSeconds(2f);
        Debug.Log("Deleting cup");
        _coffeeParticleSystem.Play();
        yield return new WaitForSeconds(3f);

        StartCoroutine(preparingCup.GetComponent<CoffeeMug>().FillingCup(pouringTime));
        yield return new WaitForSeconds(pouringTime);
        _coffeeParticleSystem.Stop();
        Debug.Log("finished");
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
