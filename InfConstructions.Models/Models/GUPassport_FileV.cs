//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InfConstractions.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GUPassport_FileV
    {
        public System.Guid ID { get; set; }
        public System.Guid Passport_ID { get; set; }
        public byte[] tsc { get; set; }
        public System.DateTimeOffset upserted { get; set; }
        public System.Guid upserter { get; set; }
        public System.Guid stream_id { get; set; }
        public byte[] file_stream { get; set; }
        public string name { get; set; }
        public string file_type { get; set; }
        public Nullable<long> cached_file_size { get; set; }
        public System.DateTimeOffset creation_time { get; set; }
        public System.DateTimeOffset last_write_time { get; set; }
        public Nullable<System.DateTimeOffset> last_access_time { get; set; }
        public bool is_directory { get; set; }
        public bool is_offline { get; set; }
        public bool is_hidden { get; set; }
        public bool is_readonly { get; set; }
        public bool is_archive { get; set; }
        public bool is_system { get; set; }
        public bool is_temporary { get; set; }
    }
}
