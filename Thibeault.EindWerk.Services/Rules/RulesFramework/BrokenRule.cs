namespace Thibeault.Example.Services.Rules.RulesFramework
{
    public class BrokenRule
    {
        public BrokenRule(string property, string message)
        {
            PropertyName = property;
            Message = message;
        }

        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}