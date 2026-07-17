using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door1 : MonoBehaviour
{
    public Animator animator;
    public GameObject text;
    private bool playerInRange = false;
    private bool doorAnimationFinished = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerInRange && doorAnimationFinished && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Room2Scene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (animator != null) animator.SetTrigger("DoorOpener");
            playerInRange = true;

            if (doorAnimationFinished && text != null)
            {
                text.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            if (text != null)
            {
                text.SetActive(false);
            }
        }
    }

    public void OnDoorAnimationFinished()
    {
        doorAnimationFinished = true;

        if (playerInRange && text != null)
        {
            text.SetActive(true);
        }
    }
}