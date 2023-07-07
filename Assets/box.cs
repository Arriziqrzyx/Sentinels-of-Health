using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class box : MonoBehaviour
{
    [SerializeField] private string Scene;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BackToGameplay()
    {
        SceneManager.UnloadSceneAsync(Scene);
    }
}
