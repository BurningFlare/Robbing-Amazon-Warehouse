using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    private void Start()
    {
        // all enemies must register themselves to the director upon start
        if (!EnemyDirector.registerEnemy(this))
        {
            Debug.LogError("Error registering this enemy to the director: " + this.GetType().Name);
        }
    }

    public abstract void lookoutFor(string TransmutationName);
}
