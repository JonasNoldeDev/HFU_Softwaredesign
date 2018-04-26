using System;

namespace Aufgabe_4
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
        }

        public class Tree<G>
        {
            public TreeNode<G> CreateNode(G content)
            {
                TreeNode<G> node = new TreeNode<G>();
                node._nodeContent = content;
                return node;
            }
        }

        public class TreeNode<G>
        {
            public TreeNode<G> _parentNode;
            public TreeNode<G>[] _childNodes;
            public int _numberOfChildNodes = 0;
            public G _nodeContent;

            public void AppendChild(TreeNode<G> node)
            {
                if (_numberOfChildNodes == 0)
                {
                    _childNodes = new TreeNode<G>[] { node };
                }
                else
                {
                    TreeNode<G>[] childNodesOld = _childNodes;
                    _childNodes = new TreeNode<G>[_numberOfChildNodes+1];
                    Array.Copy(childNodesOld, _childNodes, _numberOfChildNodes);
                    _childNodes[_numberOfChildNodes] = node;
                }

                _numberOfChildNodes++;
                node._parentNode = this;
            }

            public void RemoveChild(TreeNode<G> node)
            {
                if (_numberOfChildNodes == 0)
                {
                    Console.WriteLine(node._nodeContent + ": This TreeNode doesn't have any childNodes");
                }
                else
                {
                    Boolean found = false;
                    for (int i = 0; i < _childNodes.Length - 1; i++)
                    {
                        if (_childNodes[i].Equals(node))
                        {
                            found = true;
                        }

                        if (found)
                        {
                            _childNodes[i] = _childNodes[i+1];
                        }
                    }

                    _numberOfChildNodes--;

                    TreeNode<G>[] childNodesOld = _childNodes;
                    _childNodes = new TreeNode<G>[_numberOfChildNodes];
                    Array.Copy(childNodesOld, _childNodes, _numberOfChildNodes);
                }
            }

            public void PrintTree(String levelBuffer = "")
            {
                Console.WriteLine(levelBuffer + _nodeContent.ToString());

                if (_numberOfChildNodes > 0)
                {
                    foreach(var node in _childNodes)
                    {
                        node.PrintTree(levelBuffer + "*");
                    }
                }
            }
        }
    }
}
