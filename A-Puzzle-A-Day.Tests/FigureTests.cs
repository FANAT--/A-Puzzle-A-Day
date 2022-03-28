using System;
using A_Puzzle_A_Day_Solver;
using NUnit.Framework;

namespace A_Puzzle_A_Day.Tests
{
    public class FigureTests
    {
        [Test]
        public void Validate()
        {
            var ex = Assert.Throws<ArgumentException>(() => new Figure(0, 0, 0, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0));
            Assert.AreEqual("Первая строка пуста", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => new Figure(0, 1, 1, 0,
                0, 1, 0, 0,
                0, 1, 0, 0,
                0, 0, 0, 0));
            Assert.AreEqual("Первый столбец пустой", ex.Message);
        }

        [Test]
        public void Width()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            Assert.AreEqual(3, figure.Width);
        }

        [Test]
        public void Height()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            Assert.AreEqual(3, figure.Height);
        }

        [Test]
        public void Test_ToString()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            Console.WriteLine(figure);

            Assert.AreEqual("XXX\r\nX\r\nX", figure.ToString());
        }

        [Test]
        public void Test_Equals()
        {
            var figure1 = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            var figure2 = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            var figure3 = new Figure(
                1, 1, 1, 1,
                0, 1, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);

            Assert.IsTrue(figure1.Equals(figure2));
            Assert.IsTrue(figure2.Equals(figure1));
            Assert.IsFalse(figure1.Equals(figure3));
            Assert.IsFalse(figure2.Equals(figure3));
        }

        [Test]
        public void Test_GetHashCode()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            // биты хранятся построчно в обратном порядке
            Assert.AreEqual(0b0111_0001_0001_0000, figure.GetHashCode());
        }

        [Test]
        public void GetTurned()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            var turnedFigure = figure.GetTurned();

            Console.WriteLine(turnedFigure);

            Assert.AreEqual("XXX\r\n  X\r\n  X", turnedFigure.ToString());
        }

        [Test]
        public void GetRotated90()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            var rotatedFigure = figure.GetRotated90();

            Console.WriteLine(rotatedFigure);

            Assert.AreEqual("XXX\r\n  X\r\n  X", rotatedFigure.ToString());
        }

        [Test]
        public void GetKinds()
        {
            var figure = new Figure(
                1, 1, 1, 0,
                1, 0, 0, 0,
                1, 0, 0, 0,
                0, 0, 0, 0);

            var kinds = figure.GetKinds();

            foreach (var kind in kinds)
            {
                Console.WriteLine(kind);
                Console.WriteLine();
            }

            Assert.AreEqual(4, kinds.Count);
        }

        [Test]
        public void GetKinds2()
        {
            var figure = new Figure(
                1, 1, 1, 1,
                0, 0, 1, 0,
                0, 0, 0, 0,
                0, 0, 0, 0);

            var kinds = figure.GetKinds();

            foreach (var kind in kinds)
            {
                Console.WriteLine(kind);
                Console.WriteLine();
            }

            Assert.AreEqual(8, kinds.Count);
        }
    }
}
