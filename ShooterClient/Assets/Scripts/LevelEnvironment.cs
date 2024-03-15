using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnvironment : MonoBehaviour
{
    [field: SerializeField] public Transform[] SpawnPoints { get; private set; }
}
