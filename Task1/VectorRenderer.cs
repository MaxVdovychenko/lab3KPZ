using System;

namespace Task3
{
    public class VectorRenderer : IRenderer
    {
        public void Render(string shapeName)
        {
            Console.WriteLine($"Drawing {shapeName} as vectors");
        }
    }
}