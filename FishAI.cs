using UnityEngine;
using System.Collections.Generic;

public class FishAI : MonoBehaviour
{
    [Header("Fish Properties")]
    public float swimSpeed = 2f;
    public float turnSpeed = 120f;
    public float neighborRadius = 5f;
    public float avoidanceRadius = 2f;
    
    [Header("School Properties")]
    public bool isSchooling = true;
    public float schoolCohesion = 1f;
    public float schoolSeparation = 1f;
    public float schoolAlignment = 1f;
    
    private Vector3 swimDirection;
    private List<FishAI> nearbyFish = new List<FishAI>();
    private WaterSystem waterSystem;
    
    private void Start()
    {
        waterSystem = FindObjectOfType<WaterSystem>();
        swimDirection = Random.insideUnitSphere;
        InvokeRepeating("UpdateNearbyFish", 0f, 0.5f);
    }
    
    private void Update()
    {
        if (isSchooling && nearbyFish.Count > 0)
        {
            ApplySchoolingBehavior();
        }
        else
        {
            ApplyWanderingBehavior();
        }
        
        AvoidObstacles();
        StayInWater();
        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(swimDirection),
            turnSpeed * Time.deltaTime
        );
        
        transform.position += transform.forward * swimSpeed * Time.deltaTime;
    }
    
    private void UpdateNearbyFish()
    {
        nearbyFish.Clear();
        foreach (FishAI fish in FindObjectsOfType<FishAI>())
        {
            if (fish != this && Vector3.Distance(transform.position, fish.transform.position) < neighborRadius)
            {
                nearbyFish.Add(fish);
            }
        }
    }
    
    private void ApplySchoolingBehavior()
    {
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;

        foreach (FishAI fish in nearbyFish)
        {
            cohesion += fish.transform.position;

            float distance = Vector3.Distance(transform.position, fish.transform.position);
            if (distance < avoidanceRadius)
            {
                separation += (transform.position - fish.transform.position).normalized / distance;
            }

            alignment += fish.transform.forward;
        }
        
        cohesion = cohesion / nearbyFish.Count - transform.position;
        alignment = alignment / nearbyFish.Count;

        swimDirection = swimDirection + 
            cohesion.normalized * schoolCohesion * Time.deltaTime +
            separation.normalized * schoolSeparation * Time.deltaTime +
            alignment.normalized * schoolAlignment * Time.deltaTime;
            
        swimDirection.Normalize();
    }
    
    private void ApplyWanderingBehavior()
    {

        if (Random.value < 0.01f)
        {
            swimDirection = Random.insideUnitSphere;
            swimDirection.y *= 0.5f;
            swimDirection.Normalize();
        }
    }
    
    private void AvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("Ship"))
            {
                swimDirection = Vector3.Reflect(swimDirection, hit.normal);
            }
        }
    }
    
    private void StayInWater()
    {

        Vector3 position = transform.position;
        
        if (position.y > waterSystem.transform.position.y)
        {
            swimDirection.y = -1f;
        }
        else if (position.y < waterSystem.transform.position.y - 10f)
        {
            swimDirection.y = 1f;
        }

        float horizontalDistance = new Vector2(position.x, position.z).magnitude;
        if (horizontalDistance > 100f)
        {
            Vector3 toCenter = new Vector3(0, 0, 0) - position;
            toCenter.y = 0;
            swimDirection = Vector3.Slerp(swimDirection, toCenter.normalized, 0.05f);
        }
    }
}
