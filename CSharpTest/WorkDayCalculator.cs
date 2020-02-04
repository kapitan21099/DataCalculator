using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            DateTime finishDay = startDate.AddDays(dayCount - 1);
            if (weekEnds == null || weekEnds.Length == 0)
            {
                return finishDay;
            }
            else
            {
                for (int i = 0; i < weekEnds.Length; i++)
                {
                    if (IsLeft(startDate, weekEnds, finishDay, i))
                    {
                        finishDay = finishDay.AddDays((weekEnds[i].EndDate - startDate).Days + 1);
                    }
                    else if (IsRight(startDate, weekEnds, finishDay, i))
                    {
                        int days = (finishDay - weekEnds[i].StartDate).Days + 1;
                        finishDay = finishDay.AddDays(days);
                    }
                    else if (IsInside(startDate, weekEnds, finishDay, i))
                    {
                        return finishDay.AddDays((weekEnds[i].EndDate - weekEnds[i].StartDate).Days + 1);
                    }
                }
            }
            return finishDay;
        }

        private static bool IsLeft(DateTime startDate, WeekEnd[] weekEnds, DateTime finishDay, int i)
        {
            bool v2 = (weekEnds[i].StartDate <= startDate);
            bool v = (weekEnds[i].EndDate >= startDate);
            bool v1 = (weekEnds[i].EndDate <= finishDay);
            return v & v1 & v2;
        }

        private static bool IsInside(DateTime startDate, WeekEnd[] weekEnds, DateTime finishDay, int i)
        {
            return (weekEnds[i].StartDate > startDate) & (weekEnds[i].EndDate < finishDay);
        }

        private static bool IsRight(DateTime startDate, WeekEnd[] weekEnds, DateTime finishDay, int i)
        {
            bool v = (weekEnds[i].StartDate <= finishDay);
            bool v1 = (weekEnds[i].StartDate >= startDate);
            bool v2 = (weekEnds[i].EndDate >= finishDay);
            return v & v1 & v2;
        }
    }
}
