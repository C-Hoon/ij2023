using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    string scene;
    [SerializeField]
    bool isSave = true;
    [SerializeField]
    bool loadMode = false;
    public void LoadScene()
    {
        if(isSave)
        {
            if(GameCore.Managers.Game.Player != null)
                GameCore.Managers.Game.Player.GetComponent<PlayerMove>().playerStat.Save();
            
        }
        GameCore.Managers.Game.loadMode = loadMode;
        SceneManager.LoadScene(scene);
    }
}
