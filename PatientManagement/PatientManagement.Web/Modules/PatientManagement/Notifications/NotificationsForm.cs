﻿
using PatientManagement.Administration;

namespace PatientManagement.PatientManagement.Forms
{
    using Serenity;
    using Serenity.ComponentModel;
    using Serenity.Data;
    using System;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.IO;

    [FormScript("PatientManagement.Notifications")]
    [BasedOnRow(typeof(Entities.NotificationsRow))]
    public class NotificationsForm
    {
        public String EntityType { get; set; }
        
        public Int64 EntityId { get; set; }
        public String Text { get; set; }
        public Int32 InsertUserId { get; set; }
        public DateTime InsertDate { get; set; }
    }
}