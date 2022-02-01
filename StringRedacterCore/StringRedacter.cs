using System.Collections;

namespace StringRedacterProject
{
    public class StringRedacter
    {
        public string DefaultText { get; set; }

        public StringRedacter() {
            DefaultText = "redacted";
        }
        public StringRedacter(string defaultText)
        {
            DefaultText = defaultText;
        }

        public void Redact(object obj)
        {
            if (obj == null)
                return;

            if (obj.GetType().IsValueType)
                return;

            if (RedactAsDictionary(obj))
                return;

            if (RedactAsList(obj))
                return;

            // if none of the above, go through properties
            foreach (var prop in obj.GetType().GetProperties())
            {
                var propValue = prop.GetValue(obj, null);

                if (propValue == null)
                    continue;

                if (propValue is string)
                {
                    var setter = prop.GetSetMethod();
                    if(setter != null)
                        prop.SetValue(obj, DefaultText);
                    continue;
                }

                Redact(propValue);
            }
        }

        protected virtual bool RedactAsList(object obj)
        {
            if (!(obj is IList))
                return false;

            var list = obj as IList;
            if (list == null)
                return true;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] is string)
                {
                    list[i] = DefaultText;
                }
                else
                {
                    Redact(list[i]);
                }
            }

            return true;
        }

        protected virtual bool RedactAsDictionary(object obj)
        {
            if (!(obj is IDictionary))
                return false;

            var dict = obj as IDictionary;
            if (dict == null)
                return true;

            foreach (var key in dict.Keys)
            {
                if (dict[key] is string)
                {
                    dict[key] = DefaultText;
                }
                else
                {
                    Redact(dict[key]);
                }
            }

            return true;
        }
    }
}
