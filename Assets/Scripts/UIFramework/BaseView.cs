using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseView
{
    protected Transform CanvasTransform;

    protected Transform viewNode;

    public virtual void InitPreLoadView()
    {
  
    }

    public virtual void CloseView()
    {

    }

    public virtual void DestroyView()
    {

    }
}
