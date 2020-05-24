using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public void OpenStore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Fcode.SolveMe");
    }

    public void GoHome()
    {
        GameManager.instance.NextLevel();
    }
}