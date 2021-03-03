using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour {

    protected UIType m_UIType;
    public UIType GetUIType
    {
        get { return m_UIType; }
    }

	public virtual void OnEnter()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnExit()
    {
        gameObject.SetActive(false);
    }
}
