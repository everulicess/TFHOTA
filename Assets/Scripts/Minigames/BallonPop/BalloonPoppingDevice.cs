using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class BalloonPoppingDevice : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 10f;

    public static event Action  NoBulletsLeft;
    [SerializeField] TextMeshPro bulletsAmountLeft;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        bulletsAmountLeft.text = timesFired.ToString();
    }

    public int timesFired;
    private void Update()
    {
        if (CheckBulletsLeft())
        {
            NoBulletsLeft?.Invoke();
        } 
    }
    private bool CheckBulletsLeft()
    {
        return timesFired == 0;
    }
    public void FireBullet(ActivateEventArgs arg)
    {
        if (CheckBulletsLeft()) return;

        timesFired--;
        GameObject spawnObject = Instantiate(bullet);
        spawnObject.transform.position = spawnPoint.position;
        spawnObject.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnObject, 2f);

        bulletsAmountLeft.text = timesFired.ToString();

    }


}
