using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    //primary stats

    [field: SerializeField] public int Strength { get; set; }
    [field: SerializeField] public int Dexterity { get; set; }
    [field: SerializeField] public int Vitality { get; set; }
    [field: SerializeField] public int Intellect { get; set; }
    [field: SerializeField] public int Faith { get; set; }
    [field: SerializeField] public int Wisdom { get; set; }
    [field: SerializeField] public int Charisma { get; set; }

    //secondary stats

    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentHealth { get; set; }
    [field: SerializeField] public int Speed { get; set; }

    public Stats()
    {
        CurrentHealth = MaxHealth;
        Speed = 10;
    }

    public void Update()
    {
        MaxHealth = Vitality * 10;
    }
}