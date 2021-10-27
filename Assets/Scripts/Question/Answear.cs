using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Answear : MonoBehaviour
{
    [SerializeField] private TMP_Text _aswerVariant;
    [SerializeField] private int _answearCost;

    public event UnityAction<int> RightAnswear;
    public bool IsRight { get; private set; }

    public void SetAwswear(string answear, bool isRight)
    {
        _aswerVariant.text = answear;
        IsRight = isRight;
    }

    public void ResetAnswear()
    {
        _aswerVariant.text = "";
        IsRight = false;
    }

    public void OnClickMouse()
    {
        if (IsRight)        
        {
            RightAnswear?.Invoke(_answearCost);
        }
    }
}
