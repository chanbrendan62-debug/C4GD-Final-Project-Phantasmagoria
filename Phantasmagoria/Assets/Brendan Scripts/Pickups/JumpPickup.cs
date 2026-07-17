using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            BetterMovement bm = collision.GetComponent<BetterMovement>();
            if (bm != null)
            {
                bm.maxDJump += 1;
            }

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.addJumps();
            }
            Destroy(gameObject);
        }
    }
}
