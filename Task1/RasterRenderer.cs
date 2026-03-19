using System;

namespace Task3
{
    public class RasterRenderer : IRenderer
    {
        public void Render(string shapeName)
        {
            Console.WriteLine($"Drawing {shapeName} as pixels");
        }
    }
}