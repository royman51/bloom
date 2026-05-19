using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<float> OnMoveEvent;
    


    public event Action OnJumpEvent;

    private void Update()
    {
        MovePlayer();

        JumpPlayer();
    }

    private void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJumpEvent?.Invoke();
        }
    }



    private void MovePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        OnMoveEvent?.Invoke(x);
    }
}