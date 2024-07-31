using UnityEngine;

public class ChasePlayerState : GuardBaseState 
{
    // In order to make the chase more realistic,
    // only chase to the last location the player was seen
    // If guard loses player from LOS, run forwards for a bit, then slow down
    public int currentChaseDuration = 0;
    public bool hasLostLOS = false;
    public Vector2 lastKnownLocation;
    public override void EnterState(GuardStateManager guard)
    {
        
    }

    public override void UpdateState(GuardStateManager guard)
    {
        // if player is in LOS, chase player regardless of transmutation
        if (!hasLostLOS)
        {
            // chasePlayer
            lastKnownLocation = guard.targetPlayer.currentTransmutation.transform.position;
            //if (brokeLos) hasLostLOS = true
        }

        // if player break LOS, chase to last known location. 
        // if a moving player comes into LOS, chase
        // if none, either run forward, or start checking nearby props
    }


}
