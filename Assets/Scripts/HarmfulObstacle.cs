using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulObstacle : MonoBehaviour {

    Player playerReference;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerReference == null)
            {
                playerReference = other.GetComponent<Player>();
            }
            playerReference.PlayerHurt();
            InvokeRepeating("KeepHarm", 0.4f, 0.4f);
        }
    }

    private void KeepHarm()
    {
        playerReference.PlayerHurt();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CancelInvoke();
        }
    }
}
