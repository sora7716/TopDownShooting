using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStart : MonoBehaviour
{
    [SerializeField] private string sceneName_;
    public void SceneChange()
    {
       SceneManager.LoadScene(sceneName_);
    }
}
