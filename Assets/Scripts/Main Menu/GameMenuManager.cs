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

    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle audioToggle;
    private AudioManager audioManager;
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
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

    // called when changing volume
    public void OnValueChanged()
    {
        audioManager.VolumeControl(volumeSlider.value);
    }

    //public void ToggleSound()
    //{
    //    audioManager.ToggleSound(audioToggle.isOn);
    //    volumeSlider.enabled = audioToggle.isOn;
    //}

    //public void ToggleMusic()
    //{
    //    audioManager.ToggleMusic(musicToggle.isOn);
    //}
}
