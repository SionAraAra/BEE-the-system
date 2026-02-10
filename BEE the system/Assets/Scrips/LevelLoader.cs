using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{

    public bool interact = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(interact)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

    public void EnterRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    
    
}
