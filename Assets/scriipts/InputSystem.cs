using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private NewControls controls;
    protected override void OnCreate()
    {
        if (!SystemAPI.TryGetSingleton<InputComponent2>(out InputComponent2 input))
        {
            EntityManager.CreateEntity(typeof(InputComponent2));
        }

        controls = new NewControls();
        controls.Enable();
    }

    protected override void OnUpdate()
    {
        Vector2 moveVector = controls.ActionMap.Movement.ReadValue<Vector2>();
        Vector2 mousePosition = controls.ActionMap.MousePosition.ReadValue<Vector2>();
        bool isPressingLMB = controls.ActionMap.Shooting.ReadValue<float>() == 1 ? true : false;
        SystemAPI.SetSingleton(new InputComponent2{ mousePos = mousePosition,movement = moveVector,pressingLMB = isPressingLMB});
    }
}
