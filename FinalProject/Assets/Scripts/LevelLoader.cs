using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] public int levelIndex;

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
