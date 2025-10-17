using System.Collections;
using UnityEditor;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // ref ค่าจาก enemy
    [SerializeField] private BaseEnemy enemyData;
    [SerializeField] private float speed;
    [SerializeField] private Transform enemySprite;

    // Pathfinder variable
    [SerializeField] private Transform target;
    [SerializeField] private float nextWPDis = 3f;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;
    bool reachedEOP;

    //เพื่อส่งให้ State Manager
    public Vector2 force { get; private set; }



    void Start()
    {
        //Component ref
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        enemyData = GetComponent<BaseEnemy>();


        //Set variable
        speed = enemyData.speed;
        reachedEOP = false;


        //Command in Start
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }
        

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
        
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEOP = true;
            return;
        }
        else
        {
            reachedEOP = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        force = direction * speed * Time.deltaTime;

        //rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWPDis)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if(rb.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
