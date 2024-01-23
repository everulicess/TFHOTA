using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameMenuManager : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance;

    [SerializeField] GameObject menuObjectCanvas;
    [SerializeField] GameObject menuObject;
    [SerializeField] GameObject settingsObject;
    [SerializeField] GameObject controlsObject;
    [SerializeField] GameObject audioObject;
    [SerializeField] InputActionProperty showMenuButtonLeft;
    [SerializeField] InputActionProperty showMenuButtonRight;

    private void Update()
    {
        if (showMenuButtonLeft.action.WasPressedThisFrame() || showMenuButtonRight.action.WasPressedThisFrame())
        {
            Debug.Log("OPEN MENU");

            ShowMenu();

        }
        menuObjectCanvas.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        menuObjectCanvas.transform.LookAt(new Vector3(head.position.x, menuObject.transform.position.y, head.position.z));
        menuObjectCanvas.transform.forward *= -1;
    }

    public void ShowMenu()
    {
        menuObjectCanvas.SetActive(!menuObjectCanvas.activeSelf);
        menuObject.SetActive(menuObjectCanvas.activeSelf);
        settingsObject.SetActive(false);
        controlsObject.SetActive(false);
    }

    public void ShowSettings()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
        menuObject.SetActive(!menuObject.activeSelf);

    }

    public void ShowControls()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
        controlsObject.SetActive(!controlsObject.activeSelf);
    }

    public void ShowAudio()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
        audioObject.SetActive(!audioObject.activeSelf);
    }
}
