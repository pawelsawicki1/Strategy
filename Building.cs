using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EBuilding
{
    Main,
}
[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "Building")]
public class Building : ScriptableObject
{
    public int max_health;
    public int health;
    public int armor;
    public int health_regeneration;
    public int fow;
    public int attack_damage;
    public int attack_range;
    public string building_name;
    public EBuilding building;
    public Resource[] ResourceCost = {Resource.gold,Resource.wood,Resource.iron};
    public int[] cost = new int[3];
    public GameObject prefab;
    public int team = 0;
    public int index;
}
