//Authored by Nathan Beattie

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTest
{
    /// <summary>
    /// Holds all gestures in a graph. The graph is navigated from a movement sequence
    /// </summary>
    class GestureTree
    {
        //declare variables
        private Node root;
        public Node currentNode;
        private List<Gesture> allGestures = new List<Gesture>();

        /// <summary>
        /// Constructor
        /// </summary>
        public GestureTree()
        {
            root = new Node();
            currentNode = root;
        }

        /// <summary>
        /// Node class for the graph. Stores a gesture and surrounding nodes.
        /// </summary>
        public class Node
        {
            public Gesture gesture;
            public int dirValue;
            public List<Node> leafNodes;
            public Node baseNode;

            /// <summary>
            /// Node constructor
            /// </summary>
            /// <param name="g">The gesture assigned to the Node</param>
            /// <param name="d">the direction of this node from the previous node</param>
            /// <param name="bNode">the previous node</param>
            public Node(Gesture g = null, int d = 0, Node bNode = null)
            {
                gesture = g;
                dirValue = d;
                baseNode = bNode;
                leafNodes = new List<Node>();
            }
        }

        /// <summary>
        /// Adds a gesture to the graph
        /// </summary>
        /// <param name="g">the gesture to add to the graph</param>
        public void AddGesture(Gesture g)
        {
            ReturnToRoot();
            //travel through tree, adding nodes as necessary
            int[] sequence = g.GetSequence();
            for (int i = 0; i < sequence.Length; i++)
            {
                if (!NextDirExists(sequence[i]))
                {
                    currentNode.leafNodes.Add(new Node(null, sequence[i], currentNode));
                    SelectNode(sequence[i]);
                }
                else
                {
                    SelectNode(sequence[i]);
                }
            }
            //will now be at the final node
            currentNode.gesture = g;

            //adds gesture to list of all gestures. Basically a lazy way of finding all gestures without traversing the tree
            if (g != null)
            {
                allGestures.Add(g);
            }
            
        }

        /// <summary>
        /// Removes a gesture from the graph
        /// </summary>
        /// <param name="g">the gesture to remove from the graph</param>
        public void RemoveGesture(Gesture g)
        {
            ReturnToRoot();
            //travel through tree, adding nodes as necessary
            int[] sequence = g.GetSequence();
            for (int i = 0; i < sequence.Length; i++)
            {
                SelectNode(sequence[i]);
            }
            //will now be at the final node
            //remove the gesture
            currentNode.gesture = null;

            //remove gesture from list
            if (g != null)
            {
                allGestures.Remove(g);
            }

        }

        /// <summary>
        /// returns to the root node in the graph
        /// </summary>
        public void ReturnToRoot()
        {
            currentNode = root;
        }

        /// <summary>
        /// Travels to the next node in a given direction
        /// </summary>
        /// <param name="dir">The direction of the next node</param>
        /// <returns>Returns false if node doesn't exist, or true if traversal is successful</returns>
        public bool GoToNext(int dir)
        {
            if (!NextDirExists(dir))
            {
                return false;
            }
            else
            {
                SelectNode(dir);
                return true;
            }
        }

        /// <summary>
        /// Tests that a adjacent node exists in a given direction
        /// </summary>
        /// <param name="dir">The direction to test for the given node</param>
        /// <returns>Returns true if node exists, false if not</returns>
        private bool NextDirExists(int dir)
        {
            foreach (Node l in currentNode.leafNodes)
            {
                if (l.dirValue == dir)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets current node as the adjacent node in the given direstion
        /// </summary>
        /// <param name="dir">The direction of the node to select</param>
        private void SelectNode(int dir)
        {
            foreach (Node n in currentNode.leafNodes)
            {
                if (n.dirValue == dir)
                {
                    currentNode = n;
                    break;
                }
            }
        }

        /// <summary>
        /// Returns a gesture based on a given movement sequence
        /// </summary>
        /// <param name="seq">a movement sequence</param>
        /// <returns>returns the gesture associated with the sequence, or null if the gesture doesn't exist</returns>
        public Gesture ReturnGesture(int[] seq)
        {
            ReturnToRoot();
            bool gestureFound = true;
            int[] sequence = seq;
            Gesture gesture = null;
            //traverse the tree
            for (int i = 0; i < sequence.Length; i++)
            {
                if (NextDirExists(sequence[i]))
                {
                    SelectNode(sequence[i]);
                }
                else
                {
                    gestureFound = false;
                    gesture = null;
                    break;
                }
            }
            //will now be at the final node
            if (gestureFound)
            {
                gesture = currentNode.gesture;
            }
            
            return gesture;
        }

        /// <summary>
        /// Returns a list of all gestures in the tree
        /// </summary>
        /// <returns>Returns a list of all gestures in the tree</returns>
        public List<Gesture> ReturnAllGestures()
        {
            return allGestures;
        }

        /// <summary>
        /// Returns a gesture with a given name
        /// </summary>
        /// <param name="name">the name of the gesture to return</param>
        /// <returns>the gesture associated with the given name, or null if the gesture doesn't exist</returns>
        public Gesture ReturnGestureByName(string name)
        {
            Gesture gest = null;
            foreach (Gesture g in allGestures)
            {
                if (g.GetName() == name)
                {
                    gest = g;
                    break;
                }
            }
            return gest;
        }
    }
}
