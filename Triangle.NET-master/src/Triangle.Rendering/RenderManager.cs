﻿
namespace TriangleNet.Rendering
{
    using System.Collections.Generic;
    using TriangleNet.Geometry;
    using TriangleNet.Meshing;
    using TriangleNet.Rendering.Util;

    public class RenderManager
    {
        // TODO: delete
        public static bool VORONOI_DEBUG = false;

        IRenderControl control;
        IRenderContext context;
        IRenderer renderer;
        Projection zoom;

        public IRenderControl Control => control; 

        public IRenderContext Context => context;

        public RenderManager()
        {
        }

        public RenderManager(IRenderControl control, IRenderer renderer)
        {
            Initialize(control, renderer);
        }

        public void Initialize(IRenderControl control, IRenderer renderer)
        {
            this.zoom = new Projection(control.ClientRectangle);

            this.context = new RenderContext(zoom, ColorManager.Default());

            this.renderer = renderer;
            this.renderer.Context = context;

            this.control = control;
            this.control.Initialize();
            this.control.Renderer = renderer;
        }

        public bool TryCreateControl(string assemblyName, IEnumerable<string> dependencies,
            out IRenderControl control)
        {
            return ReflectionHelper.TryCreateControl(assemblyName, dependencies, out control);
        }

        public void Resize()
        {
            control.HandleResize();
        }

        public void Clear()
        {
            context.Clear();
            control.Refresh();
        }

        public void Enable(int layer, bool enabled)
        {
            context.Enable(layer, enabled);

            control.Refresh();
        }

        public void Set(IPolygon data, bool refresh = true)
        {
            context.Add(data);

            if (refresh)
            {
                control.Refresh();
            }
        }

        public void Set(IMesh data, bool reset, bool refresh = true)
        {
            context.Add(data, reset);

            if (refresh)
            {
                control.Refresh();
            }
        }

        /// <summary>
        /// Set data for Voronoi layer.
        /// </summary>
        public void Set(ICollection<Point> points, IEnumerable<IEdge> edges, bool reset, bool refresh = true)
        {
            context.Add(points, edges, reset);

            if (refresh)
            {
                control.Refresh();
            }
        }

        public void Update(float[] values)
        {
            context.Add(values);
            control.Refresh();
        }

        public void Update(int[] partition)
        {
            context.Add(partition);
            control.Refresh();
        }
    }
}
