using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeUse : MonoBehaviour
{
    public float damage = 30f;
    public float range = 3f;

    public float firerate = 4f;
    private float nexttimetofire = 0f;

    public Camera fpsCam;

    private AudioSource audioSource;
    public AudioClip knifesound;

    private Animator animator;
    public GameObject blood;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    public void Stab()
    {
        if (Time.time >= nexttimetofire)
        {
            nexttimetofire = Time.time + 1f / firerate;
            audioSource.clip = knifesound;
            audioSource.Play();
            animator.Play("knifemov", 0, 0f);
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
            }
        }
    }
}
