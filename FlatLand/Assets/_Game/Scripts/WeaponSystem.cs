using OknaaEXTENSIONS.CustomWrappers;
using UnityEngine;

namespace _Game.Scripts {
    public class WeaponSystem : Singleton<WeaponSystem> {
        public GameObject BackWeapon;
        public GameObject HandWeapon;



        public void Init() {
            UnEquip();
        }
        
        
        public void Equip() {
            BackWeapon.SetActive(false);
            HandWeapon.SetActive(true);
        }
        
        public void UnEquip() {
            BackWeapon.SetActive(true);
            HandWeapon.SetActive(false);
        }
        
        
        
        
        
    }
}