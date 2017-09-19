using System.Configuration;

namespace commonB1.Config
{
    [ConfigurationCollection(typeof(StandartName),AddItemName="StandartName")]
    public class StandartNames:ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new StandartName();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((StandartName)(element)).Name;
        }
        public StandartName this[int idx]
        {
            get { return (StandartName)BaseGet(idx); }
        }

    }
}
