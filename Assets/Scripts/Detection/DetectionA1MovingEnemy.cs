using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionA1MovingEnemy : MonoBehaviour
{

    public bool PlayerDetected;
    public UnityEvent<GameObject> OnPlayerDetected;


    [Range(-1f, 1f)]
    public float radius;

    public LayerMask targetLayer;

    [Header("Gizmo parameters")]
    public Color gizmoColor = Color.green;
    public bool showGizmos = true;

    private GameObject target;

    public GameObject Target
    {
        get => target;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }
    }

    private void Update()
    {
        var collider = Physics2D.OverlapCircle(transform.position, radius, targetLayer);
        PlayerDetected = collider != null;
        if (PlayerDetected)
        {
            OnPlayerDetected?.Invoke(collider.gameObject);
        }
    }

    

    

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = gizmoColor;
            
            Gizmos.DrawSphere(transform.position, radius);
        }
    }


}
