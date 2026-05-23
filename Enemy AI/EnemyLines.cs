
using TMPro;
using UnityEngine;

public class EnemyLines : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip dead;
    public AudioClip hurt;
    public AudioClip detect1;
    public AudioClip detect2;
    public AudioClip detect3;
    public AudioClip invest1;
    public AudioClip invest2;

    private Enemy enm;

    private bool hasbeencalled = false;
    private bool hasbeencalled1 = false;
    private bool hasbeencalled2 = false;

    public GameObject gb1;
    public GameObject gb2;
    public GameObject gb3;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        enm = GetComponentInParent<Enemy>();

        audioSource1 = gb1.GetComponent<AudioSource>();
        audioSource2 = gb2.GetComponent<AudioSource>();
        audioSource3 = gb3.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (enm.health < 30f && enm.health > 0f)
        {
            if (hasbeencalled2 == false)
            {
                hasbeencalled2 = true;
                audioSource.clip = hurt;
                audioSource.Play();
            }
        }
        else if (enm.health <= 0f)
        {
            if (hasbeencalled == false)
            {
                hasbeencalled = true;
                audioSource.clip = dead;
                audioSource.Play();
                audioSource.enabled = false;
            }
        }
        else if (enm.canseeplayer == true)
        {
            if (hasbeencalled1 == false)
            {
                hasbeencalled1 = true;
                int randomAnimation = Random.Range(0, 3);
                switch (randomAnimation)
                {
                    case 0:
                        audioSource.clip = detect1;
                        audioSource.Play();
                        break;
                    case 1:
                        audioSource.clip = detect2;
                        audioSource.Play();
                        break;
                    case 2:
                        audioSource.clip = detect3;
                        audioSource.Play();
                        break;
                }
            }
        }
        else if (enm.canseeplayer == false && (audioSource1.isPlaying && Vector3.Distance(transform.position, enm.playerinves.transform.position) <= enm.detectionRange1 || audioSource2.isPlaying && audioSource2.clip == enm.targetclip1 && Vector3.Distance(transform.position, enm.playerinves.transform.position) <= enm.detectionRange2 || audioSource3.isPlaying && audioSource3.clip == enm.targetclip2 && Vector3.Distance(transform.position, enm.playerinves.transform.position) <= enm.detectionRange2))
        {
            int randomAnimation = Random.Range(0, 2);
            switch (randomAnimation)
            {
                case 0:
                    audioSource.clip = invest1;
                    audioSource.Play();
                    break;
                case 1:
                    audioSource.clip = invest2;
                    audioSource.Play();
                    break;
            }
        }
    }
}
