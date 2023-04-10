using System;
using System.Collections.Generic;

public class Position : IComparer<Position>, IComparable<Position>
{
    public int CurrentCheckpoint;

    public int CurrentLap;

    public float DistanceToNextCheckpoint;

    public Position(int currentCheckpoint, int currentLap, float distanceToNextCheckpoint)
    {
        CurrentCheckpoint = currentCheckpoint;
        CurrentLap = currentLap;
        DistanceToNextCheckpoint = distanceToNextCheckpoint;
    }

    public int Compare(Position x, Position y)
    {
        if (x.CurrentLap != y.CurrentLap)
        {
            return y.CurrentLap - x.CurrentLap;
        }
        else
        {
            if (x.CurrentCheckpoint != y.CurrentCheckpoint)
            {
                return y.CurrentCheckpoint - x.CurrentCheckpoint;
            }
            else
            {
                return (int) (x.DistanceToNextCheckpoint - y.DistanceToNextCheckpoint);
            }
        }

    }

    public int CompareTo(Position other)
    {
        if (CurrentLap != other.CurrentLap)
        {
            return other.CurrentLap - CurrentLap;
        }
        
        if (CurrentCheckpoint != other.CurrentCheckpoint)
        {
            return other.CurrentCheckpoint - CurrentCheckpoint;
        }

        return (int) (DistanceToNextCheckpoint - other.DistanceToNextCheckpoint);
    }
}
