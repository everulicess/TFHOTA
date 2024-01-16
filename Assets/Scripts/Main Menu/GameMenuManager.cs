using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameMenuManager : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance;

    [SerializeField] GameObject menuObject;
    [SerializeField] GameObject settingsObject;
    [SerializeField] InputActionProperty showMenuButtonLeft;
    [SerializeField] InputActionProperty showMenuButtonRight;
    private void Start()
    {
        
    }
    private void Update()
    {
        HandleShowMenu();
    }

    private void HandleShowMenu()
    {
        if (showMenuButtonLeft.action.WasPressedThisFrame() || showMenuButtonRight.action.WasPressedThisFrame())
        {
            menuObject.SetActive(!menuObject.activeSelf);

            menuObject.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        menuObject.transform.LookAt(new Vector3(head.position.x, menuObject.transform.position.y, head.position.z));
        menuObject.transform.forward *= -1;
    }

    public void ShowSettings()
    {
        settingsObject.SetActive(!settingsObject.activeSelf);
    }

}
