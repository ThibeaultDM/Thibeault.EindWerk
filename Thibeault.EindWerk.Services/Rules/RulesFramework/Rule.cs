namespace Thibeault.EindWerk.Services.Rules.RulesFramework
{
    public class Rule
    {
        public bool Passed { get; set; } = true;
        public string PropertyName { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Rule: object cannot be null
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="valueToCheck"></param>
        /// <returns>Business rule</returns>
        public Rule IsRequired(string propertyName, object valueToCheck)
        {
            PropertyName = propertyName;

            if (valueToCheck == null)
            {
                Passed = false;
                Message = $"Property '{propertyName}' is required";
            }

            return this;
        }

        /// <summary>
        /// Rule: string cannot be null or only whitespace characters
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="valueToCheck"></param>
        /// <returns>Business rule</returns>
        public Rule IsRequired(string propertyName, string valueToCheck)
        {
            PropertyName = propertyName;

            if (string.IsNullOrWhiteSpace(valueToCheck) || valueToCheck == string.Empty)
            {
                Passed = false;
                Message = $"Property '{propertyName}' is required and can not be empty or whitespace";
            }

            return this;
        }
    }
}