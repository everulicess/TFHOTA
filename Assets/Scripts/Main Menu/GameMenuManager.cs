using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameMenuManager : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance;

    [SerializeField] GameObject menuObjectCanvas;
    [SerializeField] GameObject menuObject;
    [SerializeField] GameObject settingsObject;
    [SerializeField] GameObject controlsObject;
    [SerializeField] InputActionProperty showMenuButtonLeft;
    [SerializeField] InputActionProperty showMenuButtonRight;
    private void Start()
    {
        
    }
    private void Update()
    {

        if (showMenuButtonLeft.action.WasPressedThisFrame() || showMenuButtonRight.action.WasPressedThisFrame())
        {
            Debug.Log("OPEN MENU");

            ShowMenu();

        }
        menuObjectCanvas.transform.position = head.position + new Vector3(head.forward.x, 3, head.forward.z).normalized * spawnDistance;
        menuObjectCanvas.transform.LookAt(new Vector3(head.position.x, menuObject.transform.position.y, head.position.z));
        menuObjectCanvas.transform.forward *= -1;
    }

    public void ShowMenu()
    {
        menuObjectCanvas.SetActive(!menuObjectCanvas.activeSelf);
        menuObject.SetActive(menuObjectCanvas.activeSelf);
    }

    public void ShowSettings()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
    }

    public void ShowControls()
    {
        controlsObject.SetActive(!controlsObject.activeSelf);
    }

}
