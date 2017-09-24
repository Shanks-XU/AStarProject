using System;
using System.Collections.Generic;

namespace A星算法1
{
    public class Map
    {
        private byte[,] map;

        public Map(byte[,] _map)
        {
            this.map = _map;
        }





        // Open List：记录所有被考虑来寻找最短路径的点
        private List<Point> open_List = new List<Point>();
        // Close List：记录下不会再被考虑的方块
        private List<Point> close_List = new List<Point>();

        /// <summary>
        /// Ises the obstacle.
        /// 是否是障碍点
        /// </summary>
        /// <returns><c>true</c>, if obstacle was ised, <c>false</c> otherwise.</returns>
        /// <param name="p">P.</param>
        //private bool IsObstacle(Point p)
        //{
        //    if (this.map[p.x, p.y] == 0)
        //    {
        //        return true;
        //    }

        //    return false;
        //}


        private bool IsObstacle(int x, int y)
        {
            if (this.map[x, y] == 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Ises the in open list.
        /// 判断点是否在开放列表中
        /// </summary>
        /// <returns><c>true</c>, if in open list was ised, <c>false</c> otherwise.</returns>
        /// <param name="point">Point.</param>
        //private bool IsInOpenList(Point point)
        //{
        //    if (this.open_List.Contains((point)))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        private bool IsInOpenList(int x, int y)
        {
            foreach (var point in this.open_List)
            {
                if (point.x == x && point.y == y)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ises the in close list.
        /// 判断点是否在关闭列表中
        /// </summary>
        /// <returns><c>true</c>, if in close list was ised, <c>false</c> otherwise.</returns>
        /// <param name="point">Point.</param>
        //private bool IsInCloseList(Point point)
        //{
        //    if (this.close_List.Contains(point))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private bool IsInCloseList(int x, int y)
        {
            foreach (var point in this.close_List)
            {
                if (point.x == x && point.y == y)
                {
                    return true;
                }
            }
                return false;
        }


        private Point GetPointFromOpenList(int x, int y)
        {
            foreach (var point in this.open_List)
            {
                if (point.x == x && point.y == y)
                {
                    return point;
                }
            }

            return null;
        }




        /// <summary>
        /// Gets the minimum FV alue in open list.
        /// 在开放列表中找到F值（F=G + H）最小的点
        /// 查找优化
        /// </summary>
        private Point GetMinFValueInOpenList()
        {
            Point result = null;
            foreach (var point in this.open_List)
            {
                if (result == null || point.F < result.F)
                {
                    result = point;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the minimum FV alue in open list2.
        /// 查找优化
        /// </summary>
        /// <returns>The minimum FV alue in open list2.</returns>
		private Point GetMinFValueInOpenList2()
		{
            int fMinValue = int.MaxValue;
            Point result = null;

            foreach (var point in this.open_List)
            {
                if (point.F < fMinValue)
                {
                    result = point;
                    fMinValue = point.F;
                }
            }

            return result;
        }


        private void CheckPoint8(Point p0, Point end)
        {
            // 8点
            for (int x = p0.x - 1; x <= p0.x + 1; x++)
            {
                for (int y= p0.y - 1; y <= p0.y + 1; y++)
                {
                    // 排除边界点，和自身点
                    if ((x >= 0 && x < 10) && (y >= 0 && y < 10) && !(x == p0.x && y == p0.y))
                    {
                        // 排除障碍点，关闭列表中的点
                        if (!this.IsObstacle(x, y) && !this.IsInCloseList(x, y))
                        {
                            if (this.IsInOpenList(x, y))
                            {
                                // 在开放列表中
                                Point p = this.GetPointFromOpenList(x, y);

                                // 此时父节点变了,现在变为p0
                                int G_new = p.GetGValueRelative(p0);
                                if (G_new < p.G)
                                {
                                    p.father = p0;
                                    p.GenerateGValue();

                                    //this.open_List.Remove(p);
                                    //p.father = p0;
                                    //p.GenerateGValue();
                                    //this.open_List.Add(p);
                                }

                            }
                            else
                            {
                                // 不在开放列表中,计算F = G + H，并放入开放列表
                                Point p = new Point(x, y, p0);
                                p.GenerateGValue();
                                p.GenerateHValue(end);
                                this.open_List.Add(p);
							}

                        }
                    }
                }
            }
        }



        /// <summary>
        /// Finds the shortest path.
        /// 查询最短路径（AStar算法）
        /// </summary>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        public void FindShortestPath(Point start, Point end)
        {
            // 起始点加入close列表
            this.open_List.Add(start);
            while (!this.IsInOpenList(end.x, end.y) || this.open_List.Count == 0)
            {
                Console.WriteLine(this.open_List.Count);
                Point p = this.GetMinFValueInOpenList();
                if (p == null)
                {
                    return;
                }

                this.open_List.Remove(p);
                this.close_List.Add(p);
                this.CheckPoint8(p, end);
            }

            Point endP = this.GetPointFromOpenList(end.x, end.y);
            while (endP.father != null)
            {
                endP = endP.father;
                this.map[endP.x, endP.y] = 3;
            }
        }

		/// <summary>
		/// Finds the shortest path2.
		/// http://www.cnblogs.com/leoin2012/p/3899822.html
		/// </summary>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		public void FindShortestPath2(Point start, Point end)
        {
            this.close_List.Add(start);
            Point nextP = start;

            while (true)
            {
				// 搜寻点P所有临近点(不在开放列表和封闭列表中)，计算出F值，设置父节点为P
				this.CheckPoint8(nextP, end);

				// 如果开放列表中已经没有点，则说明寻路失败，在找到目标点时候，已经找完所有路径点，算法结束
				if (this.open_List.Count <= 0)
				{
                    break;; // 寻路失败
				}

				// 开放列表中取得F值最小，作为寻路下一点
				nextP = this.GetMinFValueInOpenList2();
				
				if (nextP.IsEndPoint(end))
				{
                    break;  // 寻路成功
				}
                else
                {
					this.open_List.Remove(nextP);
					this.close_List.Add(nextP);
                }
            }

			Point endP = this.GetPointFromOpenList(end.x, end.y);
			while (endP.father != null)
			{
				endP = endP.father;
				this.map[endP.x, endP.y] = 3;
			}

        }


        /// <summary>
        /// Prints the map.
        /// 打印出寻路
        /// </summary>
        public void PrintMap()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int v = map[i, j];
                    if (v == 1)
                    {
                        Console.Write("*");
                    }
                    else if (v == 3)
                    {
                        Console.Write("#");
                    }
                    else if (v == 0)
                    {
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }

                Console.Write("\n");
            }
        }
    }
}
