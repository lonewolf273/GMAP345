﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Controls
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    SHOOT,
    FOCUS,
    PAUSE
};

public class KeyboardController {



    static protected Dictionary<Controls, KeyCode>[] control =
    {
        new Dictionary<Controls, KeyCode>()
        {
            {Controls.UP, KeyCode.W},
            {Controls.DOWN, KeyCode.S},
            {Controls.LEFT, KeyCode.A},
            {Controls.RIGHT, KeyCode.D},
            {Controls.SHOOT, KeyCode.Space},
            {Controls.FOCUS, KeyCode.LeftShift},
            {Controls.PAUSE, KeyCode.Escape},
        },
        new Dictionary<Controls, KeyCode>()
        {
            {Controls.UP, KeyCode.UpArrow},
            {Controls.DOWN, KeyCode.DownArrow},
            {Controls.LEFT, KeyCode.LeftArrow},
            {Controls.RIGHT, KeyCode.RightArrow},
            {Controls.SHOOT, KeyCode.RightControl},
            {Controls.FOCUS, KeyCode.RightShift},
            {Controls.PAUSE, KeyCode.Escape},
        },
    };

    public void setControl(int p, Controls c, KeyCode k) //sets the controls of player # to 
    {
        if (p <= 0 || p > 2) return;
        foreach(var d in control)
        {
            foreach(var i in d)
            {
                if (i.Value == k) return;
            }
        }
        control[p - 1][c] = k;
    }
    
    public bool Player1Check(Controls c)
    {
        return Input.GetKey(control[0][c]);
    }

    public bool Player1CheckPressed(Controls c)
    {
        return Input.GetKeyDown(control[0][c]);
    }

    public bool Player1CheckReleased(Controls c)
    {
        return Input.GetKeyUp(control[0][c]);
    }

    //whatever
    public bool Player2Check(Controls c)
    {
        return Input.GetKey(control[1][c]);
    }

    public bool Player2CheckPressed(Controls c)
    {
        return Input.GetKeyDown(control[1][c]);
    }

    public bool Player2CheckReleased(Controls c)
    {
        return Input.GetKeyUp(control[1][c]);
    }

    //whatever
    public bool PlayerEitherCheck(Controls c)
    {
        return Player1Check(c) || Player2Check(c);
    }

    public bool PlayerEitherCheckPressed(Controls c)
    {
        return Player1CheckPressed(c) || Player2CheckPressed(c);
    }

    public bool PlayerEitherCheckReleased(Controls c)
    {
        return Player1CheckReleased(c) || Player2CheckReleased(c);
    }

    //check
    public bool PlayerCheck(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1Check(c);
            default:
                return Player2Check(c);
        }
    }

    public bool PlayerCheckPressed(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1CheckPressed(c);
            default:
                return Player2CheckPressed(c);
        }
    }

    public bool PlayerCheckReleased(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1CheckPressed(c);
            default:
                return Player2CheckPressed(c);
        }
    }
}

public class Controller {

    [SerializeField]
    protected float threshold;
    
    protected Dictionary<Controls, string>[] control =
    {
        new Dictionary<Controls, string>()
        {
            {Controls.UP, "Player 1 Vertical"},
            {Controls.DOWN, "Player 1 Vertical"},
            {Controls.LEFT, "Player 1 Horizontal"},
            {Controls.RIGHT, "Player 1 Horizontal"},
            {Controls.SHOOT, "Player 1 Shoot"},
            {Controls.FOCUS, "Player 1 Focus"},
            {Controls.PAUSE, "Cancel"},
        },
        new Dictionary<Controls, string>()
        {
            {Controls.UP, "Player 2 Vertical"},
            {Controls.DOWN, "Player 2 Vertical"},
            {Controls.LEFT, "Player 2 Horizontal"},
            {Controls.RIGHT, "Player 2 Horizontal"},
            {Controls.SHOOT, "Player 2 Shoot"},
            {Controls.FOCUS, "Player 2 Focus"},
            {Controls.PAUSE, "Cancel"},
        },
    };

    public bool Player1Check(Controls c)
    {
        return Input.GetButton(control[0][c]);
    }

    public bool Player1CheckPressed(Controls c)
    {
        return Input.GetButtonDown(control[0][c]);
    }

    public bool Player1CheckReleased(Controls c)
    {
        return Input.GetButtonUp(control[0][c]);
    }
    public float Player1Axis(Controls c)
    {
        if (Mathf.Abs(Input.GetAxis(control[0][c])) > 0.1)
            return Input.GetAxis(control[0][c]);
        return 0f;
    }

    //whatever
    public bool Player2Check(Controls c)
    {
        return Input.GetButton(control[1][c]);
    }

    public bool Player2CheckPressed(Controls c)
    {
        return Input.GetButtonDown(control[1][c]);
    }

    public bool Player2CheckReleased(Controls c)
    {
        return Input.GetButtonUp(control[1][c]);
    }
    public float Player2Axis(Controls c)
    {
        return Input.GetAxis(control[1][c]);
    }

    //whatever
    public bool PlayerEitherCheck(Controls c)
    {
        return Player1Check(c) || Player2Check(c);
    }

    public bool PlayerEitherCheckPressed(Controls c)
    {
        return Player1CheckPressed(c) || Player2CheckPressed(c);
    }

    public bool PlayerEitherCheckReleased(Controls c)
    {
        return Player1CheckReleased(c) || Player2CheckReleased(c);
    }
    public float PlayerEitherAxis(Controls c)
    {
        return Player1Axis(c) + Player2Axis(c);
    }

    //check
    public bool PlayerCheck(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1Check(c);
            default:
                return Player2Check(c);
        }
    }

    public bool PlayerCheckPressed(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1CheckPressed(c);
            default:
                return Player2CheckPressed(c);
        }
    }

    public bool PlayerCheckReleased(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1CheckPressed(c);
            default:
                return Player2CheckPressed(c);
        }
    }

    public float PlayerAxis(int i, Controls c)
    {
        if (i <= 0 || i > 2)
            throw new System.Exception("Invalid Player (PlayerCheck)");
        switch (i)
        {
            case 1:
                return Player1Axis(c);
            default:
                return Player2Axis(c);
        }
    }
}