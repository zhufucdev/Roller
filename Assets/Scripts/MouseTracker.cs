using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private static Plane raycast;
    private static Vector3 last;
    private static DateTime lastMoment;
    public static Vector3 Veloity
    {
        get => (Current - last) / (float)(DateTime.Now - lastMoment).TotalSeconds;
    }

    // Start is called before the first frame update
    void Start()
    {
        var filter = GameObject.FindGameObjectWithTag("ReferencePlane").GetComponent<MeshFilter>();
        Vector3 normal;
        if (filter && filter.mesh.normals.Length > 0)
        {
            normal = filter.transform.TransformDirection(filter.mesh.normals[0]);
            raycast = new Plane(normal, transform.position);
        }
        
        last = Vector3.zero;
        lastMoment = DateTime.Now;
    }


    // Update is called once per frame
    private void Update()
    {
        var newPos = Current;
        Debug.DrawLine(last, gameObject.transform.position, Color.red, 3f);
        gameObject.transform.position = newPos;

        last = Current;
        lastMoment = DateTime.Now;
    }

    public static Vector3 Current
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!raycast.Raycast(ray, out float distance))
            {
                return Vector3.zero;
            }
            return ray.GetPoint(distance);
        }
    }
}
