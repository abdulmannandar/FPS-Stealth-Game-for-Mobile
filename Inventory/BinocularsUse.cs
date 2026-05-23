using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinocularsUse : MonoBehaviour
{
    public Camera fpsCam;

    private AudioSource audioSource;
    public AudioClip zoomIN;
    public AudioClip zoomOUT;
    public AudioClip scope;

    public GameObject Burnoculars;
    public GameObject BurnoScope;
    public GameObject Buttons;
    public GameObject BurnoButtons;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        float Sensitivity = SensitivityManager.Instance.Sensitivity;
    }

    public void ScopeActive()
    {
        Burnoculars.SetActive(false);
        BurnoScope.SetActive(true);
        Buttons.SetActive(false);
        BurnoButtons.SetActive(true);

        audioSource.clip = scope;
        audioSource.Play();
    }
    public void ScopeInactive()
    {
        Burnoculars.SetActive(true);
        BurnoScope.SetActive(false);
        Buttons.SetActive(true);
        BurnoButtons.SetActive(false);
        fpsCam.fieldOfView = 60f;

        audioSource.clip = scope;
        audioSource.Play();
    }
    public void ZoomIn()
    {
        fpsCam.fieldOfView -= 10;
        fpsCam.fieldOfView = Mathf.Clamp(fpsCam.fieldOfView, 1f, 179f);
        audioSource.clip = zoomIN;
        audioSource.Play();
    }
    public void ZoomOut()
    {
        if (fpsCam.fieldOfView < 60f)
        {
            fpsCam.fieldOfView += 10;
            fpsCam.fieldOfView = Mathf.Clamp(fpsCam.fieldOfView, 1f, 179f);
            audioSource.clip = zoomOUT;
            audioSource.Play();
        }
    }
}
