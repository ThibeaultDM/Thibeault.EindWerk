namespace Thibeault.EindWerk.Base.RulesFrameWork
{
    public class Rule
    {
        public bool Passed { get; set; }
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
            this.PropertyName = propertyName;

            if (valueToCheck == null)
            {
                this.Passed = false;
                this.Message = ($"Property '{propertyName}' is required");
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
            this.PropertyName = propertyName;

            if (string.IsNullOrWhiteSpace(valueToCheck) || valueToCheck == string.Empty)
            {
                this.Passed = false;
                this.Message = ($"Property '{propertyName}' is required and can not be empty or whitespace"); //$"Is required: {valueToCheck} for property {property}";
            }

            return this;
        }
    }
}