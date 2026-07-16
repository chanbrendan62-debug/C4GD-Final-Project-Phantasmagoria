using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class boss : ENEMYBASE
{

    public float biggerRange;



    public override void Idle()
    {
        base.Idle();
        anim.SetBool("chasing", false);
        Transform sprite = anim.transform;
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void Chase()
    {

        chaseRange = biggerRange;

        anim.SetBool("chasing", true);
        Transform sprite = anim.transform;

        float diff = PlayerInstance.Instance.transform.position.x - transform.position.x;
        int dir = (int)Mathf.Sign(diff);

        sprite.transform.rotation = Quaternion.Euler(0,0, 12.76f * -dir);

        rb.MovePosition(new Vector2(rb.position.x + speed * dir * Time.fixedDeltaTime, rb.position.y));

    }


}
