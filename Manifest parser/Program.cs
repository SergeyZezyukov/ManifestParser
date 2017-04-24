using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Manifest_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.IO.Directory.GetCurrentDirectory();
            var json = File.ReadAllText(path + @"\Manifest");
            var sourse = JsonConvert.DeserializeObject<RootObject>(json);


            var dest = new RootObject();
            dest.Packages = sourse.Packages.Where(i => i.Name.Contains("_SalesUp_")).ToList();

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            jsonSettings.DefaultValueHandling = DefaultValueHandling.Ignore;

            var resultJSON = JsonConvert.SerializeObject(dest, jsonSettings);
            File.WriteAllText(path + @"\Manifest", resultJSON);
        }
    }

    public class DependsOn
    {
        public string UId { get; set; }
        public string PackageVersion { get; set; }
        public string Name { get; set; }
    }

    public class Parent
    {
        public string UId { get; set; }
        public string Name { get; set; }
    }

    public class Schema
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedOnUtc { get; set; }
        public Parent Parent { get; set; }
        public bool? ExtendParent { get; set; }
        public string ManagerName { get; set; }
        public string Caption { get; set; }
    }

    public class Schema2
    {
        public string UId { get; set; }
        public string Name { get; set; }
    }

    public class Column
    {
        public string ColumnUId { get; set; }
        public bool IsKey { get; set; }
        public string ColumnName { get; set; }
        public string DataTypeValueUId { get; set; }
        public bool? IsForceUpdate { get; set; }
    }

    public class Datum
    {
        public string UId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedOnUtc { get; set; }
        public int InstallType { get; set; }
        public Schema2 Schema { get; set; }
        public List<Column> Columns { get; set; }
    }

    public class Package
    {
        public string UId { get; set; }
        public string PackageVersion { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedOnUtc { get; set; }
        public string Maintainer { get; set; }
        public List<DependsOn> DependsOn { get; set; }
        public List<Schema> Schemas { get; set; }
        public List<object> Assemblies { get; set; }
        public List<object> SqlScripts { get; set; }
        public List<Datum> Data { get; set; }
    }

    public class RootObject
    {
        public List<Package> Packages { get; set; }
    }

}
