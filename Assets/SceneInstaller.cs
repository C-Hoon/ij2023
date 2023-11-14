using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInstaller : MonoBehaviour
{
    [SerializeField]
    Transform playerSponPoint;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Install();
    }

    void Install()
    {
        player = GameCore.Managers.Resource.Instantiate("Character/Player");
        if (GameCore.Managers.Game.loadMode)
            player.GetComponent<PlayerMove>().playerStat.Load();
        else
        {
            PlayerStat stat = player.GetComponent<PlayerMove>().playerStat;
            stat.Init();
        }
        if (playerSponPoint)
        {
            player.transform.position = playerSponPoint.transform.position;
        }
        else
        {
            player.transform.position = transform.position;
        }

        GameCore.Managers.Game.Player = player;

        Managers.Sound.SetVolume(0.1f);
        Managers.Sound.Play("BGM/BGM_01", Define.Sound.Bgm);
        Managers.UI.ShowSceneUI(Define.SceneUIType.State_Panel, "State_Panel");
        Managers.UI.ShowSceneUI(Define.SceneUIType.Info, "Info");
        Managers.UI.ShowSceneUI(Define.SceneUIType.Pause, "Pause");
    }
}
