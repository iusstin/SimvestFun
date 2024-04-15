using Quartz;

namespace SimvestFun.ApplicationCore.Jobs
{
    public class Job
    {
        public Job(Type type, ITrigger trigger)
        {
            Type = type;
            this.trigger = trigger;
        }

        public Type Type { get; set; }
        public string TimeExpression { get; set; }
        public ITrigger trigger;
    }
}
