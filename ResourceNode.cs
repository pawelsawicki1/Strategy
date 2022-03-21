using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Resource
    {
    gold = default,
    wood,
    iron,
}
[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ResourceNode")]
public class ResourceNode : ScriptableObject
{
    public string resourceName;
    public int resourceValue = 500;
    public Resource resource;
    public GameObject prefab;
}
