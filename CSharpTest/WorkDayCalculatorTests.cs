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
            DateTime startDate = new DateTime(2019, 2, 2);
            DateTime expected = new DateTime(2019, 2, 12);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2019, 2, 2);
            DateTime expected = new DateTime(2019, 2, 16);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2019, 2, 5), new DateTime(2019, 2, 8))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestNormalPath2()
        {
            DateTime startDate = new DateTime(2019, 1, 23);
            DateTime expected = new DateTime(2019, 2, 8);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2019, 1, 29), new DateTime(2019, 2, 3))//6
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(expected, result);
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

            Assert.IsTrue(result.Equals(new DateTime(2017, 4, 30)));
        }

        [TestMethod]
        public void TestWeekendAfterEndTwoPeriods()
        {
            DateTime startDate = new DateTime(2019, 9, 30);
            int count = 50;
            DateTime expected = new DateTime(2019, 11, 28);
            WeekEnd[] weekends = new WeekEnd[]
            {
                new WeekEnd(new DateTime(2019, 10, 10), new DateTime(2019, 10, 11)),
                new WeekEnd(new DateTime(2019, 10, 30), new DateTime(2019, 11, 1)),
                new WeekEnd(new DateTime(2019, 11, 5), new DateTime(2019, 11, 8)),
            };
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestWeekendAfterEndEmpty()
        {
            DateTime startDate = new DateTime(2019, 9, 30);
            int count = 50;
            DateTime expected = new DateTime(2019, 11, 19);
            WeekEnd[] weekends = new WeekEnd[]
            {
            };
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.AreEqual(expected, result);
        }
    }
}
