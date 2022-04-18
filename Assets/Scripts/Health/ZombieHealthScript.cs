using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieHealthScript : HealthScript
{
    ZombieStateMachine zombieStateMachine;

    private void Awake()
    {
        zombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        zombieStateMachine.ChangeState(ZombieStateType.DEAD);
        StartCoroutine(Delay());
        
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        Score.scoreAmount = Score.scoreAmount + 10;

        if(Score.scoreAmount == 1000)
        {
            SceneManager.LoadScene("Win");
        }
    }
}