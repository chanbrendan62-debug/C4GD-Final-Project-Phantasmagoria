using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToLast : MonoBehaviour
{
    public GameObject bpss;
    public string lastone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bpss == null)
        {
            SceneManager.LoadScene(lastone);
        }
    }
}
