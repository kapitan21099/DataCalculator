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
            DateTime finishDay;
            if (weekEnds == null || weekEnds.Length == 0)
            {
                finishDay = startDate.AddDays(dayCount);
            }
            else
            {
                int deltaDays = 0;
                for (int i = 0; i < weekEnds.Length; i++)
                {
                    TimeSpan deltaWeekends = weekEnds[i].EndDate - weekEnds[i].StartDate;
                    deltaDays += deltaWeekends.Days + 1;
                }
                finishDay = startDate.AddDays(dayCount + deltaDays);
            }
            return finishDay;
        }
    }
}
