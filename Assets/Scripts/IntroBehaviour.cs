using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroBehaviour : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer video_player;
    // Start is called before the first frame update
    void Start()
    {
       video_player = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((video_player.frame) > 0 && (video_player.isPlaying == false))
        {
            SceneManager.LoadScene("ProBuilder_Scene");
        }
    }
}
