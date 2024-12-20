using Unity.Entities;
using Unity.Mathematics;


public struct InputComponent2 : IComponentData
{
 public float2 movement;
 public float2 mousePos;
 public bool pressingLMB;
}
       