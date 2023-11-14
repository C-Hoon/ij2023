using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// enum���� ����� �����ϴ� DefineŬ����
/// </summary>
public class Define
{
    /// <summary>
    /// Scene�� ����, ���ο� Scene�� ����Ϸ��� �� ���� �߰� �� ���
    /// </summary>
    /// MaxCount�� Scene�� ũ�⸦ ��Ÿ��
    public enum Scene
    {
        Unknown,
        Main,
        Lobby,
        Start,
        Game,
        Endding,
        GameManagerTest,
        PlayerTest,
        StageTest,
        UITest,
        TestScene,
        GameStart,
        MaxCount,
    }

    /// <summary>
    /// SceneUI ����
    /// </summary>
    public enum SceneUIType
    {
        Start_Panel,
        State_Panel,
        Info,
        Pause,
        MaxCount,
    }

    /// <summary>
    /// PopupUI ����
    /// </summary>
    public enum PopupUIType
    {
        Panel,
        Button,
        Map,
        KeySetting,
        GameMenu,
        Inventory,
        ShopUI,
        Achievement,
        DiePopup,
        MaxCount,
    }

    /// <summary>
    /// Sound ����
    /// </summary>
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum KeyAction
    {

        UP,
        DOWN,
        LEFT,
        RIGHT,
        MaxCount,
    }

    public enum CharacterStat
    {
        MaxHp,
        Hp,
        MaxMp,
        Mp,
        Attack,
        Defense,
        Exp,
        MoveSpeed,
        JumpPower,
        CritRate,
        CritDamage,
        MaxCount,
    }
    public enum keyMaps
    {
        Attack,
        Shoot,
        Up,
        Down,
        Left,
        Right,
        Jump,
        Interaction,
        MaxCount,
    }

    public enum Language
    {
        en_US,
        fr_FR,
        ja_JP,
        zh_CH,
        ko_KR
    }

    public enum LanguagePartition
    {
        Achievements,
        Codex,
        Event,
        GameUI,
        Item,
        Perks,
        Quotes,
        Tutorial
    }

    #region UI
    public enum Scene_Game
    {
        State_Panel,
        Info,
        Pause,
        Skill,
        MaxCount,
    }
    #endregion
}

