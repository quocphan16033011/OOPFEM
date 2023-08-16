using DEMSoft.Drawing;
using DEMSoft.Drawing.Geometry;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Model
    {
        private List<Node> listNodes;

        private List<Truss3DElement> listElements;
        private List<Force> listForces;
        private List<Constraint> listConstraints;

        public int NumberOfFeild { get; set; }
        public Model(int numberOfField)
        {
            listNodes = new List<Node>();
            listElements = new List<Truss3DElement>();
            listForces = new List<Force>();
            listConstraints = new List<Constraint>();
            NumberOfFeild = numberOfField;
        }
        public void AddNode(Node node)
        {
            listNodes.Add(node);
        }
        public Node GetNode(int index)
        {
            return listNodes[index];
        }
        public Truss3DElement GetElement(int Elementindex)
        {
            return listElements[Elementindex];
        }
        public void AddElement(Truss3DElement element)
        {
            listElements.Add(element);
        }
        public Force GetForce(int indexForce)
        {
            return listForces[indexForce];
        }
        public void AddForce(Force force)
        {
            listForces.Add(force);
        }
        public Constraint GetConstrain(int indexConstrain)
        {
            return listConstraints[indexConstrain];
        }
        public void AddConstrain(Constraint constraint)
        {
            listConstraints.Add(constraint);
        }
        public void DrawNode(ViewerForm viewer)
        {
            int countNode = listNodes.Count;
            double[] x = new double[countNode];
            double[] y = new double[countNode];
            double[] z = new double[countNode];
            for (int i = 0; i < countNode; i++)
            {
                x[i] = listNodes[i].GetLocation(0);
                y[i] = listNodes[i].GetLocation(1);
                z[i] = listNodes[i].GetLocation(2);
            }
            PointSet ps = new PointSet(x, y, z);
            ps.SetColor(Color.Blue);
            ps.SetPointSize(5);
            viewer.AddObject3D(ps);
        }
        public void DrawElement(ViewerForm viewer)
        {
            int countElement = listElements.Count;
            for (int i = 0; i < countElement; i++)
            {
                Node n0 = listElements[i].GetNode(0);
                Node n1 = listElements[i].GetNode(1);
                Line line = new Line(
                        n0.GetLocation(0),
                        n0.GetLocation(1),
                        n0.GetLocation(2),
                        n1.GetLocation(0),
                        n1.GetLocation(1),
                        n1.GetLocation(2)
                    );
                line.SetColor(Color.Green);
                line.SetWidth(2);
                viewer.AddObject3D(line);
            }
        }
        public void DrawForce(ViewerForm viewer, double scale = 1)
        {
            for (int i = 0; i < listForces.Count; i++)
            {
                Node n = listForces[i].GetNode();
                Line line = new Line(
                        n.GetLocation(0),
                        n.GetLocation(1),
                        n.GetLocation(2),
                        n.GetLocation(0) + scale * listForces[i].GetValueForce(0),
                        n.GetLocation(1) + scale * listForces[i].GetValueForce(1),
                        n.GetLocation(2) + scale * listForces[i].GetValueForce(2));
                line.SetColor(Color.Red);
                line.SetWidth(3);
                viewer.AddObject3D(line);
            }
        }
        public void DrawConstraint(ViewerForm viewer, double scale = 1)
        {
            for (int i = 0; i < listConstraints.Count; i++)
            {
                Constraint c = listConstraints[i];
                Node n = c.GetNode();
                for (int j = 0; j < 3; j++)
                {
                    double dx = 0, dy = 0, dz = 0;
                    if (c.IsFixed(j) == true)
                    {
                        switch (j)
                        {
                            case 0:
                                dx = scale;
                                break;
                            case 1:
                                dy = scale;
                                break;
                            case 2:
                                dz = scale;
                                break;

                        }
                    }
                    Line line = new Line(
                            n.GetLocation(0),
                            n.GetLocation(1),
                            n.GetLocation(2),
                            n.GetLocation(0) - dx,
                            n.GetLocation(1) - dy,
                            n.GetLocation(2) - dz
                        );
                    line.SetColor(Color.Black);
                    line.SetWidth(3);
                    viewer.AddObject3D(line);
                }
            }
        }
        internal void PreProcessing()
        {
            Enumerate();
            int[] bc = ApplyBC();
        }

        private int[] ApplyBC()
        {
            List<int> bc = new List<int>();
            for (int i = 0; i < listConstraints.Count; i++)
            {
                Constraint c = listConstraints[i];
                Node node = c.GetNode();
                for (int j = 0; j < NumberOfFeild; j++)
                {
                    if (c.IsFixed(j))
                    {
                        bc.Add(node.TArray[j]);
                    }
                }
            }
            foreach (var items in bc)
            {
                Console.Write(items + "\t");
            }
            Console.WriteLine();
            return bc.ToArray();
        }

        private void Enumerate()
        {
            int count = 0;
            for (int i = 0; i < listNodes.Count; i++)
            {
                Node node = listNodes[i];
                node.TArray = new int[NumberOfFeild];
                for (int j = 0; j < NumberOfFeild; j++)
                {
                    node.TArray[j] = count++;
                }
            }
            for (int i = 0; i < listElements.Count; i++)
            {
                Truss3DElement element = listElements[i];
                int numberOfNodes = element.CountNodes();
                element.TArray = new int[numberOfNodes * NumberOfFeild];
                int c = 0;
                for (int j = 0; j < numberOfNodes; j++)
                {
                    for (int z = 0; z < NumberOfFeild; z++)
                    {
                        element.TArray[c++] = element.GetNode(j).TArray[z];
                    }
                }
                foreach (var items in element.TArray)
                {
                    Console.Write(items + "\t");
                }
                Console.WriteLine();
            }
        }
        internal void Solve()
        {
            DenseMatrix K = AssemblyStiffnessMatrix();
            DenseVector F = AssemblyFroceVector();
            Console.WriteLine(K.ToString());
            Console.WriteLine(F.ToString());
        }

        private DenseVector AssemblyFroceVector()
        {
            DenseVector F = new DenseVector(listNodes.Count * NumberOfFeild);
            for (int i = 0; i < listForces.Count; i++)
            {
                Force f = listForces[i];
                Node n = f.GetNode();
                DenseVector re = f.ComputeVectorFroce();
                int[] tArrayNode = n.TArray;
                for (int j = 0; j < tArrayNode.Length; j++)
                {
                    F[tArrayNode[j]] += re[j];
                }
            }
            return F;
        }

        private DenseMatrix AssemblyStiffnessMatrix()
        {
            DenseMatrix K = new DenseMatrix(listNodes.Count * NumberOfFeild);
            for (int i = 0; i < listElements.Count; i++)
            {
                Truss3DElement element = listElements[i];
                DenseMatrix Ke = element.ComputeStiffnessMatrix();
                int[] tArrayElement = element.TArray;
                for (int j = 0; j < tArrayElement.Count(); j++)
                {
                    for (int z = 0; z < tArrayElement.Length; z++)
                    {
                        K[tArrayElement[j], tArrayElement[z]] += Ke[j, z];
                    }
                }
            }
            return K;
        }
    }
}
