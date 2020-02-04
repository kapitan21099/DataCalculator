using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2014, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count - 1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 25)),
                new WeekEnd(new DateTime(2017, 4, 29), new DateTime(2017, 4, 29))

            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 28)));
        }
        [TestMethod]
        public void TestWeekendAfterEndEmpty()
        {
            DateTime startDate = new DateTime(2019, 9, 30);
            int count = 6;
            DateTime expected = new DateTime(2019, 10, 5);
            WeekEnd[] weekends = new WeekEnd[]
            {
            };
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestSameDay()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2017, 4, 23), new DateTime(2017, 4, 23))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 26)));
        }

        [TestMethod]
        public void Test_Weekends_Start_And_Finish_Date()
        {
            DateTime startDate = new DateTime(2019, 9, 1);
            int count = 5;
            DateTime expected = new DateTime(2019, 9, 10);

            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2019, 9, 1), new DateTime(2019, 9, 5))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOutOfRange()
        {
            DateTime startDate = new DateTime(2017, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2017, 5, 28), new DateTime(2017, 5, 30))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(startDate.AddDays(count - 1)));
        }

        [TestMethod]
        public void TestOneDay_No_Weekends()
        {
            //Arrange.
            DateTime startDate = new DateTime(2020, 2, 4);
            int count = 1;
            WeekEnd[] weekends = new WeekEnd[0];

            //Act.
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            //Assert.
            DateTime expected = new DateTime(2020, 2, 4);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOneDay_No_OneWeekend()
        {
            //Arrange.
            DateTime startDate = new DateTime(2020, 2, 4);
            int count = 1;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2020, 2, 4), new DateTime(2020, 2, 4))
            };

            //Act.
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            //Assert.
            DateTime expected = new DateTime(2020, 2, 5);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestOneDay_No_OneOutSideWeekend()
        {
            //Arrange.
            DateTime startDate = new DateTime(2020, 2, 4);
            int count = 1;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2020, 2, 5), new DateTime(2020, 2, 5))
            };

            //Act.
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            //Assert.
            DateTime expected = new DateTime(2020, 2, 4);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void All()
        {
            //Arrange.
            DateTime startDate = new DateTime(2020, 1, 10);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2019, 1 ,1), new DateTime(2020, 1, 1)), // Left outside
                new WeekEnd(new DateTime(2019, 1 ,10), new DateTime(2020, 1, 14)), // +5 Left and rght point Eq +1
                new WeekEnd(new DateTime(2020, 2 ,1), new DateTime(2020, 2, 10)), // Right outside
            };

            //Act.
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            //Assert.
            DateTime expected = new DateTime(2020, 1, 19);
            Assert.AreEqual(expected, result);
        }
    }
}
