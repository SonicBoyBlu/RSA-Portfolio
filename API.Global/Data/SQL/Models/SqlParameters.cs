namespace Global.Data.SQL
{
    public class QueryParameters : List<QueryParameter>
    {
    }
    public class QueryParameter
    {
        public required string Name { get; set; }
        public required object Value { get; set; }
    }
}
