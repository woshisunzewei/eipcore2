using EIP.Common.Entities.Dtos;

namespace EIP.System.Models.Dtos.Config
{
    /// <summary>
    ///     列
    /// </summary>
    public class SystemDataBaseColumnDoubleWay : IdInput, IDoubleWayDto
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string OrdinalPosition { get; set; }
        public string DefaultSetting { get; set; }
        public string IsNullable { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }
        public string DatePrecision { get; set; }
        public int IsIdentity { get; set; }
        public int IsComputed { get; set; }
        public string ColumnDescription { get; set; }
    }
}