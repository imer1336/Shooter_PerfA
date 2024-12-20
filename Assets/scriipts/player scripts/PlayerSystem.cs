using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine.EventSystems;

public partial struct PlayerSystem : ISystem
{
    private Entity playerEntity;
    private Entity inputEntity;
    private EntityManager entityManager;
    private PlayerComponent playerComponent;
    private InputComponent2 InputComponent2;
    
    public void OnUpdate(ref SystemState state)
    {
        entityManager = state.EntityManager;
        playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();
        inputEntity = SystemAPI.GetSingletonEntity<InputComponent2>();

        playerComponent = entityManager.GetComponentData<PlayerComponent>(playerEntity);
        InputComponent2 = entityManager.GetComponentData<InputComponent2>(inputEntity);

        Move(ref state);
        Shoot(ref state);

    }

    private void Move(ref SystemState state)
    {
        LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
        
        playerTransform.Position += new float3(InputComponent2.movement * playerComponent.MoveSpeed * SystemAPI.Time.DeltaTime, 0);

        Vector2 dir = (Vector2)InputComponent2.mousePos - (Vector2)Camera.main.WorldToScreenPoint(playerTransform.Position);
        float angle = math.degrees(math.atan2(dir.y, dir.x));
        playerTransform.Rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        
        entityManager.SetComponentData(playerEntity,playerTransform);
    }

    private float nextShootTIme;

    private void Shoot(ref SystemState state)
    {
        if (InputComponent2.pressingLMB && nextShootTIme < SystemAPI.Time.ElapsedTime)
        {
            EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.Temp);
            
            Entity bulletEntity = entityManager.Instantiate(playerComponent.BulletPrefab);
            
            ECB.AddComponent(bulletEntity, new BulletComponent { Speed = 10,Size = 1 });

            LocalTransform bulletTransform = entityManager.GetComponentData<LocalTransform>(bulletEntity);
            bulletTransform.Rotation = entityManager.GetComponentData<LocalTransform>(playerEntity).Rotation;
            LocalTransform playerTransform = entityManager.GetComponentData<LocalTransform>(playerEntity);
            bulletTransform.Position = playerTransform.Position + playerTransform.Right() + playerTransform.Up() * -0.35f;
            ECB.SetComponent(bulletEntity,bulletTransform);
            ECB.Playback(entityManager);
            
            nextShootTIme = (float)SystemAPI.Time.ElapsedTime + playerComponent.ShootCoolDown;
        }
    }
}
