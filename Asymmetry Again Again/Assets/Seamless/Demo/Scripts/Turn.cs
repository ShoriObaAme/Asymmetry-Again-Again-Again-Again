using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seamless
{
    public class Turn : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up, 0.1f);
        }

    }
}
