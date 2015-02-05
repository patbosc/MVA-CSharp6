using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{

    public class Point
    {
        private readonly int _x;

        public int X { get { return _x; } }

        private readonly int _y;

        public int Y { get { return _y; } }

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override string ToString()
        {
            return String.Format("pt({0},{1})", X, Y).ToString();
        }

        public double Dist
        {
            get { return Math.Sqrt(X * X + Y * Y); }
        }

        public static Point FromJson(JObject json)
        {
            if (json != null &&
               json["x"] != null &&
               json["x"].Type == JTokenType.Integer &&
               json["y"] != null &&
               json["y"].Type == JTokenType.Integer)
            {
                return new Point((int)json["x"], (int)json["y"]);
            }
            return null;
        }

        public JObject ToJson()
        {
            var result = new JObject();
            result["x"] = X;
            result["y"] = Y;
            return result;
        }

        private static async Task<JArray> GetArrayAsync(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            Repository repository = await Repository.OpenAsync(path);
            try
            {
                var contents = await repository.ReadAsync();
                return JArray.Parse(contents);
            }
            catch (RepositoryException e)
            {
                // log it async
            }
            finally
            {
                // close it async
            }
            return new JArray();
        }

        public static string ToString(IEnumerable<Point> points)
        {
            var sb = new StringBuilder("[");
            bool first = true;
            foreach (var p in points)
            {
                if (first) 
                {
                    first = false;
                    sb.Append("{");
                }

                sb.Append(p);
            }
            sb.Append("}");
            sb.Append("]");
            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var points = new[] { new Point(3, 4), new Point(-1, 0), new Point(5, -2), new Point(7, 6), new Point(0, 0) };
            Console.WriteLine(Point.ToString(points));

            var json = new JArray(from p in points select p.ToJson().ToString());
            Console.WriteLine(json);
        }
    }
}