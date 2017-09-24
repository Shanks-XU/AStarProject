using System;

namespace A星算法1
{
	/// <summary>
	/// Main class.
	/// http://www.cnblogs.com/lipan/archive/2010/07/01/1769420.html
	/// </summary>

	class MainClass
    {
		

        public static void Main(string[] args)
        {
			// 定义Map：0-为障碍
		    byte[,] map = new byte[10, 10]
		   {
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },   // 1
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },   // 2
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },   // 3
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },   // 4
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },   // 5
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },   // 6
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },   // 7
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },   // 8
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },   // 9
            { 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 }    // 10
           };

            Map mapDirector = new Map(map);

            // 出发点
            Point start = new Point(1, 1, null);

            // 终点
            Point end = new Point(8, 8, null);

            mapDirector.FindShortestPath2(start, end);



            mapDirector.PrintMap();

            Console.WriteLine("Hello World!");
        }
    }
}
