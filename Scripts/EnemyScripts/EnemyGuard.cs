using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda; 

public class EnemyGuard : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Enemy enemyStats;
    [SerializeField] private GameObject gun;
    [SerializeField] private List<GameObject> waypoints;
    [SerializeField] private double accuracy = 0.1;
    [SerializeField] private Animator anim;
    [SerializeField] private float visionRange;
    [SerializeField] private GameObject target;
    [SerializeField] private Gun gunStats;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float visionAngle;
    [SerializeField] private float shootAccuracy;
    [SerializeField] private float leaveTargetRange;
    private int currentWaypoint;
    private Vector3 destination;
    private bool alive;
    private bool attacked;
    void Start()
    {
        health = enemyStats.health;
        agent.speed = enemyStats.speed;
        agent.acceleration = enemyStats.acceleration;
        agent.angularSpeed = enemyStats.anugluarSpeed;
        visionRange = enemyStats.visionRange;
        rotationSpeed = enemyStats.rotationSpeed;
        visionAngle = enemyStats.visionAngle;
        shootAccuracy = enemyStats.shootAccuracy;
        leaveTargetRange = enemyStats.leaveTargetRange;
        currentWaypoint = 0;
        alive = true;
        attacked = false;
    }
    private void Update()
    {
       
        if (alive == false)
        {
            StartCoroutine( this.DestroyObject());
        }
    }
   public bool GetAlive()
    {
        return alive;
    }
   public IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        
    }

    [Task]
    public void IsHealthLessThan(float amount)
    {
        if (health <= amount)
        {
            Task.current.Succeed();
        }
    }
    [Task]
    public void Die()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Shoot", false);
        anim.SetBool("Die",true);
        alive = false;
        Task.current.Succeed();
    }
    
    [Task]
    public void PickDestination()
    {
        anim.SetBool("Walk", false);
        anim.SetBool("Shoot", false);
        destination = waypoints[currentWaypoint].transform.position;
        Task.current.Succeed();
    }
    [Task]
    public void MoveToDestination()
    {
        anim.SetBool("Walk", true);
     
        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            agent.SetDestination(destination);
        else
            agent.SetDestination(this.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Task.current.Succeed();
        }
     

    }
    [Task]
    public void ChangeDestination()
    {
        currentWaypoint++;
        if (currentWaypoint >= waypoints.Count)
            currentWaypoint = 0;
        Task.current.Succeed();
    }
    [Task]
    public bool Triggered()
    {
        if (this.SeeTarget() || this.Attacked())
            return true;
        else
            return false;
           

    }
    
    public bool SeeTarget()
    {
        Vector3 dist = target.transform.position - this.transform.position;
        float angle = Vector3.Angle(this.transform.forward, dist);
        bool visionBlocked = false;
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, dist, Color.red);
        if(Physics.Raycast(this.transform.position,dist,out hit))
        {
            if ((hit.collider.gameObject.tag == "visionBlock")||(hit.collider.gameObject.tag == "Door")||(hit.collider.gameObject.tag == "Enemy"))
            {
                visionBlocked = true; 
            }
        }
        if ((dist.magnitude < visionRange) &&(angle<=30f)&& (visionBlocked == false))
        {
            agent.SetDestination(this.transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool Attacked()
    {
        Vector3 dist = target.transform.position - this.transform.position;
        if (dist.magnitude >= this.leaveTargetRange)
            attacked = false;
        return attacked;
    }
    [Task]
    public void LookAtTarget()
    {
        Vector3 direction = target.transform.position - this.transform.position;
        direction.y = 0f;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotationSpeed);
        if (Vector3.Angle(this.transform.forward, direction) < 5.0f)
        {
            Task.current.Succeed();
        }
    }
    [Task]
    public void TargetDestination()
    {
        destination = target.transform.position;
        Task.current.Succeed();
    }
   
    [Task]
    public bool CanShoot()
    {
        Vector3 distance = target.transform.position - this.transform.position;
        if (distance.magnitude < gunStats.range )
        {
            return true;
        }
        else
        {
            anim.SetBool("Shoot", false);
            return false;
        }
    }
    [Task]
    public void Attack()
    {
        if(gun.gameObject.GetComponent<GunScript>())
        {
            if (Time.time >= gun.gameObject.GetComponent<GunScript>().getNextTimeToFire())
            {
                if (gun.gameObject.GetComponent<GunScript>().getBulletsLoader() > 0)
                {
                    
                    anim.SetBool("Shoot",true);
                    Task.current.Succeed();
                }
            }

        }
       
    }
    public void Shoot()
    {
        this.gun.gameObject.GetComponent<GunScript>().setNextTimeToFire(Time.time + 1f / gunStats.fireRate);
        this.gun.gameObject.GetComponent<GunScript>().Shoot();
        float random = Random.Range(0f, 1f);
        if (random > 1 - shootAccuracy)
        {
            target.gameObject.GetComponent<PlayerHealth>().GetDamage(gunStats.damage);
        }
    }
    public void GetDamage(float amount)
    {
        this.health -= amount;
        attacked = true;
    }
       
    
}
