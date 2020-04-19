using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnsDancersWithHype : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject[] dancerPrefabs;
  public int maxDancers = 10;
  public int hypePerDancer = 10;
  public GameObject dancerSpawnArea;
  public GameObject metricsObject;

  bool shouldFlip = false;
  List<GameObject> instances = new List<GameObject>();
  System.Random random = new System.Random();
  Metrics metrics;

  void Start()
  {
    metrics = metricsObject.GetComponent<Metrics>();
    metrics.HypeUpdated += OnHypeUpdated;
    OnHypeUpdated(metrics.Hype);
  }

  private void OnHypeUpdated(float obj)
  {
    int requiredDancers = Math.Min(maxDancers, (int)metrics.Hype / hypePerDancer);

    DestroyExcessDancers(requiredDancers);
    SpawnRequiredDancers(requiredDancers);
  }

  private void SpawnRequiredDancers(int requiredDancers)
  {
    var bounds = dancerSpawnArea.GetComponent<Collider>().bounds;
    float minX = bounds.min.x,
          minY = bounds.min.y,
          maxX = bounds.max.x,
          maxY = bounds.max.y;

    while (requiredDancers > instances.Count)
    {
      int prefabIndex = random.Next(dancerPrefabs.Length);
      var newDancer = Instantiate(dancerPrefabs[prefabIndex]);
      newDancer.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 1);
      FlipXAxis(newDancer);
      shouldFlip = !shouldFlip;
      instances.Add(newDancer);
    }
  }

  private void FlipXAxis(GameObject newDancer)
  {
    if (shouldFlip)
    {
      var scale = newDancer.transform.localScale;
      scale.Scale(new Vector3(-1, 1, 1));
      newDancer.transform.localScale = scale;
    }
  }

  void DestroyExcessDancers(int requiredDancers)
  {
    while (requiredDancers < instances.Count)
    {
      var randomDancer = instances[random.Next(instances.Count)];
      instances.Remove(randomDancer);
      Destroy(randomDancer);
    }
  }
}
