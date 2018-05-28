using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BlueBall : MonoBehaviour {

    GameObject Ball;
    ParticleSystem Particles;
    public Gradient gradient = new Gradient();

    // Use this for initialization
    void Start () {
        Ball = transform.Find("Ball").gameObject;
        Particles = transform.Find("DestroyParticle").GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height + 200)
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (Ball != null)
        {
            GameManager.instance.Freeze();
            var color = Particles.colorOverLifetime;
            color.enabled = true;
            color.color = gradient;
            Particles.Play();
            Destroy(Ball);
            Destroy(gameObject, 2f);
        }
    }
}
