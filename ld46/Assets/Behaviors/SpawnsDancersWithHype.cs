using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnsDancersWithHype : MonoBehaviour
{
  public GameObject[] dancerPrefabs;
  public int maxDancers;
  public int hypePerDancer;
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

  private Vector2 GetRandomPoint()
  {
    var collider = dancerSpawnArea.GetComponent<PolygonCollider2D>();
    return Vector2.Lerp(
      Vector2.Lerp(collider.points[0], collider.points[1], Random.value),
      Vector2.Lerp(collider.points[2], collider.points[3], Random.value),
      Random.value
    );
  }

  private void OnHypeUpdated(float obj)
  {
    int requiredDancers = Math.Min(maxDancers, (int)metrics.Hype / hypePerDancer);

    DestroyExcessDancers(requiredDancers);
    SpawnRequiredDancers(requiredDancers);
  }

  private void SpawnRequiredDancers(int requiredDancers)
  {
    while (requiredDancers > instances.Count)
    {
      var randomPoint = GetRandomPoint();
      int prefabIndex = random.Next(dancerPrefabs.Length);
      var newDancer = Instantiate(dancerPrefabs[prefabIndex]);
      newDancer.transform.position = new Vector3(randomPoint.x, randomPoint.y, 170 + randomPoint.y / 10);
      FlipXAxis(newDancer);
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
    shouldFlip = !shouldFlip;
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
