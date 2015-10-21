using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Polynomial
{
    public class Polynomial
    {
        private double[] _odds;
        public double this[int i]
        {
            get { return _odds[i]; }
        }

        public int Degree { get; private set; }

        public Polynomial(double[] odds)
        {
            if (!(odds == null))
            {
                _odds = new double[odds.Length];
                ReduceOdd(ref _odds);
                odds.CopyTo(_odds, 0);
                Degree = odds.Length;
            }
        }

        private Polynomial(ref double[] odds)
        {
            if (!(odds == null))
            {
                ReduceOdd(ref _odds);
                _odds = odds;
                Degree = odds.Length;
            }
        }
        //public Polynomial(params double[] a)
        //{
        //    _odds = a;
        //}


        public static Polynomial Substract(Polynomial a, Polynomial b)
        {
            return a - b;
        }
        public static Polynomial operator -(Polynomial a,Polynomial b)
        {
            if (((object)a == null) || ((object)b == null))
                return null;
            
            if(a._odds.Length>b._odds.Length)
            {
                double[] temp = new double[a._odds.Length];
                a._odds.CopyTo(temp, 0);
                for (int i = 0; i < b._odds.Length; i++)
                    temp[i] -= b[i];
                return new Polynomial(ref temp);

            }
            else
            {
                double[] temp = new double[b._odds.Length];
                for (int i = 0; i < b._odds.Length; i++)
                    temp[i] = (-1) * b[i];
                 for (int i = 0; i < a._odds.Length; i++)
                     temp[i] +=a[i];
                return new Polynomial(ref temp);

            }
        }


        public static Polynomial Add(Polynomial a,Polynomial b)
        {
            return a + b;
        }

        public static Polynomial operator +(Polynomial a,Polynomial b)
        {
            if (((object)a == null) || ((object)b == null))
                return null;
            
            var max = a._odds.Length > b._odds.Length ? a : b;
            var min = a._odds.Length > b._odds.Length ? b : a;

            double[] temp = new double[max._odds.Length]; 
            max._odds.CopyTo(temp, 0);

            for (int i=0;i<min._odds.Length;i++)
                 temp[i] += min[i];
            return new Polynomial(ref temp);

        }

       public static Polynomial Multiply(Polynomial a,Polynomial b)
        {
            return a * b;
        }
        public static Polynomial operator *(Polynomial a,Polynomial b)
        {
            if (((object)a == null) || ((object)b == null))
                return null;

            double[] temp = new double[a._odds.Length+b._odds.Length]; 

            for(int i=0;i<a._odds.Length;i++)
                for (int j=0;j<b._odds.Length;j++)
                {
                    temp[i + j] += a[i] * b[j];
                }
            return new Polynomial(ref temp);

        }




        public static bool operator ==(Polynomial a,Polynomial b)
        {
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b);
        }


        public static bool operator !=(Polynomial a, Polynomial b)
        {
            return !(a==b);
        }


        //TODO: Посидеть ещё с Equals
        public bool Equals(Polynomial p)
        {
            if ((object)p == null)
                return false;

            if (_odds.Length != p._odds.Length)
                return false;

            //  return _odds.SequenceEqual(p._odds);
            for (int i = 0; i < _odds.Length; i++)
            {
                if (this[i] != p[i])
                    return false;
            }
            return true;

        }

        //Зачем??
        private static void ReduceOdd(ref double[] ar)
        {
            int length = ar.Length;
            while (length != 0 && ar[length - 1] == 0)
                length--;
            var temp_arr = new double[length];
            for (int i = 0; i < length; i++)
                Array.Resize(ref ar, length);
        }


        public override bool Equals(object obj)
        {
            if (!(obj is Polynomial))
                return false;
            return Equals((Polynomial)obj);
        }

        public override int GetHashCode()
        {
            return _odds.Length.GetHashCode() ^ (int)_odds[0];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = _odds.Length - 1; i >= 0;i--)
            {
                sb.Append((_odds[i]<0 ? "":"+") + String.Format("{0}*X^{1}",_odds[i],i)  );
            }
            return sb.ToString();
        }

    }
}
