using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circles : MonoBehaviour {
    [SerializeField] bool randomStartRotation;
    void Start() {
        if (randomStartRotation) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 359)));
        }
    }
}
