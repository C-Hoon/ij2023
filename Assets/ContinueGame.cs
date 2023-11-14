using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    [SerializeField]
    Button button;
    private void FixedUpdate()
    {
        if (string.IsNullOrEmpty(GameCore.Managers.Game.LoadStage()))
            button.interactable = false;
        else
            button.interactable = true;

    }
    public void ContinueLoad()
    {
        SceneManager.LoadScene(GameCore.Managers.Game.LoadStage());
    }
}
