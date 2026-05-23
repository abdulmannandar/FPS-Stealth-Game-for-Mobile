using System.Collections;
using TMPro;
using UnityEngine;

public class PistolShot : MonoBehaviour
{
    public float damage = 30f;
    public float range = 50f;

    public int maxMagazineAmmo = 12;  
    public int maxReserveAmmo = 30;
    private int currentMagazineAmmo;  
    private int currentReserveAmmo;
    public TextMeshProUGUI ammoDisplay;
    private bool isReloading = false;

    public float firerate = 3f;
    private float nexttimetofire = 0f;
    public float reloadrate = 0.5f;
    private float nexttimetoreload = 0f; 

    public Camera fpsCam;

    private AudioSource audioSource;
    public AudioClip pistolsound;
    public AudioClip pistolreload;

    private Animator animator; 
    public GameObject impact;
    public GameObject blood;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
                audioSource.clip = pistolsound;
                audioSource.Play();
                animator.Play("pistolanim", 0, 0f);
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
    public void Reload()
    {
        StartCoroutine(Reloading());
    }
    IEnumerator Reloading()
    {
        if(currentReserveAmmo > 0 && isReloading == false)
        {
            if(Time.time >= nexttimetoreload)
            { 
                nexttimetoreload = Time.time + 1f / reloadrate;
                isReloading = true;
                audioSource.clip = pistolreload;
                audioSource.Play();
                animator.Play("pistolRELOAD", 0, 0f);
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
