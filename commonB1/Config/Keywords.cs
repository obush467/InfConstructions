using System.Configuration;

namespace commonB1.Config
{
    [ConfigurationCollection(typeof(Keyword), AddItemName = "Keyword")]
    public class Keywords : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Keyword();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Keyword)(element)).Name;
        }
        public Keyword this[int idx]
        {
            get { return (Keyword)BaseGet(idx); }
        }
    }
}
