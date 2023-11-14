using UnityEngine;

namespace GameCore.Data
{
    [CreateAssetMenu(fileName = "Item", menuName = "Equipment/Item")]
    public class Item : Equipment
    {
        public int price;
    }
}