using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    //animations
    public Animator anim;

    public bool walkingAnim;
    public bool idleAnim;
    public bool attack1Anim;
    public bool attack2Anim;

    public bool summon;
    public bool summoning;
    public Transform[] summonSpots;
    public GameObject summoners;

    bool AnimatorIsPlayingWalk()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Walking (3)");
    }
    bool AnimatorIsPlayingIdle()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Mutant Breathing Idle");
    }
    bool AnimatorIsPlayingAttack1()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("attack1");
    }
    bool AnimatorIsPlayingAttack2()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("attack2");
    }
    bool AnimatorIsPlayingDead()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Standing React Death Right");
    }

    private bool rotating;
    private bool walking;

    public float damage1 = 5;
    public float damage2 = 10;

    public Transform attack1Check;
    public Transform attack2Check;
    public float attack1Range;
    public float attack2Range;

    public float attackrate = 10f;
    public float nextAttack = 0.0f;
    public float attackRange;

    private GameObject player;
    private Transform[] randomLocs;
    public GameObject enemySpawner;

    private NavMeshAgent myNMagent;

    public float hp;
    public float maxHP;

    public Image hpBar;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myNMagent = this.GetComponent<NavMeshAgent>();
        myNMagent.autoBraking = false;
        StartCoroutine(waitForRotate());
        StartCoroutine(waitForWalk());
    }

    private void Update()
    {
        GameObject.FindGameObjectWithTag("bossHpbar").SetActive(true);
        hpBar = GameObject.FindGameObjectWithTag("bossHp").GetComponent<Image>();
        hpBar.fillAmount = hp / maxHP;
        walkingAnim = AnimatorIsPlayingWalk();
        idleAnim = AnimatorIsPlayingIdle();
        attack1Anim = AnimatorIsPlayingAttack1();
        attack2Anim = AnimatorIsPlayingAttack2();

        if (Time.time > nextAttack && Vector3.Distance(player.transform.position, transform.position) <= attackRange && !summoning)
        {
            Debug.Log("BOSS ATTACK");
            float random = Random.value;
            if (random <= 0.1f)
            {
                nextAttack = Time.time + attackrate;
                StartCoroutine(hitAttack());
            }
            else if (random > 0.1f && random <= 0.85f)
            {
                StartCoroutine(hitAttack());
                nextAttack = Time.time + attackrate;
            }
            else if (random > 0.85f)
            {
                StartCoroutine(jumpAttack());
                nextAttack = Time.time + attackrate;
            }
        }

        if(hp <= 0)
        {
            if (!AnimatorIsPlayingDead())
            {
                anim.SetTrigger("Dead");
            }
        }
        if(summon)
        {
            summon = false;
            SummonEnemys();
        }

        if (rotating)
        {
            //Debug.Log("ROTATING");
            RotateToPlayer();
        }
        if (walking && Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
           // Debug.Log("WALKING");
            transform.Translate(Vector3.forward * myNMagent.speed * Time.deltaTime);
            RotateToPlayer();
            if (!walkingAnim)
            {
                PlayWalk();
            }
        }
        else if (walking && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            //Debug.Log("NOT WALKING");
            if (!attack1Anim && !attack2Anim)
            {
                PlayIdle();
            }
        }
    }

    void RotateToPlayer()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = player.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = 5 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        newDirection.y = 0;
        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    IEnumerator waitForRotate()
    {
        if (hp > 0 && !summoning)
        {
            yield return new WaitForSeconds(1.5f);
            rotating = true;
            yield return new WaitForSeconds(1.5f);
            rotating = false;
            StartCoroutine(waitForRotate());
        }
    }
    IEnumerator waitForWalk()
    {
        if (hp > 0 && !summoning)
        {
            yield return new WaitForSeconds(2.0f);
            walking = true;
            yield return new WaitForSeconds(5f);
            walking = false;
            if (!attack1Anim && !attack2Anim)
            {
                PlayIdle();
            }
            StartCoroutine(waitForWalk());
        }
    }

    IEnumerator hitAttack()
    {
        if (hp > 0)
        {
            Debug.Log("ATTACK1");
            walking = false;
            PlayAttack1();
            yield return new WaitForSeconds(1.3f);
            if (Vector3.Distance(player.transform.position, attack1Check.position) <= attack1Range)
            {
                player.GetComponent<PlayerEvents>().hp -= damage1;
                Vector3 direction = player.transform.position - attack1Check.position;
                player.GetComponent<Rigidbody>().AddForce(direction * 500f);
                player.GetComponent<HitEffect>().onHitPlayer();
            }
        }
    }

    IEnumerator jumpAttack()
    {
        if (hp > 0)
        {
            walking = false;
            Debug.Log("ATTACK2");
            PlayAttack2();
            yield return new WaitForSeconds(1.5f);
            if (Vector3.Distance(player.transform.position, attack2Check.position) <= attack2Range)
            {
                player.GetComponent<PlayerEvents>().hp -= damage2;
                Vector3 direction = player.transform.position - attack2Check.position;
                player.GetComponent<Rigidbody>().AddForce(direction * 1000f);
                player.GetComponent<HitEffect>().onHitPlayer();
            }
        }
    }

    IEnumerator summonProcedure()
    {
        yield return new WaitForSeconds(10.0f);
        for (int i = 0; i < summonSpots.Length; i++)
        {
            Instantiate(summoners, summonSpots[i].transform.position, Quaternion.identity);
        }
    }

    void SummonEnemys()
    {
        summoning = true;
        anim.SetTrigger("Summon");
        StartCoroutine(summonProcedure());
    }

    void PlayWalk()
    {
        if(!walkingAnim && !summoning)
        {
            anim.SetTrigger("Walking");
            Debug.Log("WALKING");
        }
    }
    void PlayIdle()
    {
        if (!idleAnim && !AnimatorIsPlayingDead() && !summoning)
        {
            anim.SetTrigger("idle");
        }
    }
    void PlayAttack1()
    {
        if (!attack1Anim)
        {
            anim.SetTrigger("attack1");
        }
    }
    void PlayAttack2()
    {
        if (!attack2Anim)
        {
            anim.SetTrigger("attack2");
        }
    }
}
