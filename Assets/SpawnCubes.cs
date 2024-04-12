using System.Collections;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 5;

    [SerializeField] private int _chanceSpawn = 100;

    [SerializeField] private float _timeLife = 1.5f;

    public void RunSpawn(int chanceSpawn)
    {
        _chanceSpawn = chanceSpawn;
        SpawnCube();
    }

    public void SpawnCube()
    {
        int hundredPercent = 100;
        int degreeReduction = 2;

        int randomCount = Random.Range(_minCubes, _maxCubes);
        int randomNumber = Random.Range(0, hundredPercent + 1);

        if (randomNumber <= _chanceSpawn)
        {
            _chanceSpawn /= degreeReduction;

            for (int i = 0; i < randomCount; i++)
            {
                GenerateCube();
            }
        }

        StartCoroutine(LifeTime());
    }

    private void GenerateCube()
    {
        float offsetMin = -0.5f;
        float offsetMax = 1.5f;

        int degreeScale = 2;

        Vector3 position = new Vector3(
            Random.Range(offsetMin, offsetMax),
            Random.Range(offsetMin, offsetMax), 
            Random.Range(offsetMin, offsetMax));

        position = transform.TransformPoint(transform.position);

        GameObject cube = Instantiate(_prefab, position, transform.rotation);

        cube.GetComponent<SpawnCubes>().RunSpawn(_chanceSpawn);
        cube.GetComponent<Renderer>().material.color = Random.ColorHSV();

        cube.transform.localScale = transform.localScale / degreeScale;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_timeLife);
        Destroy(gameObject);
    }
}
