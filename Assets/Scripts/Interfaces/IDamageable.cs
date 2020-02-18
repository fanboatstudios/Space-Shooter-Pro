using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int Hits { get; }

    void Damage(int amount);
}
