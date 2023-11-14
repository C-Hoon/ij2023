using UnityEditor;
using UnityEngine;
using QuestSystem;


namespace GameCore.Data
{
    public interface IParsable
    {
        void FillFromStr(string str);
    }
    public enum ContextType
    {
        Player,
        Empty,
    }

    public class Equipment : ScriptableObject
    {
        public string equipmentName;
        public Sprite Image;
        public string description;
        public ContextType contextType;
        object context;
        public object Context
        {
            get
            {
                switch (contextType)
                {
                    case ContextType.Player:
                        context = GameCore.Managers.Game.Player.GetComponent<PlayerMove>();
                        return context;
                    default:
                        return null;
                }
            }
            set { context = value; }
        }

        public float amount = 0; // 무기의 스탯 정보를 저장하는 변수
        public Define.CharacterStat stat = Define.CharacterStat.MaxCount;
        public void SetUpItem()
        {
            PlayerMove playerMove = GameCore.Managers.Game.Player.GetComponent<PlayerMove>();
            PlayerStat playerStat = playerMove.playerStat;
            switch (stat)
            {
                case Define.CharacterStat.MaxHp:
                    playerStat.hp += (int)amount;
                    break;
                case Define.CharacterStat.Hp:
                    playerMove.Health += (int)amount;
                    break;
                case Define.CharacterStat.MaxMp:
                    playerStat.mp += (int)amount;
                    break;
                case Define.CharacterStat.Mp:
                    playerMove.Mana += (int)amount;
                    break;
                case Define.CharacterStat.Attack:
                    playerStat.attack += (int)amount;
                    break;
                case Define.CharacterStat.Defense:
                    playerStat.defense += (int)amount;
                    break;
                case Define.CharacterStat.MoveSpeed:
                    playerStat.moveSpeed += amount;
                    break;
                case Define.CharacterStat.JumpPower:
                    playerStat.jumpPower += amount;
                    break;
                case Define.CharacterStat.CritRate:
                    playerStat.critRate += (int)amount;
                    break;
                case Define.CharacterStat.CritDamage:
                    playerStat.critDamage += (int)amount;
                    break;
                case Define.CharacterStat.MaxCount:
                    break;
                default:
                    // 유효하지 않은 스탯 이름 처리
                    Debug.LogWarning("Invalid stat name: " + stat);
                    break;
            }
        }
        public void TakeDownItem()
        {
            PlayerMove playerMove = GameCore.Managers.Game.Player.GetComponent<PlayerMove>();
            PlayerStat playerStat = playerMove.playerStat;
            switch (stat)
            {
                case Define.CharacterStat.MaxHp:
                    playerStat.hp -= (int)amount;
                    break;
                case Define.CharacterStat.Hp:
                    playerMove.Health -= (int)amount;
                    break;
                case Define.CharacterStat.MaxMp:
                    playerStat.mp -= (int)amount;
                    break;
                case Define.CharacterStat.Mp:
                    playerMove.Mana -= (int)amount;
                    break;
                case Define.CharacterStat.Attack:
                    playerStat.attack -= (int)amount;
                    break;
                case Define.CharacterStat.Defense:
                    playerStat.defense -= (int)amount;
                    break;
                case Define.CharacterStat.Exp:
                    playerStat.moveSpeed -= amount;
                    break;
                case Define.CharacterStat.MoveSpeed:
                    playerStat.jumpPower -= amount;
                    break;
                case Define.CharacterStat.JumpPower:
                    playerStat.jumpPower -= amount;
                    break;
                case Define.CharacterStat.CritRate:
                    playerStat.critRate -= (int)amount;
                    break;
                case Define.CharacterStat.CritDamage:
                    playerStat.critDamage -= (int)amount;
                    break;
                case Define.CharacterStat.MaxCount:
                    break;
                default:
                    // 유효하지 않은 스탯 이름 처리
                    Debug.LogWarning("Invalid stat name: " + stat);
                    break;
            }
        }
    }
    

    [System.Serializable]
    public class LevelUpExp
    {
        public int level;
        public int exp;
        public int hp;
        public int mp;
        public int attack;
        public int defense;
        public float moveSpeed;
        public float aCoolTime;
        public float sCoolTime;
        public float jumpPower;

        public Stat GetStat()
        {
            Stat stat = new Stat();
            stat.level = this.level;
            stat.hp = this.hp;
            stat.mp = this.mp;
            stat.exp = this.exp;
            stat.attack = this.attack;
            stat.defense = this.defense;
            stat.moveSpeed = this.moveSpeed;
            stat.jumpPower = this.jumpPower;
            return stat;
        }
    }
    [System.Serializable]
    public class Boss
    {
        public int code;
        public string name;
        public int level;
        public int hp;
        public int attack;
        public int defense;
    }
    [System.Serializable]
    public class Monster
    {
        public int code;
        public string name;
        public int level;
        public int hp;
        public int attack;
        public int defense;
        public float moveSpeed;
        public int exp;
    }
}

