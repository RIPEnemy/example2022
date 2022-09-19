using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class DemoCube : MonoBehaviour
{
    private readonly string COLLISION_TAG = "Finish";

    public event EventHandler OnCollide;

    private readonly Vector3[] directions = new Vector3[]
    {
        new Vector3(0, 0, -1),
        new Vector3(0, 0, 1),
        new Vector3(-1, 0, 0),
        new Vector3(1, 0, 0)
    };
    private readonly float force = 3f;

    private Rigidbody rb;
    private Vector3 direction;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    private void OnEnable()
    {
        int i = Random.Range(0, directions.Length);
        direction = directions[i];
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * force;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Equals(COLLISION_TAG))
            return;

        rb.velocity = Vector3.zero;

        OnCollide?.Invoke(this, null);   
    }
}
