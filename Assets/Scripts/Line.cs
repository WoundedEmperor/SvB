using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line
{
    public int[] blocks = new int[5];
    public int[] balls;
    public Transform LineHub;
    public int SmallBlock()
    {
        int smallestBlock = blocks[0];
        for (int i = 1; i < blocks.Length; i++)
        {
            if (blocks[i] < smallestBlock)
            {
                smallestBlock = blocks[i];
            }
        }
        return smallestBlock;
    }
    public int SmallBall()
    {
        int smallestBall = balls[0];
        for (int i = 1; i < balls.Length; i++)
        {
            if (balls[i] < smallestBall)
            {
                smallestBall = balls[i];
            }
        }
        return smallestBall;
    }

    public Line(int[] blocksVar, int[] ballsVar)
    {
        blocks = blocksVar;
        balls = ballsVar;

    }
}
