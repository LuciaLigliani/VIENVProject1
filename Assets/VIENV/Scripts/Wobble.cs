using UnityEngine;
using System.Collections;

/// <summary>
/// Applies a Sine wave to an objects vertical position to create a wobble
/// </summary>
public class Wobble : MonoBehaviour {

	private Vector3 initialpos; // Store original object position
    public float amount = 0.1f; // Amount of wobble (multiplying factor)

	// Use this for initialization
	void Start () {
		initialpos = transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = initialpos + new Vector3 (0.0f, amount*Mathf.Sin (Time.timeSinceLevelLoad), 0.0f);
	}
}
