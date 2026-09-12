using UnityEngine;

public class Spike : GimmickBase
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerDeath>(out var playerDeath))
        {
            playerDeath.Die(DeathType.Default);
        }
    }
}
