using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selector : MonoBehaviour
{
    public UnityAction<GameObject> action;
    public LayerMask layerMask;
    protected Vector3 point;
}
