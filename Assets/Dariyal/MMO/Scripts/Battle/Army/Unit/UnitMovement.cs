using UnityEngine;
using System.Collections;
using ModestTree.Zenject;

[RequireComponent(typeof(CharacterController))]
public class UnitMovement : MonoBehaviour 
{
    public float speed = 0.0025F;
    public float rotationSpeed = 0.004F;
    public bool IsMoving { get; set; }

    private Vector3 moveStart;
    private Vector3 moveDestination;

    private CharacterController controller;

	// Use this for initialization
	void Start () 
    {
        controller = gameObject.GetComponent<CharacterController>();
        IsMoving = false;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (!IsMoving)
            return;

        if (transform.position == moveDestination)
        {
            EndMovement();
            return;
        }

        Vector3 direction = moveDestination - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        Vector3 forwardDirection = transform.forward;
        forwardDirection *= speed;
        float speedModifier = Vector3.Dot(direction.normalized, transform.forward);
        forwardDirection *= Mathf.Clamp01(speedModifier);
        Log.Info("moveing by " + forwardDirection.ToString());
        controller.SimpleMove(forwardDirection);
	}

    public void StartMovingTo(Vector3 destination)
    {
        Log.Info("starting movement to " + destination.ToString());
        moveStart = transform.position;
        moveDestination = destination;
        IsMoving = true;
    }

    void EndMovement()
    {
        Log.Info("ending movement");
        IsMoving = false;
    }
}
