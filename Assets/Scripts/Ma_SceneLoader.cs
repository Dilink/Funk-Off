using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ma_SceneLoader : MonoBehaviour
{
    public void LoadScene(int indexToLoad)
    {
        SceneManager.LoadScene(indexToLoad);
    }
}
