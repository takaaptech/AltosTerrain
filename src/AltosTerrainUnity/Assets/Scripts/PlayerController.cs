using UnityEngine;
using UnityEngine.U2D;

namespace AltosTerrain
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerController : MonoBehaviour
	{
		public ParticleSystem dustTrail;
		public float passiveForce = 250.0f;
		public float passiveVelocityTarget = 10.0f;

		private Rigidbody2D playerRigidbody;

		void Awake()
		{
			playerRigidbody = GetComponent<Rigidbody2D>();
			dustTrail.Stop();
		}

		void FixedUpdate()
		{
			if (playerRigidbody.velocity.magnitude < passiveVelocityTarget)
			{
				playerRigidbody.AddForce(new Vector2(passiveForce, 0.0f));
			}
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			dustTrail.Play();

			if (collision.gameObject != null)
			{
				var spriteRenderer = collision.gameObject.GetComponent<SpriteShapeRenderer>();

				var dustTrailRenderer = dustTrail.GetComponent<ParticleSystemRenderer>();

				dustTrailRenderer.material = spriteRenderer.material;
			}
		}

		private void OnCollisionExit2D(Collision2D collision)
		{
			dustTrail.Stop();
		}
	}
}