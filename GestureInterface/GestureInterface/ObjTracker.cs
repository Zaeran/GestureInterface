//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImgTest
{
    /// <summary>
    /// This class gets the movement of an object and transforms them into a movement sequence.
    /// </summary>
    class ObjTracker
    {
        //declare variables
        public double distanceMoved;
        public int dirMoving;
        private int dirLastMoved;
        public int ticksSinceDirectionStarted;
        private List<int> currentSequence;

        /// <summary>
        /// Custom constructor
        /// </summary>
        public ObjTracker()
        {
            distanceMoved = 0;
            dirMoving = 10;
            dirLastMoved = 10;
            ticksSinceDirectionStarted = 0;
            currentSequence = new List<int>();
        }

        //
        /// <summary>
        /// places the directions moved into a sequence list
        /// </summary>
        /// <param name="dist">The distance moved by the recorded object</param>
        /// <param name="dir">The direction moved by the recorded object</param>
        /// <returns>Returns true if the sequence is over, false if not</returns>
        public bool TrackPosition(double dist, int dir)
        {
            //keep note of the amount of time spent moving in a direction.
            //1 tick = 0.1 seconds.
            ticksSinceDirectionStarted++;
            //execute this if movement has occured in the last 0.3 seconds
            if (ticksSinceDirectionStarted < 3 || (dist > 15 && dist != 0))
            {
                //if moving in the same direction, add the distance moved to the current total
                if (dir == dirMoving && dist != 0 && dist > 15)
                {
                    distanceMoved += dist;
                    ticksSinceDirectionStarted = 0;
                }
                else //direction has changed
                {
                    
                    //if moved far enough in a different direction to previous, add the direction to the sequence list
                    if (distanceMoved > 80 && dirMoving != dirLastMoved)
                    {
                        currentSequence.Add(dirMoving);
                        ticksSinceDirectionStarted = 0;
                        dirLastMoved = dirMoving;
                    }
                    distanceMoved = 0;
                    dirMoving = dir;
                }
                //sequence not yet over
                return false;
            }
            else //0.3 seconds of inactivity, gesture sequence complete
            {
                if (dist != 0)
                {
                    distanceMoved = 0;
                    dirMoving = dir;
                    dirLastMoved = 10;
                    if (ticksSinceDirectionStarted > 3)
                    {
                        ticksSinceDirectionStarted = 0;
                    }
                    
                    return true;             
                }
                return false;
            }
        }

        /// <summary>
        /// Returns the movement sequence, clearing the sequence list
        /// </summary>
        /// <returns>Returns the movement sequence</returns>
        public int[] ReturnSequence()
        {
            int[] seq = new int[currentSequence.Count];
            currentSequence.CopyTo(seq, 0);
            currentSequence = new List<int>();
            return seq;
        }

        /// <summary>
        /// Returns the movement sequence, without clearing the sequence list
        /// </summary>
        /// <returns>Returns the movement sequence</returns>
        public int[] ReturnSequenceNoErase()
        {
            int[] seq = new int[currentSequence.Count];
            currentSequence.CopyTo(seq, 0);
            return seq;
        }
    }
}
