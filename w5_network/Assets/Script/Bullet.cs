using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float Speed = 12f;
    public float Damage = 25f;
    public float LifeTime = 3f;
    private float _age = 0f;

    public override void FixedUpdateNetwork()
    {
        transform.position += transform.forward * Speed * Runner.DeltaTime;

        _age += Runner.DeltaTime;
        if (_age >= LifeTime)
        {
            if (Runner != null && Object != null)
            {
                Runner.Despawn(Object); 
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ontriggerenter");
        var health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.DealDamageRpc(Damage);
        }
        if (HasStateAuthority)
        {
            if (Runner != null && Object != null)
            {
                Runner.Despawn(Object);
            }
        }
    }
}
