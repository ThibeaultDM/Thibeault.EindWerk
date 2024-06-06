using Thibeault.EindWerk.Objects;

namespace Thibeault.EindWerk.Services.RulesFrameWork
{
    public class BusinessObject : BaseObject
    {
        public bool IsValid
        { get { return Validate(); } }

        public List<Rule> Rules { get; set; } = new();

        public List<BrokenRule> BrokenRules { get; set; } = new();

        /// <summary>
        /// Checks if the rules are valid
        /// </summary>
        /// <returns>returns true or false</returns>
        private bool Validate()
        {
            this.BrokenRules = new List<BrokenRule>(); //always reset the list to prevent duplicates

            bool rulesPassed = AddBusinessRules();

            return rulesPassed;
        }

        public virtual bool AddBusinessRules()
        {
            return CheckRules(Rules);
        }

        /// <summary>
        /// Checks for each item in the businessrules list if they are valid
        /// </summary>
        /// <returns>true or false</returns>
        protected virtual bool CheckRules(List<Rule> rules)
        {
            if (rules != null && rules.Count > 0)
            {
                foreach (var item in rules)
                {
                    if (!item.Passed)
                    {
                        this.BrokenRules.Add(new BrokenRule(item.Message, item.PropertyName));
                    }
                }
            }

            if (this.BrokenRules == null || this.BrokenRules.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}