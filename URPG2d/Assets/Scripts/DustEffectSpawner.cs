using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class DustEffectSpawner : MonoBehaviour
{
    [SerializeField] private Movement _unitMove;
    [Header("Effects")]
    [SerializeField] private GameObject _runDust;
    [SerializeField] private GameObject _jumpDust;
    [SerializeField] private GameObject _landingDust;
    // Start is called before the first frame update
    void Start()
    {
        //_unitMove.OnJump
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnStopDustEffect(int facingDirection)
    {
        SpawnDustEffect(_runDust, facingDirection, 0.6f);
    }

    public void SpawnJumpDustEffect(int facingDirection)
    {
        SpawnDustEffect(_jumpDust, facingDirection);
    }
    public void SpawnLandDustEffect(int facingDirection)
    {
        SpawnDustEffect(_landingDust, facingDirection);
    }

    private void SpawnDustEffect(GameObject dust,int facingDirection, float dustXOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * facingDirection, 0.0f, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(facingDirection, 1, 1);
        }
    }

    void AE_runStop()
    {
       SpawnStopDustEffect(_unitMove.Facing);
    }

    void AE_footstep()
    {
    }

    void AE_Jump()
    {
        SpawnJumpDustEffect(_unitMove.Facing);
    }

    void AE_Landing()
    {
       SpawnLandDustEffect(_unitMove.Facing);
    }
}
