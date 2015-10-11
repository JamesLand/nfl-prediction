using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Neural_Nets
{
    public class Node
    {
        public NodeType NodeType { get; set; }
        public int NodeNum { get; set; }
        public double NodeValue { get; set; }
        public double NodeSum { get; set; }
        public Dictionary<Node, double> Connections { get; set; }

        public void AddConnection(Node destNode, double initialWeight)
        {
            Connections.Add(destNode, initialWeight); //TODO: Initialize to something not = 0?
        }

        public void Activate()
        {

        }

        public void ReceiveActivation()
        {

        }
    }

    public enum NodeType
    {
        Input,
        Hidden,
        Output
    }
}
