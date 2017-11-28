/**
 * This script is to be used along with Unity's Particle System
 * It provides a variety of effects to be used with the particle system
 * This script only edits values of the Particle System. It is not intended to be a replacement.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleEffects : MonoBehaviour {

    //PUBLIC
    public float duration;
    public bool absorbtion;
    //PRIVATE
    ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (absorbtion)
            AbsorbtionEffect();
	}

    void AbsorbtionEffect()
    {
        ps.Stop(true, 0);

        var main = ps.main;
        main.duration = 2f;
        main.startLifetime = 3.5f;
        main.startSpeed = -0.4f;
        main.startSize = 0.23f;
        main.loop = false;

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 2.33f;
        shape.scale = Vector3.one;

        var emission = ps.emission;
        emission.rateOverTime = 200;

        absorbtion = false;

        ps.Play();
    }
}
