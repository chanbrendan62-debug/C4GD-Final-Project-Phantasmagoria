using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    float repeatWidth;
    float repeatHeight;
    private void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        repeatWidth = collider.size.x;
        repeatHeight = collider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void LateUpdate()
    {
        if(Camera.main.transform.position.x > transform.position.x + repeatWidth)
        {
            transform.position = new Vector3(player.transform.position.x + repeatWidth, transform.position.y, transform.position.z);
        }

        if(Camera.main.transform.position.x < transform.position.x - repeatWidth)
        {
            transform.position = new Vector3(player.transform.position.x - repeatWidth, transform.position.y, transform.position.z);
        }

        if(Camera.main.transform.position.y > transform.position.y + repeatHeight)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - repeatHeight, transform.position.z);
        }

        if(Camera.main.transform.position.y < transform.position.y - repeatHeight)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - repeatHeight, transform.position.z);
        }

    }
}

