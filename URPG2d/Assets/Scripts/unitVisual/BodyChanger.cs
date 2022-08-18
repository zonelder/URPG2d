using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyChanger : MonoBehaviour
{
    public void GoBodiless()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
    public void GoBodily()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
