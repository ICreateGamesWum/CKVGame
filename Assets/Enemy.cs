using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{//DIT SCRIPT IS COMPLETE BULLSHIT. HET WERKT. PUNT. NIET OVERNEMEN. ALLEEN VOOR INSPIRATIE GEBRUIKEN OMDAT DIT KUT GESCHREVEN IS. WAS MN EERSTE KEER... SORRY.
    #region states
    public float damage = 1;

    //targeting
    private GameObject player;
    public bool targeting;
    public bool idle;
    public bool searching;
    public Vector3 searchPos;

    public float plLastKnownLocTime = 2f;
    public float plLastKnownLocStartTime = 2f;
    public Vector3 plLastKnownLoc;
    public float targetRange;
    public float attackRange;
    public float detectRange;
    public float minRange;
    public GameObject arm;
    public bool playerVisible;

    public float searchingSpeed;
    public float targetingSpeed;

    public float angle;
    public float angleNew;

    public bool gotShot;

    public GameObject deathParticle;
    public GameObject hpPickup;

    //patrolling
    public Transform[] points;
    private int destPoint = 0;

    //field of view
    [Range(0,360)]
    public float viewAngle;

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    //shooting
    public float timeBTWShots;
    public float startTimeBTWShots;
    public GameObject Bullet;
    public GameObject shotPoint;

    //hp
    public float hp = 10f;
    public float maxHp = 10f;

    private NavMeshAgent myNMagent;

    private Vector3 startpos;
    private Vector3 idlePos;
    private bool headingBack;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        plLastKnownLoc = player.transform.position;
        myNMagent = this.GetComponent<NavMeshAgent>();
        idle = true;
        myNMagent.autoBraking = false;

        GotoNextPoint();
        startpos = transform.position;
    }

    private void Update()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        targetDir = targetDir.normalized;
        float dot = Vector3.Dot(targetDir, transform.forward);
        angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        float distPlayer = Vector3.Distance(player.transform.position, transform.position);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, targetDir, out hit))
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                playerVisible = true;
            }
            else
            {
                playerVisible = false;
            }
        }

        if(distPlayer <= targetRange && angle <= viewAngle/2 && distPlayer > attackRange && playerVisible)
        {
            idle = false;
            targeting = true;
            myNMagent.isStopped = false;
            myNMagent.speed = targetingSpeed;
            myNMagent.SetDestination(plLastKnownLoc);
        }
        else if(distPlayer <= attackRange && angle <= viewAngle/2 && distPlayer > detectRange && playerVisible)
        {
            idle = false;
            targeting = true;
            myNMagent.isStopped = false;
            myNMagent.speed = targetingSpeed;
            transform.LookAt(player.transform.position);
            arm.transform.LookAt(player.transform.position);
            myNMagent.SetDestination(player.transform.position);
        }
        else if (distPlayer <= detectRange && distPlayer > minRange && playerVisible)
        {
            idle = false;
            targeting = true;
            myNMagent.isStopped = false;
            myNMagent.speed = targetingSpeed;
            transform.LookAt(player.transform.position);
            arm.transform.LookAt(player.transform.position);
            myNMagent.SetDestination(player.transform.position);
        }
        else if (distPlayer <= minRange && playerVisible)
        {
            idle = false;
            targeting = true;
            myNMagent.isStopped = false;
            myNMagent.speed = targetingSpeed;
            transform.LookAt(player.transform.position);
            arm.transform.LookAt(player.transform.position);
            plLastKnownLoc = player.transform.position;
            myNMagent.SetDestination(plLastKnownLoc);
        }
        else
        {
            if(gotShot)
            {
                //Debug.Log("GOTSHOT");
                myNMagent.isStopped = false;
                transform.LookAt(player.transform.position);
                arm.transform.LookAt(player.transform.position);
                plLastKnownLoc = player.transform.position;
                myNMagent.SetDestination(plLastKnownLoc);
                searchPos = plLastKnownLoc;
                searching = true;
                gotShot = false;
                idle = false;
            }

            if(targeting)
            {
                targeting = false;
                makeIdle();
            }
        }

        if (searching && Vector3.Distance(transform.position, searchPos) < 0.5f)
        {
            //Debug.Log("op searchpos");
            searching = false;
            headingBack = true;
            myNMagent.isStopped = false;
            myNMagent.SetDestination(startpos);
            //if (points != null)
            //{
            //    GotoNextPoint();
            //}
            //else
            //{
            //    myNMagent.SetDestination(startpos);
            //}
        }

        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!myNMagent.pathPending && myNMagent.remainingDistance < 0.5f && idle)
        {
            //Debug.Log("checknextpoint");
            if (points.Length > 0)
            {
                GotoNextPoint();
            }
        }
        if(idle && Vector3.Distance(idlePos, transform.position) < 0.5f)
        {
            //Debug.Log("op punt van bestemming idle");
            //kan glitchen als enemy patrollt en dan te dicht bij dat punt komt
            //dan idlePos naar een heeeel ver punt zetten na gotonextpoint of setdestination
            if (points.Length > 0)
            {
                GotoNextPoint();
            }
            else
            {
                myNMagent.isStopped = false;
                myNMagent.SetDestination(startpos);
                headingBack = true;
                idle = false;
            }
        }
        else if(headingBack && Vector3.Distance(startpos, transform.position) < 0.5f)
        {
            myNMagent.isStopped = true;
            headingBack = false;
            idle = true;
            //Debug.Log("STOP");
        }

        if (hp <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            GameObject.FindGameObjectWithTag("GH").GetComponent<ScoreSystem>().AddPoints(10f);
            float random = Random.value;
            Debug.Log(random.ToString());
            if (random <= 0.2f)
            {
                Instantiate(hpPickup, transform.position, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }

        if(targeting)
        {
            if (timeBTWShots <= 0 && distPlayer <= attackRange)
            {
                timeBTWShots = startTimeBTWShots;
                GameObject bullet = Instantiate(Bullet, shotPoint.transform.position, shotPoint.transform.rotation);
                bullet.GetComponent<EnemyBullet>().damage = damage;
            }
            else
            {
                timeBTWShots -= Time.deltaTime;
            }
        }

        if(plLastKnownLocTime <= 0)
        {
            plLastKnownLocTime = plLastKnownLocStartTime;
            plLastKnownLoc = player.transform.position;
        }
        else
        {
            plLastKnownLocTime -= Time.deltaTime;
        }
    }

    public void makeIdle()
    {
        //Debug.Log("makeIdle");
        myNMagent.isStopped = false;
        idle = true;
        plLastKnownLoc = player.transform.position;
        myNMagent.SetDestination(plLastKnownLoc);
        idlePos = plLastKnownLoc;
    }

    void GotoNextPoint()
    {
        myNMagent.isStopped = false;
        //Debug.Log("nextPoint");
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        myNMagent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    #region testing

    //private Transform startTransform;
    //public float multiplyBy;

    ////idle state
    //public Vector3 nextPointToGo;
    //public float wanderTimer = 7f;
    //public float startWanderTimer = 7f;
    //public float wanderSpeed;
    //public Transform target;
    //public float wanderRadius;

    //public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    //{
    //    Vector3 randDirection = Random.insideUnitSphere * dist;

    //    randDirection += origin;

    //    NavMeshHit navHit;

    //    NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

    //    return navHit.position;
    //}

    ////angle = Vector3.Angle(targetDir, Vector3.forward);
    //angle= Mathf.Atan2(targetDir.x, targetDir.z);
    //angleNew = Mathf.Tan(angle);

    //if(wanderTimer <= 0)
    //{
    //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
    //    myNMagent.SetDestination(newPos);
    //    wanderTimer = startWanderTimer;
    //}
    //else
    //{
    //    wanderTimer -= Time.deltaTime;
    //}

    //if(idle)
    //{
    //    myNMagent.speed = wanderSpeed;
    //    if(plLastKnownLoc != null)
    //    {
    //        if(transform.position == plLastKnownLoc)
    //        {
    //            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
    //            myNMagent.SetDestination(newPos);
    //        }
    //    }
    //    else
    //    {
    //        Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
    //        myNMagent.SetDestination(newPos);
    //    }
    //}

    //ignore
    //private IEnumerator makeRun()
    //{
    //    yield return new WaitForSeconds(5f);
    //    StartCoroutine(makeRun());
    //    RunFrom();
    //}

    //public void RunFrom()
    //{

    //    // store the starting transform
    //    startTransform = transform;

    //    //temporarily point the object to look away from the player
    //    transform.rotation = Quaternion.LookRotation(transform.position - player.transform.position);

    //    //Then we'll get the position on that rotation that's multiplyBy down the path (you could set a Random.range
    //    // for this if you want variable results) and store it in a new Vector3 called runTo
    //    Vector3 runTo = transform.position + transform.forward * multiplyBy;
    //    Vector3 runTo2 = transform.position + transform.forward * multiplyBy;
    //    //Debug.Log("runTo = " + runTo);

    //    //So now we've got a Vector3 to run to and we can transfer that to a location on the NavMesh with samplePosition.

    //    NavMeshHit hit;    // stores the output in a variable called hit

    //    // 5 is the distance to check, assumes you use default for the NavMesh Layer name
    //    NavMesh.SamplePosition(runTo, out hit, 10, NavMesh.AllAreas);

    //    //NavMesh.SamplePosition(runTo2, out hit, 10, areaMask: 3 << NavMesh.GetAreaFromName("Hidingspot"));

    //    //Debug.Log("hit = " + hit + " hit.position = " + hit.position);

    //    // reset the transform back to our start transform
    //    transform.position = startTransform.position;
    //    transform.rotation = startTransform.rotation;

    //    // And get it to head towards the found NavMesh position
    //    myNMagent.SetDestination(hit.position);
    //}
    #endregion

    #region trash
    //public GameObject player;

    //public float hp = 10;
    //public float startHp;
    //public float startTimeBTWAttack = 1f;
    //public float timeBTWAttack = 1f;
    //public float attackDamage = 1f;

    //public ScoreSystem scoreSystem;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    startHp = hp;
    //    player = GameObject.FindGameObjectWithTag("Player");
    //    scoreSystem = GameObject.FindGameObjectWithTag("GH").GetComponent<ScoreSystem>(); // GH = GameHandler
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(hp <= 0)
    //    {
    //        scoreSystem.AddPoints(10f);
    //        Destroy(this.gameObject);
    //    }

    //    if(timeBTWAttack > 0)
    //    {
    //        timeBTWAttack -= Time.deltaTime;
    //    }

    //    if (Vector3.Distance(transform.position, player.transform.position) <= 10 || hp < startHp)
    //    {
    //        GetComponent<NavMeshAgent>().destination = player.transform.position;
    //    }
    //    if(Vector3.Distance(transform.position, player.transform.position) <= 0.5f)
    //    {
    //        GetComponent<NavMeshAgent>().destination = transform.position;

    //        if(timeBTWAttack <= 0)
    //        {
    //            timeBTWAttack = startTimeBTWAttack;
    //            player.gameObject.GetComponent<PlayerEvents>().hp -= attackDamage;
    //        }
    //    }
    //}
    #endregion
}
