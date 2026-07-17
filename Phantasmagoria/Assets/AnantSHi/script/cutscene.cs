using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class cutscene : MonoBehaviour
{
    public Image lastt;
    bool lerp = false;
    float timer;
    void Start()
    {
        Invoke("cutsscene", 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (lerp)
        {
            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, timer, timer);
            lastt.color = new UnityEngine.Color(lastt.color.r, lastt.color.g, lastt.color.b, newAlpha);
        }
    }
    void cutsscene()
    {
        lerp = true;
    }
}
