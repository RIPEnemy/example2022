using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CubeController : MonoBehaviour
{
    private readonly float Y = 0.5f;

    [SerializeField]
    private ObjectPool _pool = null;
    [SerializeField]
    private Vector3 _leftUpBorder = Vector3.zero;
    [SerializeField]
    private Vector3 _rightDownBorder = Vector3.zero;

    private readonly string[] cubeNames = new string[] { "Cube", "CubeAutoRelease" };

    public void Spawn()
    {
        DemoCube cube = _pool.Obtain<DemoCube>(cubeNames[Random.Range(0, cubeNames.Length)]);
        
        Vector3 pos = new Vector3(0, Y, 0);
        pos.x = Random.Range(_leftUpBorder.x, _rightDownBorder.x);
        pos.z = Random.Range(_rightDownBorder.z, _leftUpBorder.z);

        cube.transform.position = pos;

        cube.OnCollide += HandleCubeCollision;
        cube.SetActive(true);
    }

    private void HandleCubeCollision(object sender, EventArgs _)
    {
        DemoCube cube = (DemoCube)sender;
        cube.OnCollide -= HandleCubeCollision;
        
        if (cube.GetComponent<PoolAutoReleaser>())
        {
            cube.SetActive(false);
            return;
        }

        _pool.Release(cube);
    }
}
