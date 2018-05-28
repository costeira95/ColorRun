using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseBall : MonoBehaviour {

    GameObject Ball;
    ParticleSystem Particles;
    public Gradient gradient = new Gradient();

    private void Start()
    {
        Ball = transform.Find("Ball").gameObject;
        Particles = transform.Find("DestroyParticle").GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height + 200)
            Destroy(gameObject);
    }

    void OnMouseDown()
    {
        if (!GameManager.instance.isDead)
        {
            if (!GameManager.instance.isFroozen)
            {
                GameManager.instance.Died();
                if (Ball != null)
                {
                    var color = Particles.colorOverLifetime;
                    color.enabled = true;
                    color.color = gradient;
                    Particles.Play();
                    Destroy(Ball);
                    Destroy(gameObject, 2f);
                }
            }
        }
    }
}
