using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Dynamics.Joints;
using SFML.Graphics;
using FarseerPhysics;

using Transform = FarseerPhysics.Common.Transform;
using CircleShape = FarseerPhysics.Collision.Shapes.CircleShape;
using Microsoft.Xna.Framework;
using SpaceShooter;


// SOURCE: http://pastebin.com/L8jsKq9n
// UNKNOWN AUTHOR


namespace JumpAndRun
{

    public class DebugDraw : DebugViewBase
    {
        private struct ContactPoint
        {
            public Vector2 Normal;
            public Vector2 Position;
            public PointState State;
        }

        

        public Color DefaultShapeColor = Color.Cyan;
        public Color InactiveShapeColor = Color.Yellow;
        public Color KinematicShapeColor = Color.Magenta;
        public Color SleepingShapeColor = new Color(50, 50, 50);
        public Color StaticShapeColor = Color.Blue;

        private RenderStates m_renderStates;

        private const int CircleSegments = 32;
        private VertexArray m_TrianglesArray = new VertexArray(PrimitiveType.Triangles);
        private VertexArray m_LinesArray = new VertexArray(PrimitiveType.Lines);

        private Vector2[] _tempVertices = new Vector2[Settings.MaxPolygonVertices];

        private int _pointCount;
        private const int MaxContactPoints = 2048;
        private ContactPoint[] _points = new ContactPoint[MaxContactPoints];

        private bool m_visible = true;
        private RenderWindow m_Window;

        public DebugDraw(World world, RenderWindow window)
            : base(world)
        {
            this.m_Window = window;

            AppendFlags(DebugViewFlags.AABB |
                DebugViewFlags.CenterOfMass |
                DebugViewFlags.ContactNormals |
                DebugViewFlags.Controllers |
                DebugViewFlags.DebugPanel | DebugViewFlags.Joint | DebugViewFlags.PerformanceGraph | DebugViewFlags.PolygonPoints | DebugViewFlags.Shape);


        }


        public override void DrawCircle(Vector2 center, float radius, float red, float blue, float green)
        {
            DrawCircle(center, radius, new Color((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255)));
        }

        public override void DrawPolygon(Vector2[] vertices, int count, float red, float blue, float green, bool closed = true)
        {
            DrawPolygon(vertices, count, new Color((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255)), closed);
        }

        public override void DrawSegment(Vector2 start, Vector2 end, float red, float blue, float green)
        {
            DrawSegment(start, end, new Color((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255)));
        }

        public override void DrawSolidCircle(Vector2 center, float radius, Vector2 axis, float red, float blue, float green)
        {
            DrawSolidCircle(center, radius, axis, new Color((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255)));
        }

        public override void DrawSolidPolygon(Vector2[] vertices, int count, float red, float blue, float green)
        {
            DrawSolidPolygon(vertices, count, new Color((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255)));
        }

        public override void DrawTransform(ref FarseerPhysics.Common.Transform transform)
        {
            /*const float axisScale = 0.4f;
            Vector2 p1 = transform.p;

            Vector2 p2 = p1 + axisScale * transform.q.GetXAxis();
            DrawSegment(p1, p2, Color.Red);

            p2 = p1 + axisScale * transform.q.GetYAxis();
            DrawSegment(p1, p2, Color.Green);*/
        }


        public void DrawSolidPolygon(Vector2[] vertices, int count, Color color, bool outline = true)
        {
            if (count == 2)
            {
                DrawPolygon(vertices, count, color);
                return;
            }

            Color colorFill = new Color();
            if (outline)
            {
                byte r = (byte)(color.R / 2);
                byte g = (byte)(color.G / 2);
                byte b = (byte)(color.B / 2);
                colorFill = new Color(r, g, b);
            }
            else
                colorFill = color;

            for (int i = 1; i < count - 1; i++)
            {
                m_TrianglesArray.Append(new Vertex(vertices[0].ToSf(), colorFill));
                m_TrianglesArray.Append(new Vertex(vertices[i].ToSf(), colorFill));
                m_TrianglesArray.Append(new Vertex(vertices[i + 1].ToSf(), colorFill));
            }

            if (outline)
                DrawPolygon(vertices, count, color);
        }

