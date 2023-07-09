using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverEventArgs : EventArgs
{
    public bool finishReached;

    public GameOverEventArgs(bool finishReached)
    {
        this.finishReached = finishReached;
    }
}
