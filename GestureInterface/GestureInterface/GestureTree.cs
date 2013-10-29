using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImgTest
{
    class GestureTree
    {
        private Node root;
        public Node currentNode;
        private List<Gesture> allGestures = new List<Gesture>();

        public GestureTree()
        {
            root = new Node();
            currentNode = root;
        }

        public class Node
        {
            public Gesture gesture;
            public int dirValue;
            public List<Node> leafNodes;
            public Node baseNode;

            public Node(Gesture g = null, int d = 0, Node bNode = null)
            {
                gesture = g;
                dirValue = d;
                baseNode = bNode;
                leafNodes = new List<Node>();
            }
        }

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

        public void ReturnToRoot()
        {
            currentNode = root;
        }

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

        public List<Gesture> ReturnAllGestures()
        {
            return allGestures;
        }

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