        public void DrawPolygon(Vector2[] vertices, int count, Color color, bool closed = true)
        {
            for (int i = 0; i < count - 1; i++)
            {
                m_LinesArray.Append(new Vertex(vertices[i].ToSf(), color));
                m_LinesArray.Append(new Vertex(vertices[i + 1].ToSf(), color));
            }
            if (closed)
            {
                m_LinesArray.Append(new Vertex(vertices[count - 1].ToSf(), color));
                m_LinesArray.Append(new Vertex(vertices[0].ToSf(), color));
            }

        }

        public void DrawSolidCircle(Vector2 center, float radius, Vector2 axis, Color color)
        {
            const double increment = Math.PI * 2.0 / CircleSegments;
            double theta = 0.0;

            Color colorFill = new Color((byte)(color.R / 2), (byte)(color.G / 2), (byte)(color.B / 2));

            Vector2 v0 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            theta += increment;

            for (int i = 1; i < CircleSegments - 1; i++)
            {
                Vector2 v1 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                Vector2 v2 = center + radius * new Vector2((float)Math.Cos(theta + increment), (float)Math.Sin(theta + increment));

                m_TrianglesArray.Append(new Vertex(v0.ToSf(), colorFill));
                m_TrianglesArray.Append(new Vertex(v1.ToSf(), colorFill));
                m_TrianglesArray.Append(new Vertex(v2.ToSf(), colorFill));
                theta += increment;
            }

            DrawCircle(center, radius, color);
            DrawSegment(center, center + axis * radius, color);
        }

        public void DrawCircle(Vector2 center, float radius, Color color)
        {
            const double increment = Math.PI * 2.0 / CircleSegments;
            double theta = 0.0;

            for (int i = 0; i < CircleSegments; i++)
            {
                Vector2 v1 = center + radius * new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
                Vector2 v2 = center + radius * new Vector2((float)Math.Cos(theta + increment), (float)Math.Sin(theta + increment));

                m_LinesArray.Append(new Vertex(v1.ToSf(), color));
                m_LinesArray.Append(new Vertex(v2.ToSf(), color));
                theta += increment;
            }
        }

        private void DrawSegment(Vector2 start, Vector2 end, Color color)
        {
            m_LinesArray.Append(new Vertex(start.ToSf(), color));
            m_LinesArray.Append(new Vertex(end.ToSf(), color));
        }

        public void DrawAABB(ref AABB aabb, Color color)
        {
            Vector2[] verts = new Vector2[4];
            verts[0] = new Vector2(aabb.LowerBound.X, aabb.LowerBound.Y);
            verts[1] = new Vector2(aabb.UpperBound.X, aabb.LowerBound.Y);
            verts[2] = new Vector2(aabb.UpperBound.X, aabb.UpperBound.Y);
            verts[3] = new Vector2(aabb.LowerBound.X, aabb.UpperBound.Y);

            DrawPolygon(verts, 4, color.R, color.G, color.B);
        }

        public void DrawPoint(Vector2 p, float size, Color color)
        {
            Vector2[] verts = new Vector2[4];
            float hs = size / 2.0f;
            verts[0] = p + new Vector2(-hs, -hs);
            verts[1] = p + new Vector2(hs, -hs);
            verts[2] = p + new Vector2(hs, hs);
            verts[3] = p + new Vector2(-hs, hs);

            DrawSolidPolygon(verts, 4, color, true);
        }

