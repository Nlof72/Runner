using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _turnSpeed;
    [SerializeField] private TMP_Text _textScore;
    [SerializeField] private QuestionGenerator _questionGenerator;

    private int _score;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;

    public UnityAction GameEnded;
    public bool GameIsLose { private set; get; }

    private void OnEnable()
    {
        _questionGenerator.AnswearEnded += StartNewQuestion;
    }

    private void OnDisable()
    {
        _questionGenerator.AnswearEnded -= StartNewQuestion;
    }

    private void StartNewQuestion()
    {
        if(!GameIsLose) _questionGenerator.GenerateQuestion();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _score = 0;
        GameIsLose = false;
        Invoke(nameof(StartQuiz), 4f);
    }


    private void StartQuiz()
    {
        _questionGenerator.gameObject.SetActive(true);
        _questionGenerator.GenerateQuestion();

    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !GameIsLose)
        {
            if(_playerInput.CheckPositionCursor())
            {
                transform.Translate(_playerInput.GetDirectionToClick(transform.position) * _turnSpeed * Time.deltaTime);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.gameObject.TryGetComponent(out Obstacle obstacle) && !GameIsLose)
        {
            _score -= 1;
            UpdateScore();
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Bonus bonus) && !GameIsLose)
        {
            
            _score += bonus.CollectScore();
            UpdateScore();
            Destroy(bonus.gameObject);
        }
    }

    private void UpdateScore()
    {
        if (_score < 0)
        {
            GameEnd();
        }
        _textScore.text = "Score is: " + _score.ToString();
    }

    public void AddScore(int count)
    {
        _score += count;
        UpdateScore();
    }

    private void GameEnd()
    {
        _score = 0;
        UpdateScore();
        GameEnded?.Invoke();
        GameIsLose = true;
        _textScore.gameObject.SetActive(false);
    }
}
