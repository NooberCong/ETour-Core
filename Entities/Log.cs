using Core.Interfaces;

namespace Core.Entities
{
    public class Log: TrackedEntityWithKey<int>
    {
        public string Content { get; set; }
        public LogType Type { get; set; }


        public enum LogType
        {
            Creation,
            Modification,
            Deletion,
            Warning,
        }
    }
}
