using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLPredictor.Neural_Nets
{
    public class Network
    {
        public const double ALPHA = 0.01;

        public List<Node> InputNodes { get; set; }
        public List<Node> HiddenNodes { get; set; }
        public Node OutputNode { get; set; }

        public void InitializeNetwork()
        {
            Random rand = new Random();
            //Initialize input nodes
            for (int i = 0; i < 54; i++)
            {
                Node inputNode = new Node
                {
                    NodeType = NodeType.Input,
                    NodeNum = i,
                    Connections = new Dictionary<Node, double>()
                };
                if (i == 0) //Initialize "bias"
                {
                    inputNode.NodeValue = 1;
                }

                InputNodes.Add(inputNode);
                
            }

            //Initialize hidden nodes
            for (int i = 0; i < 6; i++)
            {
                Node hiddenNode = new Node
                {
                    NodeType = NodeType.Hidden,
                    NodeNum = i,
                    Connections = new Dictionary<Node, double>()
                };
                if (i != 0)
                {
                    foreach (Node node in InputNodes)
                    {
                        node.AddConnection(hiddenNode, rand.NextDouble() - 0.5);
                    } 
                }
                else //Initialize bias
                {
                    hiddenNode.NodeValue = 1;
                }
                HiddenNodes.Add(hiddenNode);
            }

            //Initialize output node
            OutputNode = new Node
            {
                NodeType = NodeType.Output,
                NodeNum = 0,
                Connections = new Dictionary<Node,double>()
            };
            foreach (Node node in HiddenNodes)
            {
                node.AddConnection(OutputNode, rand.NextDouble() - 0.5);
            }

        }

        public double Run(SeasonSnapshot homeTeam, SeasonSnapshot awayTeam)
        {
            #region " Setting Input Values "

            InputNodes[1].NodeValue = homeTeam.WinPercentage;
            InputNodes[2].NodeValue = homeTeam.PointsFor;
            InputNodes[3].NodeValue = homeTeam.PointsAgainst;
            InputNodes[4].NodeValue = homeTeam.TotalYards;
            InputNodes[5].NodeValue = homeTeam.TotalYardsAllowed;
            InputNodes[6].NodeValue = homeTeam.PassingYards;
            InputNodes[7].NodeValue = homeTeam.PassingYardsAllowed;
            InputNodes[8].NodeValue = homeTeam.RushingYards;
            InputNodes[9].NodeValue = homeTeam.RushingYardsAllowed;
            InputNodes[10].NodeValue = homeTeam.TurnoverMargin;
            InputNodes[11].NodeValue = homeTeam.StartingQuarterback.CompletionPercentage;
            InputNodes[12].NodeValue = homeTeam.StartingQuarterback.PassingYards;
            InputNodes[13].NodeValue = homeTeam.StartingQuarterback.Touchdowns;
            InputNodes[14].NodeValue = homeTeam.StartingQuarterback.Interceptions;
            InputNodes[15].NodeValue = homeTeam.SkillPlayers[0].Yards;
            InputNodes[16].NodeValue = homeTeam.SkillPlayers[0].Touchdowns;
            InputNodes[17].NodeValue = homeTeam.SkillPlayers[0].Fumbles;
            InputNodes[18].NodeValue = homeTeam.SkillPlayers[1].Yards;
            InputNodes[19].NodeValue = homeTeam.SkillPlayers[1].Touchdowns;
            InputNodes[20].NodeValue = homeTeam.SkillPlayers[1].Fumbles;
            InputNodes[21].NodeValue = homeTeam.SkillPlayers[2].Yards;
            InputNodes[22].NodeValue = homeTeam.SkillPlayers[2].Touchdowns;
            InputNodes[23].NodeValue = homeTeam.SkillPlayers[2].Fumbles;
            InputNodes[24].NodeValue = awayTeam.WinPercentage;
            InputNodes[25].NodeValue = awayTeam.PointsFor;
            InputNodes[26].NodeValue = awayTeam.PointsAgainst;
            InputNodes[27].NodeValue = awayTeam.TotalYards;
            InputNodes[28].NodeValue = awayTeam.TotalYardsAllowed;
            InputNodes[29].NodeValue = awayTeam.PassingYards;
            InputNodes[30].NodeValue = awayTeam.PassingYardsAllowed;
            InputNodes[31].NodeValue = awayTeam.RushingYards;
            InputNodes[32].NodeValue = awayTeam.RushingYardsAllowed;
            InputNodes[33].NodeValue = awayTeam.TurnoverMargin;
            InputNodes[34].NodeValue = awayTeam.StartingQuarterback.CompletionPercentage;
            InputNodes[35].NodeValue = awayTeam.StartingQuarterback.PassingYards;
            InputNodes[36].NodeValue = awayTeam.StartingQuarterback.Touchdowns;
            InputNodes[37].NodeValue = awayTeam.StartingQuarterback.Interceptions;
            InputNodes[38].NodeValue = awayTeam.SkillPlayers[0].Yards;
            InputNodes[39].NodeValue = awayTeam.SkillPlayers[0].Touchdowns;
            InputNodes[40].NodeValue = awayTeam.SkillPlayers[0].Fumbles;
            InputNodes[41].NodeValue = awayTeam.SkillPlayers[1].Yards;
            InputNodes[42].NodeValue = awayTeam.SkillPlayers[1].Touchdowns;
            InputNodes[43].NodeValue = awayTeam.SkillPlayers[1].Fumbles;
            InputNodes[44].NodeValue = awayTeam.SkillPlayers[2].Yards;
            InputNodes[45].NodeValue = awayTeam.SkillPlayers[2].Touchdowns;
            InputNodes[46].NodeValue = awayTeam.SkillPlayers[2].Fumbles;

            #endregion

            //Setting hidden node values
            foreach (Node hiddenNode in HiddenNodes)
            {
                double hiddenNodeSum = 0;
                if (hiddenNode.NodeNum != 0)
                {
                    foreach (Node inputNode in InputNodes)
                    {
                        hiddenNodeSum += inputNode.NodeValue * inputNode.Connections[hiddenNode];
                    }
                }
                hiddenNode.NodeSum = hiddenNodeSum; //a
                hiddenNode.NodeValue = ActivationFunction(hiddenNodeSum); //z
            }

            //Setting output node value
            double outputNodeSum = 0;
            foreach (Node hiddenNode in HiddenNodes)
            {
                outputNodeSum += hiddenNode.NodeValue * hiddenNode.Connections[OutputNode];
            }
            OutputNode.NodeSum = outputNodeSum;
            OutputNode.NodeValue = ActivationFunction(outputNodeSum);
            return OutputNode.NodeValue;
        }

        public void UpdateWeights(bool didHomeTeamWin)
        {
            int expectedValue = didHomeTeamWin ? 1 : 0;
            
            //Update weights of each hidden node
            foreach (Node hiddenNode in HiddenNodes)
            {
                double delta2 = (expectedValue - OutputNode.NodeValue) * ActivationFunctionDerivative(OutputNode.NodeSum);
                hiddenNode.Connections[OutputNode] = hiddenNode.Connections[OutputNode] +
                    ALPHA * (hiddenNode.NodeValue * delta2);
            }

            //Update weights of each input node
            foreach (Node inputNode in InputNodes)
            {
                foreach (Node hiddenNode in HiddenNodes)
                {
                    double delta1 = ActivationFunctionDerivative(inputNode.Connections[hiddenNode]) * hiddenNode.Connections[OutputNode] *
                        (expectedValue - OutputNode.NodeValue) * ActivationFunctionDerivative(OutputNode.NodeSum);

                    inputNode.Connections[hiddenNode] += ALPHA * (inputNode.NodeValue * delta1);
                }
            }
        }

        private double ActivationFunction(double sum)
        {
            return 1 / (1 + Math.Pow(Math.E, -sum));
        }

        private double ActivationFunctionDerivative(double input)
        {
            return (1 - ActivationFunction(input)) * ActivationFunction(input);
        }
    }
}
