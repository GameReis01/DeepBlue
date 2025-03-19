using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Ship Properties")]
    public float maxSpeed = 10f;
    public float acceleration = 2f;
    public float rotationSpeed = 30f;
    public float buoyancyForce = 10f;
    
    [Header("Ship Components")]
    public Transform[] buoyancyPoints;
    public ParticleSystem wakeEffect;
    
    private Rigidbody shipRigidbody;
    private float currentSpeed;
    private WaterSystem waterSystem;
    
    private void Start()
    {
        shipRigidbody = GetComponent<Rigidbody>();
        waterSystem = FindObjectOfType<WaterSystem>();
    }
    
    private void Update()
    {
        HandleInput();
    }
    
    private void FixedUpdate()
    {
        ApplyBuoyancy();
        MoveShip();
    }
    
    private void HandleInput()
    {

        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, -maxSpeed/2, acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, acceleration * 0.5f * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        if (wakeEffect != null)
        {
            var emission = wakeEffect.emission;
            emission.rateOverTime = Mathf.Abs(currentSpeed) * 2;
        }
    }
    
    private void MoveShip()
    {
        Vector3 movement = transform.forward * currentSpeed * Time.fixedDeltaTime;
        shipRigidbody.MovePosition(shipRigidbody.position + movement);
    }
    
    private void ApplyBuoyancy()
    {
        foreach (Transform point in buoyancyPoints)
        {
            float waterHeight = waterSystem.transform.position.y;

            if (point.position.y < waterHeight)
            {
                float submersion = waterHeight - point.position.y;
                Vector3 force = Vector3.up * submersion * buoyancyForce;
                shipRigidbody.AddForceAtPosition(force, point.position);
            }
        }
    }
}
