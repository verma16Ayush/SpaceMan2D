using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPathFindingAI : MonoBehaviour
{
    //Store the rigidBody component
    private Rigidbody2D m_Rb;
    //store the seeker component
    private Seeker m_Seeker;
    //store the transform of target
    public Transform target;
    // store the calculated path
    private Path m_Path;
    //how many times per second the path must be updated
    public float pathUpdateRate;
    // current waypoint index
    private int m_CurrentWaypointIndex = 0;
    //force applied on ai agent
    public float force = 300f;
    //flag to know if end of calculated path reaached
    private bool m_PathEnded = false;
    //store the forcemode to apply to rb
    public ForceMode2D fmode;
    //time elapsed since target not found to search a target
    private float m_TimeToSearch = 0.5f;
    private void Start()
    {
        // Assign variables approprietly
        m_Rb = GetComponent<Rigidbody2D>();
        m_Seeker = GetComponent<Seeker>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //start a path from current position to target position
        if (target != null)
        {
            m_Seeker.StartPath(transform.position, target.position, OnPathCalculateEnter);
        }

        StartCoroutine(UpdatePath());
    }

    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            SearchTarget();
            yield break;
        }

        m_Seeker.StartPath(transform.position, target.position, OnPathCalculateEnter);
        yield return new WaitForSeconds(1f / pathUpdateRate);
        StartCoroutine(UpdatePath());
    }

    private void OnPathCalculateEnter(Path p)
    {
        //enter on having calculated path
        if (!p.error)
        {
            m_Path = p;
            m_CurrentWaypointIndex = 0;
        }
    }

    private void FixedUpdate()
    {
        //actually move the enemy

        if (target == null)
        {
            SearchTarget();
            return;
        }

        if (m_Path == null)
        {
            Debug.LogError("path not found");
            return;
        }

        if (m_CurrentWaypointIndex >= m_Path.vectorPath.Count)
        {
            m_PathEnded = true;
            return;
        }

        m_PathEnded = false;

        if (!m_PathEnded)
        {
            //calculate direction to next waypoint
        Vector3 dir = (m_Path.vectorPath[m_CurrentWaypointIndex] - transform.position).normalized;
        dir *= force * Time.deltaTime;
        m_Rb.AddForce(dir, fmode);

        m_CurrentWaypointIndex++;
        }
    }

    void SearchTarget()
    {
        if (Time.time > m_TimeToSearch)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                StartCoroutine(UpdatePath());
            }

            m_TimeToSearch = Time.time + 0.5f;
        }
    }
}
