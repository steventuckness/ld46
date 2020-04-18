using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HypeSlider : MonoBehaviour
{
  public GameObject global;

  private SpawnsDancersWithHype dancerSpawnBehavior;
  private Slider slider;

  void Start()
  {
    slider = this.GetComponent<Slider>();
    dancerSpawnBehavior = global.GetComponent<SpawnsDancersWithHype>();
    slider.onValueChanged.AddListener(UpdateDancerHype);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void UpdateDancerHype(float hypeValue)
  {
    dancerSpawnBehavior.hype = (int)hypeValue;
  }

  // This is a little goofy since it won't run until GC
  ~HypeSlider()
  {
    this.slider.onValueChanged.RemoveListener(UpdateDancerHype);
  }
}
