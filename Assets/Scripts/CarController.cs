using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public Rigidbody BodyRigidbody => bodyRigidbody;
    public Transform CenterOfMass;

    [Header("Wheels")]
    public Rigidbody WheelFL;
    public Rigidbody WheelFR;
    public Rigidbody WheelBL;
    public Rigidbody WheelBR;

    [Header("Settings")]
    public float MaxSpeed = 100;
    public float Acceleration = 1700f;
    public float BrakeForce = 1000f;
    public float wheelFriction = 100f;
    public float TurnForce = 1500f;
    public AnimationCurve TurnForceCurve;


    public void Accelerate(float force = 1.0f)
    {
        float sp = Vector3.Dot(BodyRigidbody.velocity, transform.forward) * 3.6f;
        if (sp > MaxSpeed)
        {
            Debug.Log("Capped front");
            return;
        }
        force = Mathf.Clamp01(force);
        BodyRigidbody.AddForceAtPosition(transform.forward * Acceleration * force, CenterOfMass.position);
    }
    public void Brake(float force = 1.0f)
    {
        if (Vector3.Dot(BodyRigidbody.velocity, -transform.forward) * 3.6f > MaxSpeed)
        {
            Debug.Log("Capped back");
            return;
        }
        force = Mathf.Clamp01(force);
        BodyRigidbody.AddForceAtPosition(-transform.forward * BrakeForce * force, CenterOfMass.position);
    }
    public void DoTurn(float input)
    {
        input = Mathf.Clamp(input, -1, 1);
        float sp = Mathf.Abs(Vector3.Dot(BodyRigidbody.velocity, transform.forward));
        sp *= 3.6f;
        sp /= MaxSpeed;
        sp = Mathf.Clamp01(sp);
        sp = TurnForceCurve.Evaluate(sp);
        BodyRigidbody.AddTorque(transform.up * input * TurnForce * sp);
    }

    void WheelFriction(Rigidbody wheel)
    {
        Vector3 dir = wheel.transform.up;
        float force = Vector3.Dot(wheel.velocity, dir) * wheelFriction;
        wheel.AddForce(-dir * force);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Accelerate(1.0f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Brake(1.0f);
        }
        DoTurn(Input.GetAxis("Horizontal"));

        WheelFriction(WheelFR);
        WheelFriction(WheelFL);
        WheelFriction(WheelBR);
        WheelFriction(WheelBL);
    }

    void Awake()
    {
        bodyRigidbody = GetComponent<Rigidbody>();
    }


    Rigidbody bodyRigidbody;
}
