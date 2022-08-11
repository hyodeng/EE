using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float HP { get; set; }
    float MaxHP { get;}

    System.Action onHealthChange { get; set; }

    
}
