//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTest
{
    class ObjTracker
    {
        public double distanceMoved;
        public int dirMoving;
        private int dirLastMoved;
        public int ticksSinceDirectionStarted;
        private List<int> currentSequence;

        public ObjTracker()
        {
            distanceMoved = 0;
            dirMoving = 10;
            dirLastMoved = 10;
            ticksSinceDirectionStarted = 0;
            currentSequence = new List<int>();
        }

        //places the directions moved into a sequence list
        public bool TrackPosition(double dist, int dir)
        {
            ticksSinceDirectionStarted++;
            //if moved a small distance, and for less than 1.2 seconds
            if (ticksSinceDirectionStarted < 5 || dist > 15)
            {
                //if moving in the same direction, add the distance moved
                if (dir == dirMoving && dist != 0)
                {
                    distanceMoved += dist;
                    ticksSinceDirectionStarted = 0;
                }
                else
                {
                    if (distanceMoved > 50 && dirMoving != dirLastMoved)
                    {
                        currentSequence.Add(dirMoving);
                        ticksSinceDirectionStarted = 0;
                        dirLastMoved = dirMoving;
                    }
                    distanceMoved = 0;
                    dirMoving = dir;
                }
                return false;
            }
            else
            {
                distanceMoved = 0;
                dirMoving = dir;
                dirLastMoved = 10;
                if (ticksSinceDirectionStarted > 5)
                {
                    ticksSinceDirectionStarted = 0;
                }
                return true;
            }
        }

        public int[] ReturnSequence()
        {
            int[] seq = new int[currentSequence.Count];
            currentSequence.CopyTo(seq, 0);
            currentSequence = new List<int>();
            return seq;
        }

        public int[] ReturnSequenceNoErase()
        {
            int[] seq = new int[currentSequence.Count];
            currentSequence.CopyTo(seq, 0);
            //currentSequence = new List<int>();
            return seq;
        }
    }
}
