using UnityEngine;

namespace GameCore.Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Equipment/Weapon")]
    public class Weapon : Equipment
    {
        public WeaponActionType actionType;

        //조건은 이벤트버스로 활성
        public WeaponAction weaponAction => WeaponEventFactory.CreateAction(actionType);
        public void SetContext(Object context)
        {
            this.Context = context;
        }
        public void Run(Object context)
        {
            if (this.Context != null)
                weaponAction.Run(this.Context);

            weaponAction.Run(context);
        }
    }
}