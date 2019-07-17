using System.Configuration;

namespace commonB1.Config
{
    public class commonB1_config : ConfigurationSection
    {
        [ConfigurationProperty("StandartNames")]
        public StandartNames standartNames
        { get { return (StandartNames)this["StandartNames"]; } }
        [ConfigurationProperty("Keywords")]
        public Keywords keywords
        { get { return (Keywords)this["Keywords"]; } }
    }
}
