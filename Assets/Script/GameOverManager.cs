using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject target_;
    [SerializeField] FadeControl fade_;
    Color beginColor_ = Vector4.zero;
    Color endColor_ = Color.black;
    [SerializeField] private string sceneName_;
    // Start is called before the first frame update
    void Start()
    {
        fade_.Initialize(beginColor_);   
    }

    // Update is called once per frame
    void Update()
    {
        if (!target_.activeSelf)
        {
            fade_.SetIsFadeOut(true);
        }
        fade_.FadeOut(beginColor_, endColor_);
        if (fade_.isFinished())
        {
            SceneManager.LoadScene(sceneName_);
        }
    }
}
