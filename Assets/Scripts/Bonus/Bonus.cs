using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _score;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    public int CollectScore()
    {
        return _score;
    }

}
