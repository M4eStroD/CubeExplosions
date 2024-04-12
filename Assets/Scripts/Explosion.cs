using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;
    [SerializeField] private ParticleSystem _effect;

    [SerializeField] private SpawnCubes _spawnCubes;

    [SerializeField] private int _chanceExplode = 100;

    private void OnMouseUpAsButton()
    {
        TryExplode();
        Destroy(gameObject);
    }

    private void TryExplode()
    {
        int hundredPercent = 100;
        int degreeReduction = 2;

        int randomNumber = Random.Range(0, hundredPercent + 1);

        if (randomNumber <= _chanceExplode)
        {
            _chanceExplode /= degreeReduction;

            Explode();
            _spawnCubes.SpawnCube();
        }
    }

    private void Explode()
    {
        Instantiate(_effect, transform.position, transform.rotation);

        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
