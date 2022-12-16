using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinState : MonoBehaviour
{
    public GameObject Win;
    public GameObject head;


    // Start is called before the first frame update
    void Start()
    {
        Win.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Exit");

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Win.SetActive(true);
            Scene currentscene = SceneManager.GetActiveScene();
            Time.timeScale = 0f;

            head.GetComponent<HeadBehaviour>().input_enabled = false;
            head.GetComponent<MovementBehaviour2>().input_enabled = false;



        }

    }
}
