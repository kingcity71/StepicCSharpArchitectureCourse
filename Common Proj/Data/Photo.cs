using MyPhotoshop.Data;
using System;

namespace MyPhotoshop
{
	public class Photo
	{
		readonly int _width;
		readonly int _height;
		readonly Pixel[,] _data;
		public Photo(int w, int h)
		{
			_width = w;
			_height = h;
			_data = new Pixel[_width, _height];
		}
		public int Width { get => _width; }
		public int Height { get => _height; }

		public Pixel this[int x, int y]
		{
			get => _data[x, y];
			set => _data[x, y] = value;
		} 
	}
}

