using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdle : ZombieStates
{
    int movementHash = Animator.StringToHash("Movement");
    public ZombieIdle(ZombieComponent zombie, ZombieStateMachine zombieSM) : base(zombie, zombieSM)
    {

    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavmeshAgent.isStopped = true;
        ownerZombie.zombieNavmeshAgent.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementHash, 0);
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavmeshAgent.isStopped = false;
    }
}
