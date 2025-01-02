using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float fireForce = 5f;
    public float lifetime = 5f;

    public void Start()
    {
        rb.AddForce(transform.forward * fireForce, ForceMode.Impulse);
        Destroy(gameObject, lifetime);
    }

    public void Update()
    {

    }
}