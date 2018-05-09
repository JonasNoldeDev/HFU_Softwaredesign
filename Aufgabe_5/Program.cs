using System;
using System.Collections.Generic;

namespace Aufgabe_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Tree<String>();
            var root = tree.CreateNode("root");
            var child1 = tree.CreateNode("child1");
            var child2 = tree.CreateNode("child1");
            root.AppendChild(child1);
            root.AppendChild(child2);
            var grand11 = tree.CreateNode("grand11");
            var grand12 = tree.CreateNode("grand12");
            var grand13 = tree.CreateNode("grand13");
            child1.AppendChild(grand11);
            child1.AppendChild(grand12);
            child1.AppendChild(grand13);
            var grand21 = tree.CreateNode("grand21");
            child2.AppendChild(grand21);
            child1.RemoveChild(grand12);

            root.PrintTree();

            String searchFor = "child1";
            var resultList = root.Find(searchFor);

            Console.WriteLine();
            Console.WriteLine("Gefundene Knoten mit dem Inhalt \"" + searchFor + "\":");
            if (resultList.Count == 0) Console.WriteLine("Keine Knoten gefunden");
            foreach(var element in resultList) Console.WriteLine(element._nodeContent.ToString());
        }

        public class Tree<G>
        {
            public TreeNode<G> CreateNode(G content)
            {
                return new TreeNode<G>(content);
            }
        }

        public class TreeNode<G>
        {
            public G _nodeContent;
            public TreeNode<G> _parentNode;
            public List<TreeNode<G>> _childNodes;

            public TreeNode(G content)
            {
                _nodeContent = content;
                _childNodes = new List<TreeNode<G>>();
            }

            public void AppendChild(TreeNode<G> nodeToAdd)
            {
                _childNodes.Add(nodeToAdd);
                nodeToAdd._parentNode = this;
            }

            public void RemoveChild(TreeNode<G> nodeToRemove)
            {
                _childNodes.Remove(nodeToRemove);
            }

            public void PrintTree(String levelBuffer = "")
            {
                Console.WriteLine(levelBuffer + _nodeContent.ToString());

                if (_childNodes.Count > 0)
                {
                    foreach(var node in _childNodes)
                    {
                        node.PrintTree(levelBuffer + "*");
                    }
                }
            }
            public List<TreeNode<G>> Find(G contentToFind, List<TreeNode<G>> listToReturn = null)
            {
                if (listToReturn == null)
                {
                    listToReturn = new List<TreeNode<G>>();
                }

                if (_nodeContent.Equals(contentToFind))
                {
                    listToReturn.Add(this);
                }

                if (_childNodes.Count > 0)
                {
                    foreach (var node in _childNodes)
                    {
                        node.Find(contentToFind, listToReturn);
                    }
                }
            
                return listToReturn;
            }
        }
    }
}
