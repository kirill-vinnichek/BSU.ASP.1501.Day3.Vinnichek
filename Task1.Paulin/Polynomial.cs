using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Polynomial
    {
        private readonly double[] _odds;
        public double this[int i]
        {
            get { return _odds[i]; }
        }

        public int Degree { get; private set; }

        public Polynomial(params double[] odds)
        {
            if (odds == null)
                throw new ArgumentNullException("Argument can not be null");
            if (odds.Length == 0)
            {
                this._odds = new double[] { 0 };
            }
            else
            {
                this._odds = new double[odds.Length];
                ReduceOdd(ref odds);
                odds.CopyTo(_odds, 0);
            }

                Degree = odds.Length - 1;
            

           
        }

        private Polynomial(ref double[] odds)
        {
            if (odds == null)
                throw new ArgumentNullException("Argument can not be null");
            if (odds.Length == 0)
            {
                this._odds = new double[] { 0 };
            }
            {
                ReduceOdd(ref odds);
                this._odds = odds;
            }

            Degree = odds.Length - 1;

        }



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
            if (object.ReferenceEquals(a, b))
                return true;

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

        public override bool Equals(object obj)
        {
            if (!(obj is Polynomial))
                return false;
            return Equals((Polynomial)obj);
        }
      
        private static void ReduceOdd(ref double[] ar)
        {
            int length = ar.Length;
            while (length != 0 && ar[length - 1] == 0)
                length--;
            var temp_arr = new double[length];
            for (int i = 0; i < length; i++)
                Array.Resize(ref ar, length);
        }



        public override int GetHashCode()
        {
            return _odds.Length.GetHashCode() ^ (int)_odds[0];
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = Degree; i > 0;i--)
            {
                if (this[i] == 0)
                    continue;
                if (i == Degree)
                    sb.Append(String.Format("{0}*X^{1}", _odds[i], i));
                else
                sb.Append((_odds[i]<0 ? "":" + ") + String.Format("{0}*X^{1}",_odds[i],i)  );
            }
            sb.Append(String.Format(" + {0}",this[0]));
            return sb.ToString();
        }

    }
}
