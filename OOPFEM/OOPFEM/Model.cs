using DEMSoft.Drawing;
using DEMSoft.Drawing.Geometry;
using MathNet.Numerics.LinearAlgebra.Double;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPFEM
{
    class Model
    {
        private List<Node> listNodes;

        private List<AbstractELement> listElements;
        private List<AbstractLoad> listForces;
        private List<Constraint> listConstraints;

        public int NumberOfFeild { get; set; }
        public Model(int numberOfField)
        {
            listNodes = new List<Node>();
            listElements = new List<AbstractELement>();
            listForces = new List<AbstractLoad>();
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
        public AbstractELement GetElement(int Elementindex)
        {
            return listElements[Elementindex];
        }
        public void AddElement(AbstractELement element)
        {
            listElements.Add(element);
        }
        public AbstractLoad GetForce(int indexForce)
        {
            return listForces[indexForce];
        }
        public void AddForce(AbstractLoad force)
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
                if (listElements[i] is Truss3DElement)
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
                else if (listElements[i] is T3Elements)
                {
                    Node n0 = listElements[i].GetNode(0);
                    Node n1 = listElements[i].GetNode(1);
                    Node n2 = listElements[i].GetNode(2);
                    Triangle tri = new Triangle(n0.GetLocation(0), n0.GetLocation(1), n0.GetLocation(2),
                        n1.GetLocation(0), n1.GetLocation(1), n1.GetLocation(2),
                        n2.GetLocation(0), n2.GetLocation(1), n2.GetLocation(2));
                    //tri.SetRandomColor();
                    tri.SetColor(Color.Green);
                    viewer.AddObject3D(tri);
                }
            }
        }
        public void DrawForce(ViewerForm viewer, double scale = 1)
        {
            for (int i = 0; i < listForces.Count; i++)
            {
                if (listForces[i] is Force)
                {
                    Node n = ((Force)listForces[i]).GetNode();
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
                else if (listForces[i] is PressureEdge2D)
                {
                    Node[] nodes = ((PressureEdge2D)listForces[i]).GetNodesOnEdge();
                    double xc = (nodes[0].GetLocation(0) + nodes[1].GetLocation(0)) / 2.0;
                    double yc = (nodes[0].GetLocation(1) + nodes[1].GetLocation(1)) / 2.0;
                    Line line = new Line(
                           xc,
                           yc,
                           0,
                           xc + scale * listForces[i].GetValueForce(0),
                           yc + scale * listForces[i].GetValueForce(1),
                           0);
                    line.SetColor(Color.Red);
                    line.SetWidth(3);
                    viewer.AddObject3D(line);
                }
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
                AbstractELement element = listElements[i];
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
        DenseMatrix KGlobal;
        DenseVector FGlobal;
        DenseVector uGlobal;
        internal void Solve()
        {
            DenseMatrix K = AssemblyStiffnessMatrix();
            KGlobal = (DenseMatrix)K.Clone();
            DenseVector F = AssemblyFroceVector();
            FGlobal = (DenseVector)F.Clone();
            Console.WriteLine(K.ToString());
            Console.WriteLine(F.ToString());
            int[] bc = ApplyBC();
            ApplyBoundaryCondition(bc, ref K, ref F);
            DenseVector u = (DenseVector)K.Solve(F);
            uGlobal = (DenseVector)u.Clone();
            WriteArrayMessagePackFormatter("uglobal.data", uGlobal.ToArray());
            uGlobal = ReadArrayMessagePackFormatter("uglobal.data");
            Console.WriteLine(u.ToString());
            for (int i = 0; i < listNodes.Count; i++)
            {
                Node node = listNodes[i];
                int[] tArrayNode = node.TArray;
                for (int j = 0; j < tArrayNode.Length; j++)
                {
                    node.SetU(j, u[tArrayNode[j]]);
                }
            }
        }

        private void ApplyBoundaryCondition(int[] bc, ref DenseMatrix k, ref DenseVector f)
        {
            for (int i = 0; i < bc.Length; i++)
            {
                int tArrayConstraint = bc[i];
                double u0 = 0;//gia tri ap dat dkb
                for (int j = 0; j < k.RowCount; j++)
                {
                    k[tArrayConstraint, j] = 0;
                    if (j == tArrayConstraint)
                    {
                        f[tArrayConstraint] = u0;
                        k[j, tArrayConstraint] = 1;
                    }
                    else
                    {
                        f[tArrayConstraint] += -k[j, tArrayConstraint] * u0;
                        k[j, tArrayConstraint] = 0;
                    }
                }
            }
            Console.WriteLine(k.ToString());
            Console.WriteLine(f.ToString());
        }

        private DenseVector AssemblyFroceVector()
        {
            DenseVector F = new DenseVector(listNodes.Count * NumberOfFeild);
            for (int i = 0; i < listForces.Count; i++)
            {
                AbstractLoad f = listForces[i];
                if (f is Force)
                {
                    Node n = ((Force)f).GetNode();
                    DenseVector re = f.ComputeVectorFroce();
                    int[] tArrayNode = n.TArray;
                    for (int j = 0; j < tArrayNode.Length; j++)
                    {
                        F[tArrayNode[j]] += re[j];
                    }
                }
                else if (f is PressureEdge2D)
                {
                    PressureEdge2D press = (PressureEdge2D)f;
                    DenseVector re = press.ComputeVectorFroce();
                    int[] tArrayElement = press.GetElements().TArray;
                    for (int j = 0; j < tArrayElement.Length; j++)
                    {
                        F[tArrayElement[j]] += re[j];
                    }
                }
            }
            return F;
        }

        private DenseMatrix AssemblyStiffnessMatrix()
        {
            DenseMatrix K = new DenseMatrix(listNodes.Count * NumberOfFeild);
            for (int i = 0; i < listElements.Count; i++)
            {
                AbstractELement element = listElements[i];
                DenseMatrix Ke = element.ComputeStiffnessMatrix();
                Console.WriteLine(Ke.ToString());
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

        public void DrawDeformation(ViewerForm viewer, double scale = 1)
        {
            int countElement = listElements.Count;
            for (int i = 0; i < countElement; i++)
            {
                if (listElements[i] is Truss3DElement)
                {
                    Node n0 = listElements[i].GetNode(0);
                    Node n1 = listElements[i].GetNode(1);
                    Line line = new Line(
                            n0.GetLocation(0) + scale * n0.GetU(0),
                            n0.GetLocation(1) + scale * n0.GetU(1),
                            n0.GetLocation(2) + scale * n0.GetU(2),
                            n1.GetLocation(0) + scale * n1.GetU(0),
                            n1.GetLocation(1) + scale * n1.GetU(1),
                            n1.GetLocation(2) + scale * n1.GetU(2)
                        );
                    line.SetColor(Color.Orange);
                    line.SetDashedLine(true);
                    line.SetWidth(3);
                    viewer.AddObject3D(line);
                }
                else if (listElements[i] is T3Elements)
                {
                    Node n0 = listElements[i].GetNode(0);
                    Node n1 = listElements[i].GetNode(1);
                    Node n2 = listElements[i].GetNode(2);
                    Triangle tri = new Triangle(n0.GetLocation(0) + scale * n0.GetU(0),
                        n0.GetLocation(1) + scale * n0.GetU(1),
                        0,
                        n1.GetLocation(0) + scale * n1.GetU(0),
                        n1.GetLocation(1) + scale * n1.GetU(1),
                        0,
                        n2.GetLocation(0) + scale * n2.GetU(0),
                        n2.GetLocation(1) + scale * n2.GetU(1),
                        0);
                    //tri.SetRandomColor();
                    tri.SetColor(Color.Yellow);
                    tri.SetOpacity(0.5);
                    viewer.AddObject3D(tri);
                }
            }
        }
        internal void DrawReactionFroces(ViewerForm viewer, double scale = 1)
        {
            DenseVector ReForce = KGlobal * uGlobal - FGlobal;
            Console.WriteLine(ReForce.ToString());
            for (int i = 0; i < listConstraints.Count; i++)
            {
                Constraint c = listConstraints[i];
                Node n = c.GetNode();
                int[] tArrayNode = n.TArray;
                for (int j = 0; j < tArrayNode.Length; j++)
                {
                    if (c.IsFixed(j))
                    {
                        double rfx = 0, rfy = 0, rfz = 0;
                        switch (j)
                        {
                            case 0:
                                rfx = ReForce[tArrayNode[j]];
                                break;
                            case 1:
                                rfy = ReForce[tArrayNode[j]];
                                break;
                            case 2:
                                rfz = ReForce[tArrayNode[j]];
                                break;
                        }
                        Line line = new Line(
                                    n.GetLocation(0),
                                    n.GetLocation(1),
                                    n.GetLocation(2),
                                    n.GetLocation(0) + scale * rfx,
                                    n.GetLocation(1) + scale * rfy,
                                    n.GetLocation(2) + scale * rfz);
                        line.SetColor(Color.Blue);
                        line.SetWidth(5);
                        viewer.AddObject3D(line);

                    }
                }
            }
        }

        public static void WriteArrayMessagePackFormatter(string linkFile, double[,] matrix)
        {
            string directory = Path.GetDirectoryName(linkFile);

            if (File.Exists(linkFile))
                File.Delete(linkFile);
            // Presist to file
            FileStream stream = File.Create(linkFile);

            MessagePackSerializer.Serialize(stream, matrix);

            stream.Close();
        }
        public static void WriteArrayMessagePackFormatter(string linkFile, double[] vector)
        {
            if (File.Exists(linkFile))
                File.Delete(linkFile);
            // Presist to file
            FileStream stream = File.Create(linkFile);
            MessagePackSerializer.Serialize(stream, vector);
            stream.Close();
        }
        public static double[,] ReadMatrixMessagePackFormatter(string filename)
        {
            // Presist to file
            FileStream stream = File.OpenRead(filename);
            double[,] v = MessagePackSerializer.Deserialize<double[,]>(stream);
            stream.Close();
            return v;
        }
        public static double[] ReadArrayMessagePackFormatter(string filename)
        {
            // Presist to file
            FileStream stream = File.OpenRead(filename);
            double[] v = MessagePackSerializer.Deserialize<double[]>(stream);
            stream.Close();
            return v;
        }
    }
}
