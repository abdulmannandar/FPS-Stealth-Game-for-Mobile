using UnityEngine;
using System.Collections;
using TMPro;

public class RifleGun : MonoBehaviour
{
    public float damage = 7f;
    public float range = 80f;

    public int maxMagazineAmmo = 30;
    public int maxReserveAmmo = 200;
    private int currentMagazineAmmo;
    private int currentReserveAmmo;
    public TextMeshProUGUI ammoDisplay;
    private bool isReloading = false;

    public float firerate = 8f;
    private float nexttimetofire = 0f;
    public float reloadrate = 0.5f;
    private float nexttimetoreload = 0f;

    public Camera fpsCam;

    public GameObject m4flash;
    public float flashduration;
    private Renderer muzzleFlashRenderer;
    public Light muzzleFlashLight;

    private AudioSource audioSource;
    public AudioClip riflesound;
    public AudioClip rifleReload;

    private Animator animator;
    private Animator parentAnimator;
    public GameObject impact;
    public GameObject blood;

    public bool Pressed;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        parentAnimator = transform.parent?.parent?.parent?.GetComponent<Animator>();

        GameObject muzzleFlash = Instantiate(m4flash, transform); 
        muzzleFlashRenderer = muzzleFlash.GetComponent<Renderer>(); 

        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;

        currentMagazineAmmo = maxMagazineAmmo;
        currentReserveAmmo = maxReserveAmmo;
        UpdateAmmoDisplay();
    }
    void Update()
    {
        if (Pressed && currentMagazineAmmo > 0 && isReloading == false)
        {
            if (Time.time >= nexttimetofire)
            {
                nexttimetofire = Time.time + 1f / firerate;
                StartCoroutine(ShowMuzzleFlash());
                audioSource.clip = riflesound;
                audioSource.Play();
                animator.Play("riflemov", 0, 0f);
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
    private IEnumerator ShowMuzzleFlash()
    {
        muzzleFlashRenderer.enabled = true;
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(flashduration);  
        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false; 
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
                audioSource.clip = rifleReload;
                audioSource.Play();
                animator.Play("rifleRELOAD", 0, 0f);
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


