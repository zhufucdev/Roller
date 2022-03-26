using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Prize {
    First, Second, Third, Memorial
}

public class RollerBehavior : MonoBehaviour
{
    Prize GetPrize(float angle)
    {
        if (angle > 257 && angle <= 280)
        {
            return Prize.First;
        }
        if (angle > 280 && angle <= 327)
        {
            return Prize.Second;
        }
        else if (angle > 327 || angle <= 47)
        {
            return Prize.Third;
        }
        return Prize.Memorial;
    }

    Rigidbody rb;
    Vector3 up;
    MeshRenderer arrow;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        up = rb.transform.up;

        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    Prize lastPrize = Prize.Second;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var center = gameObject.transform.position;
            var r = (MouseTracker.Current - center).magnitude;
            var v = MouseTracker.Veloity;
            var w = (v / r).magnitude * (v.y > 0 ? -1 : 1) * up;

            rb.angularVelocity = w;
        }
        var currentPrize = GetPrize(rb.rotation.eulerAngles.y);
        if (currentPrize != lastPrize)
        {
            var path = string.Format("Materials/roller.{0}", (int)currentPrize + 1);
            arrow.material = Resources.Load<Material>(path);
            lastPrize = currentPrize;
        }
    }
}
