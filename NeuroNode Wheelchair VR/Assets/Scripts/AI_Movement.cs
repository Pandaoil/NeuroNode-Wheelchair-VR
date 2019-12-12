using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    public bool isNice;
    public bool isRoaming;
    public Vector3 targetVector3;

    bool inTheWay = false;
    bool movingToTarget = true;
    
    float chance;
    float timer;
    float i;

    public Vector3 randomRoamingPoint;
    Vector3 currentLocation;
    Vector3 startLocation;
    
    Vector3 playerVector3;
    Vector3 whereToGo;

    GameObject targetDestination;
    GameObject playerPosition;
    NavMeshAgent nav;
    SphereCollider sphereCollider;

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");

        whereToGo = targetVector3;

        nav = GetComponent<NavMeshAgent>();

        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;

        startLocation = transform.position;

        GenerateRandomPoint();

        nav.SetDestination(targetVector3);

        i = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        currentLocation = gameObject.transform.position;
        playerVector3 = playerPosition.transform.position;

        if (isRoaming == true)
        {
            float dist = Vector3.Distance(new Vector3(currentLocation.x, 0, currentLocation.z), new Vector3(randomRoamingPoint.x, 0, randomRoamingPoint.z));

            if (dist <= 2f)
            {
                GenerateRandomPoint();
                timer = 0;
            }

            if(timer >= Random.Range(1, 100) || i == 0)
            {
                nav.SetDestination(randomRoamingPoint);
                i = 1;
            }
        }

        else if (isRoaming == false)
        {
            if (movingToTarget)
            {
                whereToGo = targetVector3;
            }
            else
            {
                whereToGo = startLocation;
            }

            if (timer >= Random.Range(3, 5))
            {
                nav.SetDestination(whereToGo);
            }
            
            if (Vector3.Distance(transform.position, whereToGo) < 0.4f)
            {
                movingToTarget = !movingToTarget;
                timer = 0;
            }
        }
        
        if (isNice == true && inTheWay == true)
        {
            nav.SetDestination(-playerVector3);
        }

        else if (isNice == false && inTheWay == true)
        {
            if(Random.Range(0, 100) <= 50)
            {
                nav.SetDestination(-playerVector3);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTheWay = true;
            
            nav.SetDestination(currentLocation);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTheWay = false;

            timer = 0;
            nav.SetDestination(currentLocation);

            if (timer >= Random.Range(3, 5))
            {
                nav.SetDestination(whereToGo);
            }
        }
    }

    void GenerateRandomPoint()
    {
        randomRoamingPoint = new Vector3((transform.position.x + Random.Range(-5f, 5f)), 0, (transform.position.z + Random.Range(-5f, 5f)));
    }
}