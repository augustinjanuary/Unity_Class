using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IState currentState;

    void Start(){
        currentState = InitialState;
        currentState.OnEnter();
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void ChangeState(IState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}

public interface IState
{
    public void OnEnter();

    public void UpdateState();

    public void OnHurt();

    public void OnExit();
}

public class InitialState : IState
{
    public void OnEnter()
    {
        // "What was that!?"
    }

    public void UpdateState()
    {
        // Search for player
    }

    public void OnHurt()
    {
        // Transition to Hurt State
    }
    public void OnExit()
    {
        // "Must've been the wind"
    }
}
public class MovementState : IState
{
    public void OnEnter()
    {
        // "What was that!?"
    }

    public void UpdateState()
    {
        // Search for player
    }

    public void OnHurt()
    {
        // Transition to Hurt State
    }
    public void OnExit()
    {
        // "Must've been the wind"
    }
}



public class camera_script : MonoBehaviour
{
    public Transform player;

    void Start(){

    }

    void Update(){
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }
}
