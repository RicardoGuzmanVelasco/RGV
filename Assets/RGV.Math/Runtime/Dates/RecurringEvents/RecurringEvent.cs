namespace RGV.Math.Runtime.Dates.RecurringEvents
{
    public class RecurringEvent
    {
        readonly TemporalExpression expression;
        readonly string id;

        public RecurringEvent(string id, TemporalExpression expression)
        {
            this.id = id;
            this.expression = expression;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;
            if(obj.GetType() != typeof(RecurringEvent))
                return false;
            return ((RecurringEvent)obj).id == this.id;
        }
    }
}