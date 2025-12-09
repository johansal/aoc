namespace Day9;

public static class GeometryHelper
{
    public static bool LinesIntersect((int x, int y) p1, (int x, int y) p2, (int x, int y) p3, (int x, int y) p4)
    {
        // Calculate direction of the lines
        long d1 = CrossProduct(p3, p4, p1);
        long d2 = CrossProduct(p3, p4, p2);
        long d3 = CrossProduct(p1, p2, p3);
        long d4 = CrossProduct(p1, p2, p4);
        
        // Check if lines properly intersect (crossing each other)
        // Positive indicates left side, negative right side and zero is collinear
        if (((d1 > 0 && d2 < 0) || (d1 < 0 && d2 > 0)) &&
            ((d3 > 0 && d4 < 0) || (d3 < 0 && d4 > 0)))
        {
            return true;
        }
        return false;
    }

    private static long CrossProduct((int x, int y) a, (int x, int y) b, (int x, int y) c)
    {
        //use long just in case of large coordinates
        return (long)(b.x - a.x) * (c.y - a.y) - (long)(b.y - a.y) * (c.x - a.x);
    } 
    public static bool PointInPolygon((int x, int y) point, List<(int x, int y)> polygon)
    {
        bool inside = false;
        int n = polygon.Count;
        
        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            var pi = polygon[i];
            var pj = polygon[j];
            
            // Check if point is on a horizontal edge
            if (pi.y == pj.y && pi.y == point.y)
            {
                if ((point.x >= Math.Min(pi.x, pj.x)) && (point.x <= Math.Max(pi.x, pj.x)))
                {
                    return true; // Point is on the edge
                }
            }
            
            // Check if point is on a vertical edge
            if (pi.x == pj.x && pi.x == point.x)
            {
                if ((point.y >= Math.Min(pi.y, pj.y)) && (point.y <= Math.Max(pi.y, pj.y)))
                {
                    return true; // Point is on the edge
                }
            }
            
            // Ray casting algorithm
            if (((pi.y > point.y) != (pj.y > point.y)) &&
                (point.x < (pj.x - pi.x) * (point.y - pi.y) / (pj.y - pi.y) + pi.x))
            {
                inside = !inside;
            }
        }
        
        return inside;
    }

    /// <summary>
    /// Checks if a rectangle (defined by two opposite corners) is completely inside a polygon.
    /// The rectangle's edges may touch or lie on the polygon boundary.
    /// </summary>
    public static bool RectangleInsidePolygon((int x, int y) p1, (int x, int y) p2, List<(int x, int y)> polygon)
    {
        // Get all four corners of the rectangle
        var corners = new (int x, int y)[]
        {
            (p1.x, p1.y),
            (p1.x, p2.y),
            (p2.x, p1.y),
            (p2.x, p2.y)
        };
        
        // Check if all corners are inside or on the polygon
        foreach (var corner in corners)
        {
            if (!PointInPolygon(corner, polygon))
            {
                return false;
            }
        }

        // Walk the polygon edges to ensure rectangle edges do not cross polygon edges
        for (int i = 0; i < polygon.Count; i++)
        {
            var pA = polygon[i];
            var pB = polygon[(i + 1) % polygon.Count];

            // Check each edge of the rectangle
            var rectangleEdges = new ( (int x, int y) start, (int x, int y) end )[]
            {
                ( (p1.x, p1.y), (p1.x, p2.y) ),
                ( (p1.x, p2.y), (p2.x, p2.y) ),
                ( (p2.x, p2.y), (p2.x, p1.y) ),
                ( (p2.x, p1.y), (p1.x, p1.y) )
            };

            foreach (var edge in rectangleEdges)
            {
                if (LinesIntersect(pA, pB, edge.start, edge.end))
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Calculates the area of a rectangle defined by two opposite corners.
    /// Includes boundary points in the count.
    /// </summary>
    public static long RectangleArea((int x, int y) p1, (int x, int y) p2)
    {
        int length = Math.Abs(p2.x - p1.x) + 1;
        int width = Math.Abs(p2.y - p1.y) + 1;
        return (long)length * width;
    }

    /// <summary>
    /// Finds the largest rectangle that can be formed by any two vertices of the polygon.
    /// </summary>
    public static long FindLargestRectangle(List<(int x, int y)> vertices)
    {
        long maxArea = 0;
        
        for (int i = 0; i < vertices.Count; i++)
        {
            for (int j = i + 1; j < vertices.Count; j++)
            {
                var area = RectangleArea(vertices[i], vertices[j]);
                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }
        
        return maxArea;
    }

    /// <summary>
    /// Finds the largest rectangle (with corners at polygon vertices) that fits inside the polygon.
    /// </summary>
    public static long FindLargestRectangleInsidePolygon(List<(int x, int y)> polygon)
    {
        long maxArea = 0;
        
        for (int i = 0; i < polygon.Count; i++)
        {
            for (int j = i + 1; j < polygon.Count; j++)
            {
                if (RectangleInsidePolygon(polygon[i], polygon[j], polygon))
                {
                    var area = RectangleArea(polygon[i], polygon[j]);
                    if (area > maxArea)
                    {
                        maxArea = area;
                    }
                }
            }
        }
        
        return maxArea;
    }
}
