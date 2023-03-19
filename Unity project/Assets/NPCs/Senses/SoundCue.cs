using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
// Where it happened, max hearing distance
public class SoundCue : UnityEvent<Vector3, float>
{
}
