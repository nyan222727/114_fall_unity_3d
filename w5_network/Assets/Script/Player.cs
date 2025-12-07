using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private CharacterController _controller;
    public float playerSpeed = 2f;
    public NetworkPrefabRef bulletPrefab;
    private bool spacePressed = false;
    public float bulletSpawnOffset = 1.5f;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

    }

    void Update()
    {
        if  (Input.GetKeyDown(KeyCode.Space))
        {
            spacePressed = true;
        }
    } 

    public override void FixedUpdateNetwork()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * playerSpeed;
        _controller.Move(move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (spacePressed)
        {       
            if (bulletPrefab != default)
            {
                Vector3 spawnPos = transform.position + transform.forward * bulletSpawnOffset;
                Quaternion rot = transform.rotation;
                Runner.Spawn(bulletPrefab, spawnPos, rot, Object.InputAuthority, null, default(Fusion.NetworkSpawnFlags));
            }
            spacePressed = false;
        }

    }

}
