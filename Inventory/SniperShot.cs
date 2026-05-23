using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SniperShot : MonoBehaviour
{
    public float damage = 30f;
    public float range = 300f;

    public int maxMagazineAmmo = 5;
    public int maxReserveAmmo = 10;
    private int currentMagazineAmmo;
    private int currentReserveAmmo;
    public TextMeshProUGUI ammoDisplay;
    private bool isReloading = false;

    public Camera fpsCam;
    private Animator parentAnimator;

    private AudioSource audioSource;
    public AudioClip snipersound;
    public AudioClip zoomIN;
    public AudioClip zoomOUT;
    public AudioClip scope;
    public AudioClip sniperReload;

    public float firerate = 2f;
    private float nexttimetofire = 0f;
    public float reloadrate = 0.5f;
    private float nexttimetoreload = 0f;

    public GameObject Sniper;
    public GameObject SniperScope;
    public GameObject Buttons;
    public GameObject SniperButtons;
    public GameObject impact; 
    public GameObject blood;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        parentAnimator = transform.parent?.parent?.parent?.GetComponent<Animator>();
        currentMagazineAmmo = maxMagazineAmmo;
        currentReserveAmmo = maxReserveAmmo;
        UpdateAmmoDisplay();
    }
    public void Shoot()
    {
        if (currentMagazineAmmo > 0 && isReloading == false)
        {
            if (Time.time >= nexttimetofire)
            {
                nexttimetofire = Time.time + 1f / firerate;
                audioSource.clip = snipersound;
                audioSource.Play();
                parentAnimator.Play("rifleRecoil", 0, 0f);
                currentMagazineAmmo--;
                UpdateAmmoDisplay();
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
                {
                    IDamagable target = hit.transform.GetComponent<IDamagable>();
                    if (target != null)
                    {
                        target.DecreaseHealth(damage);
                        GameObject impactGO = Instantiate(blood, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impactGO, 1.5f);
                    }
                    else
                    {
                        GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(impactGO, 1.5f);
                    }
                }
            }

        }
    }

    public void ScopeActive()
    { 
        Sniper.SetActive(false);
        SniperScope.SetActive(true);
        Buttons.SetActive(false);
        SniperButtons.SetActive(true);

        audioSource.clip = scope;
        audioSource.Play();
    }
    public void ScopeInactive()
    {
        Sniper.SetActive(true);
        SniperScope.SetActive(false);
        Buttons.SetActive(true);
        SniperButtons.SetActive(false);
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
    public void Reload()
    {
        StartCoroutine(Reloading());
    }
    IEnumerator Reloading()
    {
        if (currentReserveAmmo > 0 && isReloading == false)
        {
            if (Time.time >= nexttimetoreload)
            {
                nexttimetoreload = Time.time + 1f / reloadrate;
                isReloading = true;
                audioSource.clip = sniperReload;
                audioSource.Play();
                parentAnimator.Play("sniperRELOAD", 0, 0f);
                yield return new WaitForSeconds(2.0f);
                int ammoNeeded = maxMagazineAmmo - currentMagazineAmmo;

                if (currentReserveAmmo >= ammoNeeded)
                {
                    currentReserveAmmo -= ammoNeeded;
                    currentMagazineAmmo = maxMagazineAmmo;
                }
                else
                {
                    currentMagazineAmmo += currentReserveAmmo;
                    currentReserveAmmo = 0;
                }
                UpdateAmmoDisplay();
                isReloading = false;
            }
        }
    }
    public void UpdateAmmoDisplay()
    {

        ammoDisplay.text = "Ammo: " + currentMagazineAmmo + " / " + currentReserveAmmo;
    }
}

 
