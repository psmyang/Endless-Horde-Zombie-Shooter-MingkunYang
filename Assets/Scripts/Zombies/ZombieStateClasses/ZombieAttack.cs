using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : ZombieStates
{
    GameObject followTarget;
    float attackRange = 2;

    int movementHash = Animator.StringToHash("Movement");
    int isAttackingHash = Animator.StringToHash("isAttacking");

    private IDamageable damageableObject;

    public ZombieAttack(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine zombieSM) : base(zombie, zombieSM)
    {
        followTarget = _followTarget;
        UpdateInterval = 2.0f;

        damageableObject = followTarget.GetComponent<IDamageable>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        ownerZombie.zombieNavmeshAgent.isStopped = true;
        ownerZombie.zombieNavmeshAgent.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementHash, 0);
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, true);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();

        if (followTarget != null)
        {
            damageableObject?.TakeDamage(ownerZombie.zombieDamage);
        }
    }

    // Update is called once per frame

    public override void Update()
    {
        if (followTarget != null)
        {
            ownerZombie.transform.LookAt(followTarget.transform.position, Vector3.up);

            float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
            if (distanceBetween > attackRange)
            {
                zombieSM.ChangeState(ZombieStateType.FOLLOW);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavmeshAgent.isStopped = false;
        ownerZombie.zombieAnimator.SetBool(isAttackingHash, false);
    }
}
