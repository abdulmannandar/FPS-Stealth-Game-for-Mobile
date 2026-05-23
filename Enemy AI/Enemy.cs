
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamagable
{
    public float health = 30f;
    public float range = 500f;
    public GameObject EnemyBody;

    NavMeshAgent agent;
    public Transform playertransform;
    public GameObject shootpoint;

    private Animator animator;

    public Transform[] patrolpoints;
    public int targetpoint;

    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canseeplayer = false;

    public float damage = 5f;
    public float firerate = 8f;
    private float nexttimetofire = 0f;
    public GameObject m4flash;
    public float flashduration;
    private Renderer muzzleFlashRenderer;
    public Light muzzleFlashLight;
    private AudioSource audioSource;
    public AudioClip riflesound;
    public GameObject enemy1;
    public Transform gun;

    public GameObject gb1;  
    private AudioSource audioSource1;
    public float detectionRange1 = 20f;
    public GameObject gb2;
    private AudioSource audioSource2;
    public AudioClip targetclip1;
    public GameObject gb3;
    private AudioSource audioSource3;
    public AudioClip targetclip2;
    public float detectionRange2 = 100f;
    private Vector3 targetPosition;

    public GameObject playerinves;

    private bool hasbeencalled = false;
    public bool investigating = false;
    public bool playerAlive = true;

    public AudioClip footstep1;

    public float footstepInterval = 0.69f;
    private float footstepTimer = 0f;
    private void Start()
    {
        GameObject muzzleFlash = Instantiate(m4flash, gun.position, gun.rotation);
        muzzleFlash.transform.SetParent(gun);
        muzzleFlashRenderer = muzzleFlash.GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;

        playerRef = GameObject.FindGameObjectWithTag("detectionbody");
        StartCoroutine(FOVRoutine());

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        targetpoint = 0;

        audioSource1 = gb1.GetComponent<AudioSource>();
        audioSource2 = gb2.GetComponent<AudioSource>();
        audioSource3 = gb3.GetComponent<AudioSource>();
    }
    void Update()
    {
        if (canseeplayer == true && playerAlive == true)
        {
            ChaseAndCombat();
        }
        else if (canseeplayer == false && (audioSource1.isPlaying && Vector3.Distance(transform.position, playerinves.transform.position) <= detectionRange1 || audioSource2.isPlaying && audioSource2.clip == targetclip1 && Vector3.Distance(transform.position, playerinves.transform.position) <= detectionRange2 || audioSource3.isPlaying && audioSource3.clip == targetclip2 && Vector3.Distance(transform.position, playerinves.transform.position) <= detectionRange2))
        {
            investigating = true;
            Investigate();
        }
        else if (investigating == true)
        {
            if (health < 30)
            {
                agent.speed = 0.5f;
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                investigating = false;
            }
        }
        else if (investigating == false)
        {
            Patrol();
        }
        if (agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance)
        {
            footstepTimer -= Time.deltaTime;

            
            if (footstepTimer <= 0f)
            {
                audioSource.clip = footstep1;
                audioSource.Play();
                footstepTimer = footstepInterval; 
            }
        }
        else
        {
            footstepTimer = 0f; 
        }
    }
    public void Investigate()
    {
        investigating = true;
        agent.stoppingDistance = 3f;
        agent.speed = 1f;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        targetPosition = playerinves.transform.position;
        agent.SetDestination(targetPosition);
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Detect();
        }
    }
    private void Detect()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canseeplayer = true;
                }
            }
        }
    }
    public void ChaseAndCombat()
    {
        canseeplayer = true;
        EnemyManager.Instance.TriggerInvestigation();
        agent.stoppingDistance = 2f;
        agent.speed = 4f;
        agent.destination = playertransform.position;
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (Time.time >= nexttimetofire)
        {
            nexttimetofire = Time.time + 1f / firerate;
            StartCoroutine(ShowMuzzleFlash());
            audioSource.clip = riflesound;
            audioSource.Play();
            RaycastHit hit;
            Vector3 directionToTarget = (playertransform.position - shootpoint.transform.position).normalized;
            if (Physics.Raycast(shootpoint.transform.position, directionToTarget, out hit, range))
            {
                PlayerHealth target = hit.transform.GetComponent<PlayerHealth>();
                if (target != null)
                {
                    if(target.health > 0)
                    {
                        target.TakeDamage(damage);
                    }
                    else
                    {
                        playerAlive = false;
                    }
                }
            }
        }

        Vector3 directionToPlayer = playertransform.position - transform.position;
        directionToPlayer.y = 0;
        Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = rotationToPlayer;
    }
    private IEnumerator ShowMuzzleFlash()
    {
        muzzleFlashRenderer.enabled = true;
        muzzleFlashLight.enabled = true;
        yield return new WaitForSeconds(flashduration);
        muzzleFlashRenderer.enabled = false;
        muzzleFlashLight.enabled = false;
    }
    void Patrol()
    {
        agent.stoppingDistance = 1f;
        if (health < 30 && playerAlive == true)
        {
            agent.speed = 0.5f;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }else
        {
            agent.speed = 1f;
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            targetpoint++;
            if(targetpoint >= patrolpoints.Length)
            {
                targetpoint = 0;
            }
        }
        agent.destination = patrolpoints[targetpoint].position;
    }
    public void DecreaseHealth(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            if (hasbeencalled == false)
            {
                Death();
            }
        }
        else
        {
            StartCoroutine(Action());
        }
    }
    private IEnumerator Action()
    {
        yield return new WaitForSeconds(1.7f);
        ChaseAndCombat();
    }
    void Death()
    {
        hasbeencalled = true;
        int randomAnimation = Random.Range(0, 3);
        switch (randomAnimation)
        {
            case 0:
                animator.Play("die1", 0, 0f);
                break;
            case 1:
                animator.Play("die2", 0, 0f);
                break;
            case 2:
                animator.Play("die3", 0, 0f);
                break;
        }
        agent.ResetPath();
        this.enabled = false;
        Destroy(EnemyBody, 6f);
    }
}
