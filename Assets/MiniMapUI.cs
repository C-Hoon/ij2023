using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum StageType
{
    Empty,
    Normal,
    Boss,
    Player,
}

public class MiniMapUI : MonoBehaviour
{
    Dictionary<StageType, GameObject> miniMapResource = new Dictionary<StageType, GameObject>();
    StageInfo[] stage = { StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),
                        StageInfo.StageInfoFactory(StageType.Empty), StageInfo.StageInfoFactory(StageType.Normal , "0-0"),StageInfo.StageInfoFactory(StageType.Normal , "0-1"),StageInfo.StageInfoFactory(StageType.Normal, "0-2"),StageInfo.StageInfoFactory(StageType.Normal, "0-3"), StageInfo.StageInfoFactory(StageType.Empty),
                        StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty), StageInfo.StageInfoFactory(StageType.Normal, "1-0"),StageInfo.StageInfoFactory(StageType.Normal, "1-1"),StageInfo.StageInfoFactory(StageType.Normal, "1-2"), StageInfo.StageInfoFactory(StageType.Empty),
                       StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty), StageInfo.StageInfoFactory(StageType.Boss, "2-0"),StageInfo.StageInfoFactory(StageType.Boss, "2-1"), StageInfo.StageInfoFactory(StageType.Empty),
                       StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty),StageInfo.StageInfoFactory(StageType.Empty), };
    /*string[] stage = { "Empty", "Empty", "Empty", "Empty", "Empty", "Empty",
                        "Empty", "0-0", "0-1", "0-2", "0-3", "Empty",
                        "Empty", "Empty", "1-0", "1-1", "1-2", "Empty",
                        "Empty", "Empty", "Empty", "2-0", "2-1", "Empty",
                        "Empty", "Empty", "Empty", "Empty", "Empty", "Empty", };*/

    // Start is called before the first frame update
    void Start()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        miniMapResource.Add(StageType.Empty, GameCore.Managers.Resource.Load<GameObject>($"Prefabs/UI/SubItem/MiniMap/{StageType.Empty}"));
        miniMapResource.Add(StageType.Normal, GameCore.Managers.Resource.Load<GameObject>($"Prefabs/UI/SubItem/MiniMap/{StageType.Normal}"));
        miniMapResource.Add(StageType.Boss, GameCore.Managers.Resource.Load<GameObject>($"Prefabs/UI/SubItem/MiniMap/{StageType.Boss}"));
        miniMapResource.Add(StageType.Player, GameCore.Managers.Resource.Load<GameObject>($"Prefabs/UI/SubItem/MiniMap/{StageType.Player}"));
        
        bool isClear = false;
        int point = 0;
        for(int y = 0; y < 5; y++)
        {
            for(int x = 0; x < 6; x++)
            {
                if(stage[x + (y * 6)].Stage == currentSceneName)
                {
                    point = x + (y * 6);
                    isClear = true;
                    break;
                }
            }
            if (isClear)
                break;
        }

        point = point - 7;

        Instantiate(miniMapResource[stage[point].Type], transform);
        point++;
        Instantiate(miniMapResource[stage[point].Type], transform);
        point++;
        Instantiate(miniMapResource[stage[point].Type], transform);
        point = point+4;

        Instantiate(miniMapResource[stage[point].Type], transform);
        point++;
        Instantiate(miniMapResource[StageType.Player], transform);
        point++;
        Instantiate(miniMapResource[stage[point].Type], transform);
        point = point + 4;

        Instantiate(miniMapResource[stage[point].Type], transform);
        point++;
        Instantiate(miniMapResource[stage[point].Type], transform);
        point++;
        Instantiate(miniMapResource[stage[point].Type], transform);
        //point = point + 4;
    }
}
class StageInfo
{
    public string Stage { get; set; }   // 스테이지 번호
    public StageType Type { get; set; } // 스테이지 타입 ("노멀" 또는 "보스")
    public static StageInfo StageInfoFactory(StageType type, string name = "Empty")
    {
        return new StageInfo() { Stage = name, Type = type };
    }

}


