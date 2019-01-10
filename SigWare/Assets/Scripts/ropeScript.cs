using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR19
{
    public class ropeScript : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            GetComponent<CharacterJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
        }


    }
}

