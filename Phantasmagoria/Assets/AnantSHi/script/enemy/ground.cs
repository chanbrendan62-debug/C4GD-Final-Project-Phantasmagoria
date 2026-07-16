using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ground : ENEMYBASE
{

    public override void Chase()
    {

        

       
       

        float diff = PlayerInstance.Instance.transform.position.x - transform.position.x;
        int dir = (int)Mathf.Sign(diff);

        rb.MovePosition(new Vector2(rb.position.x + speed * dir * Time.fixedDeltaTime, rb.position.y));

    }
}
