using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;


/// <summary>
/// Makes an object pickup-able by a player
/// </summary>
public class Pickable : MonoBehaviour
{
	    public Texture aTexture;
		public GameObject tooltip;

		[Header("Audio & Wobble")]
		public AudioClip pickupSound;
		public AudioClip loopClip;      
		private AudioSource loopSource;
		private Vector3 initialpos; 
    	private float amount = 0.1f; 
		private bool IsSelected = false;
		private Coroutine fadeCoroutine;
    	private Coroutine startLoopCoroutine;

		[Header("Input")]
		public InputActionReference interactAction;

		// Use this for initialization
		void Start () {
			initialpos = transform.position;
			loopSource = gameObject.AddComponent<AudioSource>();
			loopSource.clip = loopClip;
			loopSource.loop = true;
			loopSource.volume = 0f;   // start silent
			loopSource.playOnAwake = false;

			if (interactAction != null)
            interactAction.action.Enable();
		}

		void OnEnable() {
			if (interactAction != null) {
				interactAction.action.performed += OnInteract;
				interactAction.action.Enable();
			}
		}

		void OnDisable() {
			if (interactAction != null) {
				interactAction.action.performed -= OnInteract;
				interactAction.action.Disable();
			}
		}

		private void OnInteract(InputAction.CallbackContext context) {
			if (IsSelected) {
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				if (player != null) {
					player.SendMessage("Pickup", aTexture);
					if (tooltip != null) tooltip.SetActive(false);
					IsSelected = false;
					StartFadeOut();
				}
			}
		}

		void wobble() {
			transform.position = initialpos + new Vector3 (0.0f, amount*Mathf.Sin (Time.timeSinceLevelLoad), 0.0f);
		}

		void stopWobble() {
			transform.position = initialpos;
		}

		void playSounds(){
			if (pickupSound != null) {
				AudioSource.PlayClipAtPoint(pickupSound, transform.position);
				// start loop *after* pickup sound finishes
				if (startLoopCoroutine != null) StopCoroutine(startLoopCoroutine);
					startLoopCoroutine = StartCoroutine(StartLoopAfterPickup(pickupSound.length));
			}
			else {
				StartLoopImmediately();
			}
		}

		// Update is called once per frame
        void Update() {
			if(IsSelected == true) {
				wobble();
			}
			else {
				stopWobble();
			}
		}
	
		void OnTriggerEnter (Collider col) {
				if (col.gameObject.CompareTag("Player")) {
						//col.gameObject.SendMessage ("Pickup", aTexture);
						playSounds();
						if (tooltip != null) tooltip.SetActive(true);
						IsSelected = true;
				}
		}

		void OnTriggerExit(Collider col) {
			if (col.gameObject.CompareTag("Player")) {
				StartFadeOut();
				if (tooltip != null) tooltip.SetActive(false);
				IsSelected = false;
			}
		}

		IEnumerator StartLoopAfterPickup(float delay) {
			yield return new WaitForSeconds(delay);
			if (IsSelected) {
				StartLoopImmediately();
			}
		}

		void StartLoopImmediately() {
			if (loopSource != null && !loopSource.isPlaying) {
				loopSource.Play();
				StartFadeIn();
			}
		}

		void StartFadeIn() {
			if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
			fadeCoroutine = StartCoroutine(FadeVolume(0.5f, 0.5f)); // fade to 50% over 1 sec
		}

		void StartFadeOut() {
			if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
			fadeCoroutine = StartCoroutine(FadeVolume(0f, 1f)); // fade to 0 over 1 sec
		}

		IEnumerator FadeVolume(float target, float duration) {
			float start = loopSource.volume;
			float t = 0f;

			while (t < duration) {
				t += Time.deltaTime;
				loopSource.volume = Mathf.Lerp(start, target, t / duration);
				yield return null;
			}

			loopSource.volume = target;

			if (target == 0f) {
				loopSource.Stop();
			}
		}
}
