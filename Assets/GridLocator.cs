using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GridLocator : MonoBehaviour
{
    [SerializeField] AssetReference assetReference;

    class Entity
    {
        public Transform transform;
    }
    List<Entity> entities = new List<Entity>();

    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetAsync<GameObject>(assetReference).Completed += op =>
        {
            if (op.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"{op.Status}");
                return;
            }
            var prefab = op.Result;
            for (var i = 0; i < 200; i++)
            {
                var p = 10 * Random.insideUnitCircle;
                var obj = Instantiate<GameObject>(prefab, transform);
                obj.transform.position = new Vector3(p.x, 0, p.y);
                entities.Add(new Entity { transform = obj.transform });
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        var n = entities.Count;
        if (0 < n)
        {
            for (var i = 0; i < 100; i++)
            {
                var e1 = entities[Random.Range(0, n)];
                var e2 = entities[Random.Range(0, n)];
                if (e1 == e2)
                {
                    continue;
                }
                var v = e1.transform.position - e2.transform.position;
                var d = Vector3.SqrMagnitude(v);
                if (d < 4)
                {
                    e1.transform.position += 0.05f * v.normalized;
                    e2.transform.position -= 0.05f * v.normalized;
                }
            }
        }
    }
}
