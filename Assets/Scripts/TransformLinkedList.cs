using System;
using UnityEngine;
using System.Collections;

namespace LinkedList
{
    //Tod: Be smart, return the new nodes.
    class TransformLinkedList : IEnumerator, IEnumerable // Makes it iteratable
    {
        class Node
        {
            public Transform value;
            public Node nextNode;

            public Node() // Default cTor (Constructor)
            { }

            public Node(Transform value)
            {
                this.value = value;
            }

            public Node(Node node) // Copy cTor
            {
                value = node.value;
                nextNode = node.nextNode;
            }

            ~Node() //dTor (Destructor)
            {
                Console.WriteLine("running dTor");
            }

            public void MoveNode(Vector3 position)
            {
                var oldPos = value.position;
                value.position = position;
                if (nextNode != null)
                    nextNode.MoveNode(oldPos);
            }
        }

        private Node head;
        private int iteratorPosition;

        //Property
        public Transform this[int index] // Overloading []
        {
            get => GetValue(index); // Expression body
            set => SetValue(index, value);
        }

        //Automatic property
        public int Count
        { get; private set; }

        public object Current => GetValue(iteratorPosition);

        /// <summary>
        /// Sets a vlue of the node at the current position.
        /// </summary>


        public void MoveHead(Vector3 movement)
        {
            var oldPos = head.value.position;
            head.value.Translate(movement);
            if (head.nextNode != null)
                head.nextNode.MoveNode(oldPos);
        }

        public void SetValue(int index, Transform value)
        {
            if (index < 0 || index >= Count || head == null)
                throw new IndexOutOfRangeException();

            Node currentNode = head;

            for (int i = 0; i < index && currentNode != null; i++)
            {
                if (index == i)
                {
                    currentNode.value = value;
                    return;
                }
                currentNode = currentNode.nextNode;
                //continue; // contiune to the next iteration
                //break; // Jump out of the loop
            }
        }

        private Transform GetValue(int index)
        {
            if (index < 0 || index >= Count || head == null)
                throw new IndexOutOfRangeException();

            Node currentNode = head;

            for (int i = 0; i <= index && currentNode != null; i++)
            {
                if (index == i)
                {
                    return currentNode.value;
                }
                currentNode = currentNode.nextNode;
            }
            return currentNode.value; //Should never happen
        }

        /// <summary>
        /// Adds a new node at the position of index.
        /// </summary>
        public void InsertAt(int index, Transform value)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();

            Node newNode = new Node(value);
            if (index == 0)
            {
                newNode.nextNode = head;
                head = newNode;
                Count++;
                return;
            }

            Node node = GetNode(index - 1);
            newNode.nextNode = node.nextNode;
            node.nextNode = newNode;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();

            if (index == 0)
            {
                head = head.nextNode;
                Count--;
                return;
            }

            Node previousNode = GetNode(index - 1);
            previousNode.nextNode = previousNode.nextNode?.nextNode;
            Count--;
        }

        /// <summary>
        /// Adds a new node at the start of the list
        /// </summary>
        /// <param name="value"></param>
        public void Push(Transform value)
        {
            Node newNode = new Node(value);
            newNode.nextNode = head;
            head = newNode;
            Count++;
        }

        /// <summary>
        /// Adds a new node at the end of the list
        /// </summary>
        /// <param name="value"></param>
        public void Append(Transform value)
        {
            Node node = new Node(value);
            if(head == null)
            {
                head = node;
                Count++;
                return;
            }

            Node currentNode = head;
            while(currentNode.nextNode != null)
            {
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = node;
            Count++;
        }

        private Node GetNode(int index)
        {
            if (index < 0 || index >= Count || head == null)
                throw new IndexOutOfRangeException();

            Node currentNode = head;
            for (int i = 0; i < index; i++)
            {
                if (currentNode == null)
                    throw new IndexOutOfRangeException();

                if (index == i)
                    return currentNode;

                currentNode = currentNode.nextNode;
            }
            return null;
        }
        

        public bool MoveNext()
        {
            iteratorPosition++;
            return iteratorPosition < Count;
        }

        public void Reset()
        {
            iteratorPosition = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }
    }
}
