using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class BalloonPoppingDevice : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20f;

    [SerializeField] TextMeshPro bulletsAmountLeft;
    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
        bulletsAmountLeft.text = timesFired.ToString();
    }

    public int timesFired = 0;

    public void FireBullet(ActivateEventArgs arg)
    {
        if (timesFired == 0) return;

        timesFired--;
        GameObject spawnObject = Instantiate(bullet);
        spawnObject.transform.position = spawnPoint.position;
        spawnObject.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnObject, 3f);

        bulletsAmountLeft.text = timesFired.ToString();

    }


}
