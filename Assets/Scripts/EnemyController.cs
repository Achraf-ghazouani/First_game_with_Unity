using UnityEngine;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public float runSpeed = 5f;
    public float rotationSpeed = 5f;
    public float detectionRange = 10f;
    public float punchDistance = 1f;

    public int maxHealth = 100;

    private int currentHealth;
    private Transform player;
    private Animator anim;
    private CharacterController characterController;


    private enum EnemyState
    {
        Idle,
        Run,
        Punch,
        Dead,
    }

    private EnemyState currentState = EnemyState.Idle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth > 0)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            UpdateStateMachine(distanceToPlayer);
            HandleState(distanceToPlayer);
        }
    }

    void UpdateStateMachine(float distanceToPlayer)
    {
        if (currentState != EnemyState.Dead)
        {
            if (distanceToPlayer <= punchDistance)
            {
                currentState = EnemyState.Punch;
            }
            else if (distanceToPlayer < 6f)
            {
                currentState = EnemyState.Run;
            }
            else
            {
                currentState = EnemyState.Idle;
            }
        }
    }

    void HandleState(float distanceToPlayer)
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                anim.SetBool("IsRun", false);
                anim.SetBool("IsPunch", false);
                RotateTowardsPlayer();
                break;

            case EnemyState.Run:
                anim.SetBool("IsRun", true);
                anim.SetBool("IsPunch", false);
                MoveTowardsPlayer(runSpeed);
                RotateTowardsPlayer();
                break;

            case EnemyState.Punch:
                anim.SetBool("IsRun", false);
                anim.SetBool("IsPunch", true);
                RotateTowardsPlayer();
                break;

            case EnemyState.Dead:
                anim.SetBool("IsRun", false);
                anim.SetBool("IsPunch", false);
                break;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void MoveTowardsPlayer(float currentSpeed)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        characterController.SimpleMove(direction * currentSpeed);
    }










    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            // If the enemy is in the punch state, reduce the player's health
            if (currentState == EnemyState.Punch)
            {
                hit.gameObject.GetComponent<PlayerController>().TakeDamage(1);
            }
        }
    }



    public void TakeDamage(int damage)
    {
        // Decrease the enemy's health by the damage amount
        currentHealth -= damage;

        // Check if the enemy's health is less than or equal to 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }








}
