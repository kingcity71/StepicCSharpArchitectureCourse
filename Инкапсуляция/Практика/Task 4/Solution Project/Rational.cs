using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public Rational(double numerator, double denominator = 1)
        {
            Numerator = numerator;
            Denominator = numerator == 0 && denominator != 0 ? 1 : denominator;
            IsNan = Denominator == 0;
            Reduce();
        }
        public bool IsNan { get; private set; }
        public double Denominator { get; private set; }
        public double Numerator { get; private set; }

        public static Rational operator +(Rational r1, Rational r2)
        {
            if (r1.Denominator == r2.Denominator) return new Rational(r1.Numerator + r2.Numerator, r1.Denominator);
            return new Rational(r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator, r1.Denominator * r2.Denominator);
        }

        public static Rational operator -(Rational r1, Rational r2)
        {
            if (r1.Denominator == r2.Denominator) return new Rational(r1.Numerator - r2.Numerator, r1.Denominator);
            return new Rational(r1.Numerator * r2.Denominator - r2.Numerator * r1.Denominator, r1.Denominator * r2.Denominator);
        }

        public static Rational operator *(Rational r1, Rational r2)
            => new Rational(r1.Numerator * r2.Numerator, r1.Denominator * r2.Denominator);

        public static Rational operator /(Rational r1, Rational r2)
           =>
            r1.IsNan || r2.IsNan ?
            new Rational(0, 0)
            : new Rational(r1.Numerator * r2.Denominator, r1.Denominator * r2.Numerator);

        public static implicit operator double(Rational r)
            => r.IsNan ? double.NaN : r.Numerator / r.Denominator;

        public static implicit operator Rational(int num)
            => new Rational(num);
        public static implicit operator int(Rational rational)
            => rational.Numerator % rational.Denominator == 0 ?
            (int)rational.Numerator :
            throw new InvalidCastException();
        private void Reduce()
        {
            if (Denominator == 0 || Numerator == 0) return;

            var nod = Math.Abs(GetNod((int)Numerator, (int)Denominator));
            Denominator /= nod;
            Numerator /= nod;

            if (Denominator < 0)
            {
                Denominator *= -1;
                Numerator *= -1;
            }
        }
        private static double GetNod(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
