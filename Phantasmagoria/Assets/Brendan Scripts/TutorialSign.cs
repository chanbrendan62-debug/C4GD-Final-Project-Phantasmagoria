    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TutorialSign : MonoBehaviour
    {
        public GameObject text;
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
                text.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                text.SetActive(false);
            }
        }
    }
