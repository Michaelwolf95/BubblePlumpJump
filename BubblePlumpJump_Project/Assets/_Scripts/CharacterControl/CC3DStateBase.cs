using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "CC3D/STATE_NAME", order = 1)]
public abstract class CC3DStateBase : ScriptableObject
{
    public CC3DStateMachine owner;


    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateExit();
    
}
