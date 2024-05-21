using System.Collections.Generic;

namespace CRUD_using_Ado.Models
{
    public class GetBody
    {
        public List<Table> tables { get; set; }
    }

    public class Table
    {
        public int tbl_id { get; set; }
        public List<Column> columns { get; set; }
        public List<Row> rows { get; set; }
    }

    public class Column
    {
        public string column_name { get; set; }
    }
    public class Row
    {
        public int row_id { get; set; }
        public string[]? Row_Data { get; set; }
    }

}
