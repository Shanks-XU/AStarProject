using System;
namespace A星算法1
{
    public class Point
    {
        public int x;
        public int y;
        public Point father;

        public int G;
        public int H;

        public int F
        {
            get
            {
                return G + H;
            }
        }

        public Point()
        {
        }

        public Point(int x0, int y0, Point _father = null)
        {
            this.x = x0;
            this.y = y0;
            this.father = _father;
        }


        /// <summary>
        /// Gets the GV alue.
        /// 设置起始点到该点的寻路代价
        /// </summary>
        /// <returns>The GV alue.</returns>
        public void GenerateGValue()
        {
            if (this.father == null)
            {
                this.G = 0;
            }

            int price = 0;
            if (this.x == this.father.x || this.y == this.father.y) // 横向和纵向
            {
                price = 10;
            }
            else
            {
                price = 14;
            }

            this.G = this.father.G + price;

        }

        public int GetGValueRelative(Point father)
        {
            int price = 0;
            if ((this.x == father.x) || (this.y == father.y))

            {
                price = 10;
            }
            else
            {
                price = 14;
            }

            return father.G + price;
        }

        /// <summary>
        /// Gets the HV alue.
        /// 估算该点到目标点的代价
        /// 曼哈顿街道算法
        /// </summary>
        /// <returns>The HV alue.</returns>
        /// <param name="end">End.</param>
        public void GenerateHValue(Point end)
        {
            this.H = Math.Abs(this.x - end.x) + Math.Abs(this.y - end.y);
        }


        /// <summary>
        /// Ises the end point.
        /// 是否是目标点
        /// </summary>
        /// <returns><c>true</c>, if end point was ised, <c>false</c> otherwise.</returns>
        /// <param name="end">End.</param>
        public bool IsEndPoint(Point end)
        {
            if (this.x == end.x && this.y == end.y)
            {
                return true;
            }

            return false;
        }
    }
}
