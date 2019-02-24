﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Witnessing.Client.DataModel;
//
//    var members = Members.FromJson(jsonString);

namespace Witnessing.Client.DataModel
{
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Members
    {
        [JsonProperty("witnessing_members", Required = Required.Always)]
        public WitnessingMember[] WitnessingMembers { get; set; }

        public static Members FromJson(string json) => JsonConvert.DeserializeObject<Members>(json, Witnessing.Client.DataModel.Converter.Settings);
    }
    
}
