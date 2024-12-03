using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatButtons : MonoBehaviour
{
    public void BackToMainMenu()
    {
        Loader.Load(Loader.Scene.MainMenuScene);
    }
}
