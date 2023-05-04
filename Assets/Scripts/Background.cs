using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Background", menuName = "Background")]
public class Background : ScriptableObject
{
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Stats Stats { get; set; }
}