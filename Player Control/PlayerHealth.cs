using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public Slider slider;

    public GameObject playerbody;
    private Animator parentAnimator;

    public GameObject move;
    public GameObject look;
    public GameObject buttons;
    public GameObject hitscreen;
    public GameObject inventory;
    public GameObject scopes1;
    public GameObject scopes2;

    private AudioSource audioSource;
    public AudioClip hurt;
    public AudioClip dead;
    public GameObject hurtscreen;

    public float nexttimetopain = 0f;
    public float painrate = 1f;

    public GameObject black;
    void Start()
    {
        parentAnimator = transform.parent?.parent?.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        slider.value = health;
        if (health > 0f && health < 100f)
        {
            if (Time.time >= nexttimetopain)
            {
                nexttimetopain = Time.time + 1f / painrate;
                audioSource.clip = hurt;
                audioSource.Play();
                StartCoroutine(HurtScreen());
            }
        }
        else if (health <= 0)
        {
            audioSource.clip = dead;
            audioSource.Play();
            Death();
        }
    }
    private IEnumerator HurtScreen()
    {
        yield return new WaitForSeconds(0.8f);
        hurtscreen.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        hurtscreen.SetActive(false);
    }
    void Death()
    {
        buttons.SetActive(false);
        move.SetActive(false);
        look.SetActive(false);
        inventory.SetActive(false);
        scopes1.SetActive(false);
        scopes2.SetActive(false);

        hitscreen.SetActive(true);
        Destroy(playerbody, 5f);
        parentAnimator.Play("death", 0, 0f);
        StartCoroutine(DisableCameraAfterDelay());
    }
    IEnumerator DisableCameraAfterDelay()
    {
        yield return new WaitForSeconds(4f);  
        black.SetActive(true);  
    }
}
