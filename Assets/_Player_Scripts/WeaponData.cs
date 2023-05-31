using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public float reloadSpeed;
    public float damage;
    public float attackSpeed;
    public float capacity;
    public float weaponRecoil;
}
