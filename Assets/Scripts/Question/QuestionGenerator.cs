using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class QuestionGenerator : MonoBehaviour
{
    [SerializeField] private Answear[] _answears; 
    [SerializeField] private TMP_Text _question;
    [SerializeField] private Vector2 _numberRnage;
    [SerializeField] private Player _player;


    private string _equsion;
    private float _answear;
    public UnityAction AnswearEnded;

    private void OnEnable()
    {
        _player.GameEnded += onGameEnded;
        foreach(var answear in _answears)
        {
            answear.RightAnswear += Answear_RightAnswear;
        }
    }

    private void OnDisable()
    {
        _player.GameEnded -= onGameEnded;
        foreach (var answear in _answears)
        {
            answear.RightAnswear -= Answear_RightAnswear;
        }
    }
    private void Answear_RightAnswear(int answearScore)
    {
        _player.AddScore(answearScore);
        foreach (var answear in _answears)
        {
            answear.ResetAnswear();
        }
        _question.text = "";
        AnswearEnded?.Invoke();
    }

    public void GenerateQuestion()
    {
        int randomNumber = GetRandomNumber();
        _answear = randomNumber;
        _equsion = randomNumber.ToString();
        randomNumber = GetRandomNumber();

        switch (Random.Range(0, 3))
        {
            case 0:
                _answear += randomNumber;
                _equsion += "+" + randomNumber;
                break;
            case 1:
                _answear -= randomNumber;
                _equsion += "-" + randomNumber;
                break;
            case 2:
                _answear *= randomNumber;
                _equsion += "*" + randomNumber;
                break;
        }

        int index = Random.Range(0, 3);
        _question.text = _equsion + "= ?";
        switch (index)
        {
            case 0:
                _answears[0].SetAwswear(_answear.ToString(), true);
                _answears[1].SetAwswear((_answear + Random.Range(-(int)_answear, (int)_answear)).ToString(), false);
                _answears[2].SetAwswear((_answear + Random.Range(-(int)_answear, (int)_answear)).ToString(), false);
                break;
            case 1:
                _answears[0].SetAwswear((_answear + Random.Range(-(int)_answear, (int)_answear)).ToString(), false);
                _answears[1].SetAwswear(_answear.ToString(), true);
                _answears[2].SetAwswear((_answear + Random.Range(-(int)_answear, (int)_answear)).ToString(), false);
                break;
            case 2:

                _answears[0].SetAwswear((_answear + Random.Range(-(int)_answear, (int)_answear)).ToString(), false);
                _answears[1].SetAwswear((_answear + Random.Range(-(int)_answear, (int)_answear)).ToString(), false);
                _answears[2].SetAwswear(_answear.ToString(), true);
                break;
        }

        Debug.Log(index);

    }

    private int GetRandomNumber()
    {
        return (int)Random.Range(_numberRnage.x, _numberRnage.y);
    }

    private void onGameEnded()
    {
        _question.text = "Your lose this game";
        foreach(var answear in _answears)
        {
            answear.gameObject.SetActive(false);
        }
    }
}
