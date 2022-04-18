using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStates : States
{
    protected ZombieComponent ownerZombie;
    public ZombieStates(ZombieComponent zombie, ZombieStateMachine zombieSM) : base(zombieSM)
    {
        ownerZombie = zombie;
    }
}

public enum ZombieStateType
{
    IDLE,
    ATTACK,
    FOLLOW,
    DEAD
}