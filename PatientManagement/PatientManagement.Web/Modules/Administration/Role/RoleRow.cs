﻿using PatientManagement.PatientManagement.Scripts;

namespace PatientManagement.Administration.Entities
{
    using Serenity.ComponentModel;
    using Serenity.Data;
    using Serenity.Data.Mapping;
    using System;
    using System.ComponentModel;

    [ConnectionKey("Default"), DisplayName("Roles"), InstanceName("Role"), TwoLevelCached]
    [ReadPermission("Administration:Role:Read")]
    [ModifyPermission("Administration:Role:Modify")]
    [LookupScript("Administration.Role", 
        LookupType = typeof(MultiTenantRowLookupScript<>) )]
    public sealed class RoleRow : Row, IIdRow, INameRow, IMultiTenantRow
    {
        [DisplayName("Role Id"), Identity, ForeignKey("Roles", "RoleId"), LeftJoin("jRole")]
        public Int32? RoleId
        {
            get { return Fields.RoleId[this]; }
            set { Fields.RoleId[this] = value; }
        }

        [DisplayName("Role Name"), Size(100), NotNull, QuickSearch]
        public String RoleName
        {
            get { return Fields.RoleName[this]; }
            set { Fields.RoleName[this] = value; }
        }

        [Insertable(false), Updatable(false)]
        public Int32? TenantId
        {
            get { return Fields.TenantId[this]; }
            set { Fields.TenantId[this] = value; }
        }

        IIdField IIdRow.IdField
        {
            get { return Fields.RoleId; }
        }

        StringField INameRow.NameField
        {
            get { return Fields.RoleName; }
        }

        public static readonly RowFields Fields = new RowFields().Init();

        public RoleRow()
            : base(Fields)
        {
        }

        public class RowFields : RowFieldsBase
        {
            public Int32Field RoleId;
            public StringField RoleName;

            public Int32Field TenantId;
            public RowFields()
                : base("Roles")
            {
                LocalTextPrefix = "Administration.Role";
            }
        }

        public Int32Field TenantIdField
        {
            get { return Fields.TenantId; }
        }
    }
}