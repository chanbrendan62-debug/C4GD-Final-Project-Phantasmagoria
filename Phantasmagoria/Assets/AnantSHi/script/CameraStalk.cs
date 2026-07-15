using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStalk : MonoBehaviour
{
    Transform player;
    public float CameraSpeed;
    public LevelManager manager;

    Vector3 vel = Vector3.zero;

    private void Start()
    {
        
    }

    void LateUpdate()
    {
        if (player == null)
        {
            if (PlayerInstance.Instance == null) return; // player doesn't exist yet, skip this frame
            player = PlayerInstance.Instance.transform;
            
        }
        float clampX = Mathf.Clamp(player.position.x, manager.MinPos.x, manager.MaxPos.x);
        float clampY = Mathf.Clamp(player.position.y, manager.MinPos.y, manager.MaxPos.y);

        Vector3 targetPos = new Vector3(clampX, clampY, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, CameraSpeed);

    }
}
