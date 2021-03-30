using Invector.vCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    public Transform LootPrefab;
    public float detectionRadius = 20f;
    public float attackRange = 4f;

    private NavMeshAgent _navMeshAgent;
    private Transform _player;
    private Animator _animator;

    private bool _isDead = false;
    private float _distanceToPlayer = float.MaxValue;

    void Awake()
    {
        _player = FindObjectOfType<vThirdPersonController>().transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Walk Forward", true);
    }

    void Update()
    {
        if (_isDead)
        {
            return;
        }

        _distanceToPlayer = (_player.position - transform.position).magnitude;
        _navMeshAgent.stoppingDistance = attackRange;

        if (_distanceToPlayer < detectionRadius)
        {

            if (_distanceToPlayer < attackRange)
            {
                AttackPlayer();
            }
            else
            {
                PursuePlayer();
            }
        }
		else
		{
            _animator.SetBool("Walk Forward", false);
            _animator.SetTrigger("Idle");
        }
    }

    private void PursuePlayer()
	{
        _navMeshAgent.SetDestination(_player.position);
        _animator.SetBool("Walk Forward", true);
    }
    private void AttackPlayer()
	{
        _animator.SetBool("Walk Forward", false);
        _animator.SetTrigger("Attack");
    }
    public void Die()
	{
        this.DropLoot();
        _isDead = true;
        _navMeshAgent.SetDestination(this.transform.position);
        _navMeshAgent.isStopped = true;
        _animator.SetBool("Walk Forward", false);
        _animator.SetTrigger("Die");
        this.GetComponent<NavMeshAgent>().enabled = false;
    }
    private void DropLoot()
	{
        Instantiate(LootPrefab, this.transform.position, Quaternion.identity);
	}
	private void OnDrawGizmos()
	{
        Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
	}
}
    
