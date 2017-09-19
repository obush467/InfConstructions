using System.Configuration;

namespace commonB1.Config
{
    public class filesOperateParameters:ConfigurationElement
    {
        [ConfigurationProperty("Name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["Name"])); }
            set { base["Name"] = value; }
        }
        [ConfigurationProperty("DeleteDuplicates", DefaultValue = false, IsKey = false, IsRequired = true)]
        public bool DeleteDuplicates
        {
            get { return ((bool)(base["DeleteDuplicates"])); }
            set { base["DeleteDuplicates"] = value; }
        }

        [ConfigurationProperty("DeleteEmptyFolders", DefaultValue =  true, IsKey = false, IsRequired = true)]
        public bool DeleteEmptyFolders
        {
            get { return ((bool)(base["DeleteEmptyFolders"])); }
            set { base["DeleteEmptyFolders"] = value; }
        }
        [ConfigurationProperty("DirectoryNamePriorityFirst", DefaultValue = false, IsKey = false, IsRequired = true)]
        public bool DirectoryNamePriorityFirst
        {
            get { return ((bool)(base["DirectoryNamePriorityFirst"])); }
            set { base["DirectoryNamePriorityFirst"] = value; }
        }
        [ConfigurationProperty("FileNamePriority", DefaultValue = true, IsKey = false, IsRequired = true)]
        public bool FileNamePriority
        {
            get { return ((bool)(base["FileNamePriority"])); }
            set { base["FileNamePriority"] = value; }
        }
        [ConfigurationProperty("InDirectory", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string InDirectory
        {
            get { return ((string)(base["InDirectory"])); }
            set { base["InDirectory"] = value; }
        }

        [ConfigurationProperty("OutDirectory", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string OutDirectory
        {
            get { return ((string)(base["OutDirectory"])); }
            set { base["OutDirectory"] = value; }
        }
        [ConfigurationProperty("InsertedFileNamePart", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string InsertedFileNamePart
        {
            get { return ((string)(base["InsertedFileNamePart"])); }
            set { base["InsertedFileNamePart"] = value; }
        }
        [ConfigurationProperty("FileNumberRegExp", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string FileNumberRegExp
        {
            get { return ((string)(base["FileNumberRegExp"])); }
            set { base["FileNumberRegExp"] = value; }
        }
        [ConfigurationProperty("RenamedFilesRegExp", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string RenamedFilesRegExp
        {
            get { return ((string)(base["RenamedFilesRegExp"])); }
            set { base["RenamedFilesRegExp"] = value; }
        }
        [ConfigurationProperty("RenamedFilesExtensionsRegExp", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string RenamedFilesExtensionsRegExp
        {
            get { return ((string)(base["RenamedFilesExtensionsRegExp"])); }
            set { base["RenamedFilesExtensionsRegExp"] = value; }
        }
        [ConfigurationProperty("NotRenamedFilesRegExp", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string NotRenamedFilesRegExp
        {
            get { return ((string)(base["NotRenamedFilesRegExp"])); }
            set { base["NotRenamedFilesRegExp"] = value; }
        }
        [ConfigurationProperty("NotRenamedFilesExtensionsRegExp", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string NotRenamedFilesExtensionsRegExp
        {
            get { return ((string)(base["NotRenamedFilesExtensionsRegExp"])); }
            set { base["NotRenamedFilesExtensionsRegExp"] = value; }
        }
    }
}