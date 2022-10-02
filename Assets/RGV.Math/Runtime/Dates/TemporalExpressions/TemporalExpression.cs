using System;

namespace RGV.Math.Runtime.Dates.TemporalExpressions
{
    public interface TemporalExpression
    {
        bool Includes(DateTime when);
    }
}