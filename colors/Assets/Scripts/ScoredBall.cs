using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoredBall : MonoBehaviour {

    GameObject Ball;
    ParticleSystem Particles;
    public Gradient gradient = new Gradient();

    private void Start()
    {
        Ball = transform.Find("Ball").gameObject;
        Particles = transform.Find("DestroyParticle").GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height + 200 && gameObject.transform.Find("Ball"))
        {
            Destroy(gameObject);
            if(GameManager.instance.Score > 0)
                GameManager.instance.LosePoint();
        }
    }

    void OnMouseDown()
    {
        if (!GameManager.instance.isDead)
        {
            if (!GameManager.instance.isFroozen)
            {
                GameManager.instance.Scored();
                if (Ball != null)
                {
                    var color = Particles.colorOverLifetime;
                    color.enabled = true;
                    color.color = gradient;
                    Particles.Play();
                    GetComponent<AudioSource>().volume = GameManager.effectsVolume;
                    GetComponent<AudioSource>().Play();
                    Destroy(Ball);
                    Destroy(gameObject, 2f);
                }
            }
        }
    }
}
