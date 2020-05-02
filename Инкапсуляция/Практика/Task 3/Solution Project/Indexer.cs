using System;

namespace Incapsulation.Weights
{
    public class Indexer
    {
        private readonly double[] _array;
        private readonly int _start;
        private readonly int _length;
        public Indexer(double[] array, int start, int length)
        {
            if (!IsValid(array, start,length))
                throw new ArgumentException();
            _array = array;
            _start = start;
            _length = length;
        }
        public int Length { get => _length; }
        public double this[int index] 
        { 
            get => IsIndexValid(index)?
                _array[index + _start]
                : throw new IndexOutOfRangeException();
            
            set => _array[index+_start]=
                IsIndexValid(index) ? value
                : throw new IndexOutOfRangeException(); 
        }
        private bool IsIndexValid(int index) => index >= 0 && index < _length;
        private bool IsValid(double[] array, int start, int length)
            => !(start < 0 || length < 0 || array.Length < start + length);
    }
}
