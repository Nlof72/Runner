using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Spawn _spawner;

    private void OnEnable()
    {
        _player.GameEnded += GameEnd;
    }

    private void OnDisable()
    {
        _player.GameEnded -= GameEnd;
    }

    private void GameEnd()
    {
        _spawner.StopAllCoroutines();
    }
}
