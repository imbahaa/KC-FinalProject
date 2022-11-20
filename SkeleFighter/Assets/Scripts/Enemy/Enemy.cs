using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine statemachine;
    private NavMeshAgent agent;
    private Animator Animator;
    [SerializeField]
    public float enemyHP= 100f;
    private Rigidbody RB;
    public NavMeshAgent Agent { get => agent; }
    [SerializeField]
    private string currentState;
    public Path path;
    public int velocity;
    public float damageDone;

    // Start is called before the first frame update
    void Start()
    {
        statemachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        statemachine.Initialize();
        Animator = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimation();
        velocity = 1;
    }
    void playerAnimation()
    {
        if (velocity != 0)
        {
            Animator.SetBool("Walking", true);
        }
        else
        {
            Animator.SetBool("Walking", false);
        }
    }
    public void TakeDamage(float damageTaken)
    {
        float damageDone = damageTaken;
        Debug.Log("Damaged");
        enemyHP -= damageDone;
        if (enemyHP <= 0)
        {
            EnemyDeath();
        }
    }
    public void EnemyDeath()
    {
        RB.freezeRotation = true;
        RB.constraints = RigidbodyConstraints.FreezePosition;
        Animator.SetBool("Death", true);
    }
}