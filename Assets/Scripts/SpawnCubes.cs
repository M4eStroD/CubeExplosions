using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    [SerializeField] private Explosion _explodeCube;
    [SerializeField] private Renderer _renderer;

    [SerializeField] private int _minCubes = 2;
    [SerializeField] private int _maxCubes = 5;

    private GameObject _cube;

    private void Start()
    {
        _cube = _explodeCube.gameObject;
        _renderer.material.color = Random.ColorHSV();
    }

    public void SpawnCube()
    {
        int randomCount = Random.Range(_minCubes, _maxCubes + 1);

        for (int i = 0; i < randomCount; i++)
        {
            GenerateCube();
        }
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

        var cube = Instantiate(_cube, position, transform.rotation);

        cube.transform.localScale = transform.localScale / degreeScale;
    }
}
