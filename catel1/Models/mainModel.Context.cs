﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NormativeDocument> NormativeDocuments { get; set; }
        public virtual DbSet<AdminArea> AdminAreas { get; set; }
        public virtual DbSet<Ground_Type> Ground_Types { get; set; }
        public virtual DbSet<GUPassport> GUPassports { get; set; }
        public virtual DbSet<GUPassport_File> GUPassport_Files { get; set; }
        public virtual DbSet<GUPassport_Site> GUPassport_Sites { get; set; }
        public virtual DbSet<Installation> Installations { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<WorkTask> WorkTasks { get; set; }
        public virtual DbSet<ActualStatus> ActualStatuses { get; set; }
        public virtual DbSet<AddressObjectType> AddressObjectTypes { get; set; }
        public virtual DbSet<CenterStatus> CenterStatuses { get; set; }
        public virtual DbSet<CurrentStatus> CurrentStatuses { get; set; }
        public virtual DbSet<EstateStatus> EstateStatuses { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<HouseStateStatus> HouseStateStatuses { get; set; }
        public virtual DbSet<NormativeDocumentType> NormativeDocumentTypes { get; set; }
        public virtual DbSet<Object> Objects { get; set; }
        public virtual DbSet<OperationStatus> OperationStatuses { get; set; }
        public virtual DbSet<StructureStatus> StructureStatuses { get; set; }
        public virtual DbSet<proverkaGU> proverkaGUs { get; set; }
        public virtual DbSet<GUPassport_State> GUPassport_States { get; set; }
    
        public virtual int updateProverkaGU(Nullable<int> id, string ошибки_в_адресации_ГУ, string fact, string плашки, Nullable<bool> наличие_согласованного_макета, Nullable<bool> наличие_согласованного_паспорта, string примечание, Nullable<bool> проверено, Nullable<System.DateTimeOffset> updated, Nullable<System.Guid> newPassportID)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var ошибки_в_адресации_ГУParameter = ошибки_в_адресации_ГУ != null ?
                new ObjectParameter("Ошибки_в_адресации_ГУ", ошибки_в_адресации_ГУ) :
                new ObjectParameter("Ошибки_в_адресации_ГУ", typeof(string));
    
            var factParameter = fact != null ?
                new ObjectParameter("Fact", fact) :
                new ObjectParameter("Fact", typeof(string));
    
            var плашкиParameter = плашки != null ?
                new ObjectParameter("Плашки", плашки) :
                new ObjectParameter("Плашки", typeof(string));
    
            var наличие_согласованного_макетаParameter = наличие_согласованного_макета.HasValue ?
                new ObjectParameter("Наличие_согласованного_макета", наличие_согласованного_макета) :
                new ObjectParameter("Наличие_согласованного_макета", typeof(bool));
    
            var наличие_согласованного_паспортаParameter = наличие_согласованного_паспорта.HasValue ?
                new ObjectParameter("Наличие_согласованного_паспорта", наличие_согласованного_паспорта) :
                new ObjectParameter("Наличие_согласованного_паспорта", typeof(bool));
    
            var примечаниеParameter = примечание != null ?
                new ObjectParameter("Примечание", примечание) :
                new ObjectParameter("Примечание", typeof(string));
    
            var провереноParameter = проверено.HasValue ?
                new ObjectParameter("Проверено", проверено) :
                new ObjectParameter("Проверено", typeof(bool));
    
            var updatedParameter = updated.HasValue ?
                new ObjectParameter("updated", updated) :
                new ObjectParameter("updated", typeof(System.DateTimeOffset));
    
            var newPassportIDParameter = newPassportID.HasValue ?
                new ObjectParameter("newPassportID", newPassportID) :
                new ObjectParameter("newPassportID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("updateProverkaGU", idParameter, ошибки_в_адресации_ГУParameter, factParameter, плашкиParameter, наличие_согласованного_макетаParameter, наличие_согласованного_паспортаParameter, примечаниеParameter, провереноParameter, updatedParameter, newPassportIDParameter);
        }
    
        [DbFunction("Entities", "ObjectFullAddress4")]
        public virtual IQueryable<ObjectFullAddress4_Result> ObjectFullAddress4(Nullable<System.Guid> houseGUID, string splitter, Nullable<bool> nameFull, Nullable<bool> withRoot, Nullable<bool> withSelf)
        {
            var houseGUIDParameter = houseGUID.HasValue ?
                new ObjectParameter("houseGUID", houseGUID) :
                new ObjectParameter("houseGUID", typeof(System.Guid));
    
            var splitterParameter = splitter != null ?
                new ObjectParameter("Splitter", splitter) :
                new ObjectParameter("Splitter", typeof(string));
    
            var nameFullParameter = nameFull.HasValue ?
                new ObjectParameter("NameFull", nameFull) :
                new ObjectParameter("NameFull", typeof(bool));
    
            var withRootParameter = withRoot.HasValue ?
                new ObjectParameter("WithRoot", withRoot) :
                new ObjectParameter("WithRoot", typeof(bool));
    
            var withSelfParameter = withSelf.HasValue ?
                new ObjectParameter("WithSelf", withSelf) :
                new ObjectParameter("WithSelf", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ObjectFullAddress4_Result>("[Entities].[ObjectFullAddress4](@houseGUID, @Splitter, @NameFull, @WithRoot, @WithSelf)", houseGUIDParameter, splitterParameter, nameFullParameter, withRootParameter, withSelfParameter);
        }
    
        [DbFunction("Entities", "currentUserID")]
        public virtual IQueryable<currentUserID_Result> currentUserID()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<currentUserID_Result>("[Entities].[currentUserID]()");
        }
    
        [DbFunction("Entities", "fullAddress")]
        public virtual IQueryable<fullAddress_Result> fullAddress()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fullAddress_Result>("[Entities].[fullAddress]()");
        }
    
        [DbFunction("Entities", "AllAddressObjects")]
        public virtual IQueryable<AllAddressObjects_Result> AllAddressObjects()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<AllAddressObjects_Result>("[Entities].[AllAddressObjects]()");
        }
    }
}
