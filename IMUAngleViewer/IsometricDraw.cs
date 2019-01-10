using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMUAngleViewer
{
    class IsometricDraw
    {
        public struct Point2f
        {
            public float x;
            public float y;
        }
        public struct Point3f
        {
            public float x;
            public float y;
            public float z;
        }
        private Point2f origin,xAxis,yAxis,zAxis;   //画像座標系での原点、軸頂点の座標
        private float imageLength;

        public IsometricDraw(int width,int height,float axisLength)
        {
            if(width > height) { imageLength = height; }
            else{ imageLength = width; }
            origin.x = imageLength / 2;
            origin.y = imageLength / 2;

            xAxis.x = origin.x+ imageLength * axisLength * (float)Math.Cos(30.0 / 180.0 * (float)Math.PI);
            xAxis.y = origin.y- imageLength * axisLength * (float)Math.Sin(30.0 / 180.0 * (float)Math.PI);

            yAxis.x = origin.x+imageLength * axisLength * (float)Math.Cos(150.0 / 180.0 * (float)Math.PI);
            yAxis.y = origin.y-imageLength * axisLength * (float)Math.Sin(150.0 / 180.0 * (float)Math.PI);

            zAxis.x = origin.x;
            zAxis.y = origin.y - imageLength * axisLength;
        }

        public void DrawOriginalAxis(ref Bitmap bitmap)
        {
            Graphics g = Graphics.FromImage(bitmap);
            Pen redPen = new Pen(Color.Red, 5);
            Pen greenPen = new Pen(Color.Green, 5);
            Pen bluePen = new Pen(Color.Blue, 5);

            redPen.EndCap = LineCap.ArrowAnchor;
            greenPen.EndCap = LineCap.ArrowAnchor;
            bluePen.EndCap = LineCap.ArrowAnchor;

            g.DrawLine(redPen, origin.x, origin.y, xAxis.x, xAxis.y);
            g.DrawLine(greenPen, origin.x, origin.y, yAxis.x, yAxis.y);
            g.DrawLine(bluePen, origin.x, origin.y, zAxis.x, zAxis.y);

            redPen.Dispose();
            greenPen.Dispose();
            bluePen.Dispose();
            g.Dispose();
        }

        /** 通常座標から画像座標への変換
         * 
         */ 
        public Point2f ConvertOrdinalAxisToImageAxis(Point2f OrdinalAxis)
        {
            Point2f imageAxis;
            imageAxis.x = origin.x + OrdinalAxis.x;
            imageAxis.y = origin.y - OrdinalAxis.y;
            return imageAxis;
        }

        public static float[] Convert3Dto2D(float[] pos3D)
        {
            float[] pos2D = new float[2];
            pos2D[0] = pos3D[0] *(float)Math.Cos(30.0f / (2.0f * Math.PI)) +
                       pos3D[1] *(float)Math.Cos(150.0f / (2.0f * Math.PI));
            pos2D[1] = pos3D[0] * (float)Math.Sin(30.0f / (2.0f * Math.PI)) +
                       pos3D[1] * (float)Math.Sin(150.0f / (2.0f * Math.PI)) +
                       pos3D[2];
            return pos2D;

        }
        public static Point2f Convert3Dto2D(Point3f pos3D)
        {
            Point2f pos2D;
         
            pos2D.x = pos3D.x * (float)Math.Cos(30.0f / (2.0f * Math.PI)) +
                      pos3D.y * (float)Math.Cos(150.0f / (2.0f * Math.PI));
            pos2D.y = pos3D.x * (float)Math.Sin(30.0f / (2.0f * Math.PI)) +
                       pos3D.y * (float)Math.Sin(150.0f / (2.0f * Math.PI)) +
                       pos3D.z;
            return pos2D;
        }
    }
}