        private void DrawJoint(Joint joint)
        {
            if (!joint.Enabled)
                return;

            Body b1 = joint.BodyA;
            Body b2 = joint.BodyB;
            Transform xf1;
            b1.GetTransform(out xf1);

            Vector2 x2 = Vector2.Zero;

            // WIP David
            if (!joint.IsFixedType())
            {
                Transform xf2;
                b2.GetTransform(out xf2);
                x2 = xf2.p;
            }

            Vector2 p1 = joint.WorldAnchorA;
            Vector2 p2 = joint.WorldAnchorB;
            Vector2 x1 = xf1.p;

            Color color = new Color(127, 200, 200);

            switch (joint.JointType)
            {
                case JointType.Distance:
                    DrawSegment(p1, p2, color);
                    break;
                case JointType.Pulley:
                    PulleyJoint pulley = (PulleyJoint)joint;
                    Vector2 s1 = b1.GetWorldPoint(pulley.LocalAnchorA);
                    Vector2 s2 = b2.GetWorldPoint(pulley.LocalAnchorB);
                    DrawSegment(p1, p2, color);
                    DrawSegment(p1, s1, color);
                    DrawSegment(p2, s2, color);
                    break;
                case JointType.FixedMouse:
                    DrawPoint(p1, 0.5f, new Color(0, 255, 0));
                    DrawSegment(p1, p2, new Color(200, 200, 200));
                    break;
                case JointType.Revolute:
                    DrawSegment(x1, p1, color);
                    DrawSegment(p1, p2, color);
                    DrawSegment(x2, p2, color);

                    DrawSolidCircle(p2, 0.1f, Vector2.Zero, Color.Red);
                    DrawSolidCircle(p1, 0.1f, Vector2.Zero, Color.Blue);
                    break;
                case JointType.FixedAngle:
                    //Should not draw anything.
                    break;
                case JointType.FixedRevolute:
                    DrawSegment(x1, p1, color);
                    DrawSolidCircle(p1, 0.1f, Vector2.Zero, Color.Magenta);
                    break;
                case JointType.FixedLine:
                    DrawSegment(x1, p1, color);
                    DrawSegment(p1, p2, color);
                    break;
                case JointType.FixedDistance:
                    DrawSegment(x1, p1, color);
                    DrawSegment(p1, p2, color);
                    break;
                case JointType.FixedPrismatic:
                    DrawSegment(x1, p1, color);
                    DrawSegment(p1, p2, color);
                    break;
                case JointType.Gear:
                    DrawSegment(x1, x2, color);
                    break;
                default:
                    DrawSegment(x1, p1, color);
                    DrawSegment(p1, p2, color);
                    DrawSegment(x2, p2, color);
                    break;
            }
        }

        public void DrawShape(Fixture fixture, Transform xf, Color color)
        {
            switch (fixture.Shape.ShapeType)
            {
                case ShapeType.Circle:
                    {
                        CircleShape circle = (CircleShape)fixture.Shape;

                        Vector2 center = MathUtils.Mul(ref xf, circle.Position);
                        float radius = circle.Radius;
                        Vector2 axis = MathUtils.Mul(xf.q, new Vector2(1.0f, 0.0f));

                        DrawSolidCircle(center, radius, axis, color);
                    }
                    break;

                case ShapeType.Polygon:
                    {
                        PolygonShape poly = (PolygonShape)fixture.Shape;
                        int vertexCount = poly.Vertices.Count;
                        Debug.Assert(vertexCount <= Settings.MaxPolygonVertices);

                        for (int i = 0; i < vertexCount; ++i)
                        {
                            _tempVertices[i] = MathUtils.Mul(ref xf, poly.Vertices[i]);
                        }

                        DrawSolidPolygon(_tempVertices, vertexCount, color);
                    }
                    break;


                case ShapeType.Edge:
                    {
                        EdgeShape edge = (EdgeShape)fixture.Shape;
                        Vector2 v1 = MathUtils.Mul(ref xf, edge.Vertex1);
                        Vector2 v2 = MathUtils.Mul(ref xf, edge.Vertex2);
                        DrawSegment(v1, v2, color);
                    }
                    break;

                case ShapeType.Chain:
                    {
                        ChainShape chain = (ChainShape)fixture.Shape;

                        for (int i = 0; i < chain.Vertices.Count - 1; ++i)
                        {
                            Vector2 v1 = MathUtils.Mul(ref xf, chain.Vertices[i]);
                            Vector2 v2 = MathUtils.Mul(ref xf, chain.Vertices[i + 1]);
                            DrawSegment(v1, v2, color);
                        }
                    }
                    break;
            }
        }

