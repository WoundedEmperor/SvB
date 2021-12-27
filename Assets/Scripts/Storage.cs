using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage
{
    public Line[] storage = new Line[3];
    public void AddLine(Line line)
    {
        storage[2] = storage[1];
        storage[1] = storage[0];
        storage[0] = line;
    }

    public int GetLineDurability(int line)
    {
        if (storage[line] != null)
        {
            int smallBlock = storage[line].SmallBlock();
            return smallBlock;
        }
        else
        {
            return 0;
        }
        
    }

    public int GetLineBonus(int line)
    {
        if (storage[line] != null)
        {
            int smallBall = storage[line].SmallBall();
            return smallBall;
        }
        else
        {
            return 0;
        }
    }
}

