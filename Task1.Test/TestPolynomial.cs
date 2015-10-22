using System;
using NUnit.Framework;
using Task1;
using System.Collections.Generic;
namespace Task1.Test{

    [TestFixture]
    public class TestPolynomial
    {
        [Test]
        public void EqualsTest()
        {
            var a = new Polynomial(1, 2, 3, 4);
            var b = new Polynomial(1, 2, 3, 4);
            Polynomial c = null;
            Assert.AreEqual(a.Equals(a),true);
            Assert.AreEqual(a.Equals(b), true);
            Assert.AreEqual(a == b, true);
            Assert.AreEqual(a == b, b == a);
            Assert.AreEqual(a != b, false);
            Assert.AreEqual(a.Equals(b), b.Equals(a));
            Assert.AreEqual(a.Equals((object)a), true);
            Assert.AreEqual(a.Equals(null), false);
            Assert.AreEqual(a == c, false);
            Assert.AreEqual(c == null, (object)c==null);
            a = new Polynomial(1, 2, 0);
            Assert.AreEqual(a.Degree,1);
          
        }

        public IEnumerable<TestCaseData> DataForToStringTest
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3)).Returns("3*X^2 + 2*X^1 + 1");
                yield return new TestCaseData(new Polynomial(1, 2, 0)).Returns("2*X^1 + 1");
            }
        }

        [Test, TestCaseSource(@"DataForToStringTest")]
        public string ToStringTest(Polynomial a)
        {
            return a.ToString();
        }
        public IEnumerable<TestCaseData> DataForAddTest
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(0, 2, 1)).Returns(new Polynomial(1, 4, 4));
                yield return new TestCaseData(new Polynomial(1, -2, -3), new Polynomial(0, 2, 3)).Returns(new Polynomial(1,0,0));
            }
        }
        [Test, TestCaseSource(@"DataForAddTest")]
        public Polynomial AddOperatorTest(Polynomial a,Polynomial b)
        {
            return a + b;
        }

        [Test, TestCaseSource(@"DataForAddTest")]
        public Polynomial AddTest(Polynomial a, Polynomial b)
        {
            return Polynomial.Add(a,b);
        }

        public IEnumerable<TestCaseData> DataForSubTest
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(0, 2, 1)).Returns(new Polynomial(1,0,2));
            }
        }
        [Test, TestCaseSource(@"DataForSubTest")]
        public Polynomial SubOperatorTest(Polynomial a, Polynomial b)
        {
            return a - b;
        }

        [Test, TestCaseSource(@"DataForSubTest")]
        public Polynomial SubTest(Polynomial a, Polynomial b)
        {
            return Polynomial.Substract(a, b);
        }

        public IEnumerable<TestCaseData> DataForMulTest
        {
            get
            {
                yield return new TestCaseData(new Polynomial(1, 2, 3), new Polynomial(0, 2, 1)).Returns(new Polynomial(0,2,5,8,3));
            }
        }
        [Test, TestCaseSource(@"DataForMulTest")]
        public Polynomial MulOperatorTest(Polynomial a, Polynomial b)
        {
            return a * b;
        }

        [Test, TestCaseSource(@"DataForMulTest")]
        public Polynomial MulTest(Polynomial a, Polynomial b)
        {
            return Polynomial.Multiply(a, b);
        }
    }
}
