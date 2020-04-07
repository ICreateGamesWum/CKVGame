using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Boss1 : MonoBehaviour
{

    public Animator anim;

    public bool walking;
    public bool idleAnim;
    public bool attack1Anim;
    public bool attack2Anim;

    public float hp;
    public float maxHP;

    public Image hpBar;

    bool AnimatorIsPlayingWalk()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Walking (3)");
    }
    bool AnimatorIsPlayingidle()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName("Mutant Breathing Idle");
    }
    public bool isWalking;
    public bool isIdle;

    public float damage = 1;

    public float attackrate = 10f;
    public float nextAttack = 0.0f;
    public float attackRange;

    private GameObject player;
    private Transform[] randomLocs;
    public GameObject enemySpawner;

    public float nextPlLastKnownLoc = 2f;
    public float plLastKnownLocRate = 2f;
    public Vector3 plLastKnownLoc;

    private NavMeshAgent myNMagent;

    private bool rotating;
    private bool walkingBool;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //if(!isWalking)//niet al aan het lopen en bewegen
        //{
        //    //walking = true;
        //}
        //else//en niet bewegen
        //{
        //    idleAnim = true;
        //}
        //if (walking)
        //{
        //    walking = false;
        //    walk();
        //}
        //if (idleAnim && !isIdle)
        //{
        //    idleAnim = false;
        //    idle();
        //}
        //if (attack1Anim)
        //{
        //    attack1Anim = false;
        //    attack1();
        //}
        //if (attack2Anim)
        //{
        //    attack2Anim = false;
        //    attack2();
        //}
    }

    //ECHTE SCRIPT BEGINT HIER. ALLES WAT HIERBOVEN STAAT IS INSPIRATIE

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        plLastKnownLoc = player.transform.position;
        myNMagent = this.GetComponent<NavMeshAgent>();
        myNMagent.autoBraking = false;
        StartCoroutine(waitForRotate());
        StartCoroutine(waitForWalk());
    }

    private void Update()
    {
        hpBar.fillAmount = hp / maxHP;
        isWalking = AnimatorIsPlayingWalk();
        isIdle = AnimatorIsPlayingidle();

        if (Time.time > nextAttack && Vector3.Distance(player.transform.position, transform.position) <= attackRange)
        {
            Debug.Log("BOSS ATTACK");
            float random = Random.value;
            if(random <= 0.1f)
            {
                nextAttack = Time.time + attackrate;
                attack1();
            }
            else if (random > 0.1f && random <= 0.85f)
            {
                attack1();
                nextAttack = Time.time + attackrate;
            }
            else if(random > 0.85f)
            {
                attack2();
                nextAttack = Time.time + attackrate;
            }
        }
        if(rotating && isIdle && isWalking)
        {
            Debug.Log("ROTATING");
            RotateToPlayer();
        }
        if (walkingBool && Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            Debug.Log("WALKING");
            transform.Translate(Vector3.forward * myNMagent.speed * Time.deltaTime);
            if (!isWalking)
            {
                walk();
            }
        }
        else if (walkingBool && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            Debug.Log("NOT WALKING");
            if (isWalking && !isIdle)
            {
                idle();
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
        yield return new WaitForSeconds(3.5f);
        rotating = true;
        yield return new WaitForSeconds(1.5f);
        rotating = false;
        StartCoroutine(waitForRotate());
    }
    IEnumerator waitForWalk()
    {
        yield return new WaitForSeconds(3.5f);
        walkingBool = true;
        yield return new WaitForSeconds(5f);
        if (!isIdle)
        {
            idle();
        }
        walkingBool = false;
        StartCoroutine(waitForWalk());
    }

    IEnumerator hitAttack()
    {
        yield return new WaitForSeconds(0.5f);
        //maak een empty object voor de boss, in de boss parent.
        //check daar
        //check of player in de buurt is van hitpoint
        //als dat zo is, damage
    }

    IEnumerator jumpAttack()
    {
        yield return new WaitForSeconds(1.0f);
        //maak een empty object voor de boss, in de boss parent.
        //check daar
        //check of player in de buurt is van hitpoint
        //als dat zo is, damage
    }

    void walk()
    {
        anim.SetTrigger("Walking");
    }

    void attack1()
    {
        anim.SetTrigger("attack1");
        StartCoroutine(hitAttack());
    }
    void attack2()
    {
        anim.SetTrigger("attack2");
        StartCoroutine(jumpAttack());
    }

    void idle()
    {
        anim.SetTrigger("idle");
    }
}
