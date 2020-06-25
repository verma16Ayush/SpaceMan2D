using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Pathfinding;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    public Seeker Seeker; //Store the seeker component
    public Rigidbody2D rb; // store the rigidbody2d component
    public Transform target;// target to follow
    public Path path;//store the calculated path
    public float speed = 200f;//spped of force applie (if that makes any sense)
    public ForceMode2D fmode;// force mode to be applied
    public float minWaypoinDist = 3f;// min distance bw two waypoints
    [HideInInspector] public int currentWaypoint = 0;//start waypoint indes because path.vectorpath is an array of path node
    [HideInInspector] public bool pathHasEnded = false;// to check if the calculated path has been completed
    public float pathUpdateRate = 2f;//rate at which to recalculate path every second
    public float timeToSearch = 0.5f;//if the target is null, at what time intervals to search for a target
    private void Start()
    {
        //Assign components, start coroutine 
        rb = GetComponent<Rigidbody2D>();
        Seeker = GetComponent<Seeker>();
        //start calculating a path for the first time
        Seeker.StartPath(transform.position, target.position, OnPathCalculated);
        //start the coroutine to update path so the enemy follows the player if player moves
        StartCoroutine(UpdatePath());
    }

    void OnPathCalculated(Path p)
    {
        if(p.error)
            Debug.Log("we had an error calculating path" + p.error);

        else
        {
            //assign path and set the current way point index for each path calculated
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            Debug.Log("no target detected");
            //TODO: search for player
            yield break;
        }

        Seeker.StartPath(transform.position, target.position, OnPathCalculated);
        yield return new WaitForSeconds(1f/pathUpdateRate);
        StartCoroutine(UpdatePath());
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            //TODO: search for target
            if (target == null)
            {
                SearchTarget();
                return;
            }
        }

        if (path == null)
        {
            // Debug.LogError("path not found");
            return;
        }

        if (currentWaypoint > path.vectorPath.Count)
        {
            if (pathHasEnded)
                return;

            pathHasEnded = true;
        }

        pathHasEnded = false;

        Vector3 velocity = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        velocity *= speed * Time.fixedDeltaTime;
        
        //move the object
        rb.AddForce(velocity, fmode);
        
        float distanceToWaypoint = Vector3.Distance(target.position, path.vectorPath[currentWaypoint]);
        if (distanceToWaypoint < minWaypoinDist)
        {
            currentWaypoint++;
        }

    }

    void SearchTarget()
    {
        if (timeToSearch < Time.time)
        {
            Debug.Log('X');
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                StartCoroutine(UpdatePath());
            }
            timeToSearch = Time.time + 0.5f;
        }
    }

}
