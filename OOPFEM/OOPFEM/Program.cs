using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TriangleNet;
using TriangleNet.Geometry;
using TriangleNet.Meshing.Algorithm;
using TriangleNet.Rendering.Text;
using DEMSoft.Drawing;
using System.Drawing;
using DEMSoft.Drawing.Geometry;

namespace OOPFEM
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //double x0 = 0, y0 = 0, x1 = 100, y1 = 100;
            //int numEdge0 = 5, numEdge1 = 8;
            //List<Vertex> vertices = CreatVertaxList(x0, y0, x1, y1, numEdge0, numEdge1);
            //// Generate points.
            //var points = Generate.RandomPoints(20, new TriangleNet.Geometry.Rectangle(0, 0, 100, 30));
            //points.AddRange(vertices);

            //// Choose triangulator: Incremental, SweepLine or Dwyer.
            //var triangulator = new Dwyer();

            //// Generate mesh.
            //var mesh = triangulator.Triangulate(points, new Configuration());
            //SvgImage.Save(mesh, "example-1.svg", 500);


            //int numPoints = mesh.Vertices.Count;
            //double[] x = new double[numPoints];
            //double[] y = new double[numPoints];
            //double[] z = new double[numPoints];

            //for (int i = 0; i < numPoints; i++)
            //{
            //    x[i] = mesh.Vertices.ElementAt(i).X;
            //    y[i] = mesh.Vertices.ElementAt(i).Y;

            //}

            //PointSet ps = new PointSet(x, y, z);
            //ps.SetColor(Color.Green);
            //ps.SetPointSize(5);

            //int numberOfElement = mesh.Triangles.Count;
            //Triangle[] triangules = new Triangle[numberOfElement];
            //for (int i = 0; i < numberOfElement; i++)
            //{
            //    Vertex vertex0 = mesh.Triangles.ElementAt(i).GetVertex(0);
            //    Vertex vertex1 = mesh.Triangles.ElementAt(i).GetVertex(1);
            //    Vertex vertex2 = mesh.Triangles.ElementAt(i).GetVertex(2);
            //    triangules[i] = new Triangle(vertex0.X, vertex0.Y, 0, vertex1.X, vertex1.Y, 0, vertex2.X, vertex2.Y, 0);
            //}

            //int numberOfEdges = mesh.Edges.Count();
            //Line[] lines = new Line[numberOfEdges];
            //for (int i = 0; i < numberOfEdges; i++)
            //{
            //    int idPoints0 = mesh.Edges.ElementAt(i).P0;
            //    int idPoints1 = mesh.Edges.ElementAt(i).P1;
            //    Vertex vertex0 = mesh.Vertices.ElementAt(idPoints0);
            //    Vertex vertex1 = mesh.Vertices.ElementAt(idPoints1);
            //    lines[i] = new Line(vertex0.X, vertex0.Y, 0, vertex1.X, vertex1.Y, 0);
            //}

            //ViewerForm viewer = new ViewerForm(true);
            //viewer.AddObject3D(ps);
            //for (int i = 0; i < numberOfElement; i++)
            //{
            //    triangules[i].SetColor(Color.Orange);
            //    viewer.AddObject3D(triangules[i]);
            //}
            //for (int i = 0; i < numberOfEdges; i++)
            //{
            //    lines[i].SetColor(Color.Blue);
            //    viewer.AddObject3D(lines[i]);
            //}

            //viewer.UpdateCamera();

            //viewer.Run();
            //double E = 1.2e6;
            //double A1 = 0.302;
            //double A2 = 0.729;
            //double A3 = 0.187;
            //double f = 1000;

            //List<Node> nodes = new List<Node>();
            //List<Truss3DElement> elements = new List<Truss3DElement>();
            //nodes.Add(new Node(72, 0, 0));
            //nodes.Add(new Node(0, 36, 0));
            //nodes.Add(new Node(0, 36, 72));
            //nodes.Add(new Node(0, 0, -48));
            //Truss3DElement elem1 = new Truss3DElement(E, A1, nodes[0], nodes[1]);
            //Truss3DElement elem2 = new Truss3DElement(E, A2, nodes[0], nodes[2]);
            //Truss3DElement elem3 = new Truss3DElement(E, A3, nodes[0], nodes[3]);
            //Constraint c1 = new Constraint(nodes[1], true, true, true);
            //Constraint c2 = new Constraint(nodes[2], true, true, true);
            //Constraint c3 = new Constraint(nodes[3], true, true, true);
            //Force force = new Force(nodes[0], 0, 0, -f);
            //Model model = new Model(3);
            //for (int i = 0; i < nodes.Count; i++)
            //{
            //    model.AddNode(nodes[i]);
            //}
            //model.AddElement(elem1);
            //model.AddElement(elem2);
            //model.AddElement(elem3);
            //model.AddForce(force);
            //model.AddConstrain(c1);
            //model.AddConstrain(c2);
            //model.AddConstrain(c3);

            //model.PreProcessing();
            //model.Solve();
            ////model.PostProcessing();

            //ViewerForm viewer = new ViewerForm(true);
            //model.DrawNode(viewer);
            //model.DrawElement(viewer);
            //model.DrawConstraint(viewer, 10);
            //double scale = 1e-2;
            //model.DrawForce(viewer, scale);
            //model.DrawDeformation(viewer, 10);
            //model.DrawReactionFroces(viewer, 0.1);
            //viewer.UpdateCamera();
            //viewer.Run();

            //double E = 10e9;
            //double nu = 0.25;
            //Node n1 = new Node(0, 0);
            //Node n2 = new Node(0, 2);
            //Node n3 = new Node(2, 0);
            //Node n4 = new Node(2, 2);

            //Model model = new Model(2);
            //model.AddNode(n1);
            //model.AddNode(n2);
            //model.AddNode(n3);
            //model.AddNode(n4);

            //Constraint c1 = new Constraint(n1, true, true, false);
            //Constraint c2 = new Constraint(n3, true, true, false);

            //ViewerForm viewer = new ViewerForm(true);

            //T3Elements element = new T3Elements(E, nu, n1, n3, n2);
            //T3Elements element1 = new T3Elements(E, nu, n4, n2, n3);

            //PressureEdge2D pressure = new PressureEdge2D(element1, 0, 1e3, 0);
            //model.AddElement(element);
            //model.AddElement(element1);
            //model.AddConstrain(c1);
            //model.AddConstrain(c2);
            //model.AddForce(pressure);

            double E = 10e6;
            double nu = 0.3;

            Node n1 = new Node(0, 0);
            Node n2 = new Node(0, 1);
            Node n3 = new Node(1, 0);
            Node n4 = new Node(1, 1);
            Node n5 = new Node(2, 0);
            Node n6 = new Node(2, 1);
            Node n7 = new Node(3, 0);
            Node n8 = new Node(3, 1);
            Node n9 = new Node(4, 0);
            Node n10 = new Node(4, 1);

            Model model = new Model(2);
            model.AddNode(n1);
            model.AddNode(n2);
            model.AddNode(n3);
            model.AddNode(n4);
            model.AddNode(n5);
            model.AddNode(n6);
            model.AddNode(n7);
            model.AddNode(n8);
            model.AddNode(n9);
            model.AddNode(n10);

            T3Elements t1 = new T3Elements(E, nu, n1, n3, n4);
            T3Elements t2 = new T3Elements(E, nu, n4, n2, n1);
            T3Elements t3 = new T3Elements(E, nu, n3, n5, n6);
            T3Elements t4 = new T3Elements(E, nu, n6, n4, n3);
            T3Elements t5 = new T3Elements(E, nu, n5, n7, n8);
            T3Elements t6 = new T3Elements(E, nu, n8, n6, n5);
            T3Elements t7 = new T3Elements(E, nu, n7, n9, n10);
            T3Elements t8 = new T3Elements(E, nu, n10, n8, n7);

            model.AddElement(t1);
            model.AddElement(t2);
            model.AddElement(t3);
            model.AddElement(t4);
            model.AddElement(t5);
            model.AddElement(t6);
            model.AddElement(t7);
            model.AddElement(t8);

            Constraint c2 = new Constraint(n2, true, false, false);
            Constraint c1 = new Constraint(n1, true, true, false);

            PressureEdge2D pressure = new PressureEdge2D(t7, 1, 10e3, 0);

            model.AddConstrain(c1);
            model.AddConstrain(c2);
            model.AddForce(pressure);
            ViewerForm viewer = new ViewerForm(true);

            model.DrawNode(viewer);
            model.DrawElement(viewer);
            model.DrawConstraint(viewer, 1e-1);
            model.DrawForce(viewer, 5e-5);
            model.PreProcessing();
            model.Solve();
            model.DrawDeformation(viewer, 1e2);
            viewer.UpdateCamera();
            viewer.Run();
        }

        static List<Vertex> CreatVertaxList(double x0, double y0, double x1, double y1, int numEdge0, int numEdge1)
        {
            List<Vertex> vertices = new List<Vertex>();

            double dx = (x1 - x0) / numEdge0;
            double dy = (y1 - y0) / numEdge1;
            for (int i = 0; i < numEdge0 + 1; i++)
            {
                vertices.Add(new Vertex(i * dx, y0));
                vertices.Add(new Vertex(i * dx, y1));
            }
            for (int i = 0; i < numEdge1; i++)
            {
                vertices.Add(new Vertex(i * dy, x0));
                vertices.Add(new Vertex(i * dy, x1));
            }
            return vertices;
        }
    }
}