        internal void DrawDebugData()
        {
        

            if ((Flags & DebugViewFlags.Shape) == DebugViewFlags.Shape)
            {
                foreach (Body b in World.BodyList)
                {
                    Transform xf;
                    b.GetTransform(out xf);
                    foreach (Fixture f in b.FixtureList)
                    {
                        if (b.Enabled == false)
                            DrawShape(f, xf, InactiveShapeColor);
                        else if (b.BodyType == BodyType.Static)
                            DrawShape(f, xf, StaticShapeColor);
                        else if (b.BodyType == BodyType.Kinematic)
                            DrawShape(f, xf, KinematicShapeColor);
                        else if (b.Awake == false)
                            DrawShape(f, xf, SleepingShapeColor);
                        else
                            DrawShape(f, xf, DefaultShapeColor);
                    }
                }
            }

            if ((Flags & DebugViewFlags.ContactPoints) == DebugViewFlags.ContactPoints)
            {
                const float axisScale = 0.3f;

                for (int i = 0; i < _pointCount; ++i)
                {
                    ContactPoint point = _points[i];

                    if (point.State == PointState.Add)
                        DrawPoint(point.Position, 0.1f, new Color(85, 240, 85));
                    else if (point.State == PointState.Persist)
                        DrawPoint(point.Position, 0.1f, new Color(85, 85, 240));

                    if ((Flags & DebugViewFlags.ContactNormals) == DebugViewFlags.ContactNormals)
                    {
                        Vector2 p1 = point.Position;
                        Vector2 p2 = p1 + axisScale * point.Normal;
                        DrawSegment(p1, p2, new Color(100, 230, 100));
                    }
                }

                _pointCount = 0;
            }

            if ((Flags & DebugViewFlags.PolygonPoints) == DebugViewFlags.PolygonPoints)
            {
                foreach (Body body in World.BodyList)
                {
                    foreach (Fixture f in body.FixtureList)
                    {
                        PolygonShape polygon = f.Shape as PolygonShape;
                        if (polygon != null)
                        {
                            Transform xf;
                            body.GetTransform(out xf);

                            for (int i = 0; i < polygon.Vertices.Count; i++)
                            {
                                Vector2 tmp = MathUtils.Mul(ref xf, polygon.Vertices[i]);
                                DrawPoint(tmp, 0.1f, Color.Red);
                            }
                        }
                    }
                }
            }

            if ((Flags & DebugViewFlags.Joint) == DebugViewFlags.Joint)
            {
                foreach (Joint j in World.JointList)
                {
                    DrawJoint(j);
                }
            }

            if ((Flags & DebugViewFlags.AABB) == DebugViewFlags.AABB)
            {
                Color color = new Color(230, 85, 230);
                IBroadPhase bp = World.ContactManager.BroadPhase;

                foreach (Body body in World.BodyList)
                {
                    if (body.Enabled == false)
                        continue;

                    foreach (Fixture f in body.FixtureList)
                    {
                        for (int t = 0; t < f.ProxyCount; ++t)
                        {
                            FixtureProxy proxy = f.Proxies[t];
                            AABB aabb;
                            bp.GetFatAABB(proxy.ProxyId, out aabb);

                            DrawAABB(ref aabb, color);
                        }
                    }
                }
            }

            if ((Flags & DebugViewFlags.CenterOfMass) == DebugViewFlags.CenterOfMass)
            {
                foreach (Body b in World.BodyList)
                {
                    Transform xf;
                    b.GetTransform(out xf);
                    xf.p = b.WorldCenter;
                    DrawTransform(ref xf);
                }
            }

            if ((Flags & DebugViewFlags.Controllers) == DebugViewFlags.Controllers)
            {
                for (int i = 0; i < World.ControllerList.Count; i++)
                {
                    Controller controller = World.ControllerList[i];

                    BuoyancyController buoyancy = controller as BuoyancyController;
                    if (buoyancy != null)
                    {
                        AABB container = buoyancy.Container;
                        DrawAABB(ref container, Color.Cyan);
                    }
                }
            }

            m_Window.Draw(m_TrianglesArray);
            m_Window.Draw(m_LinesArray);
            m_TrianglesArray.Clear();
            m_LinesArray.Clear();
        }
    }
}