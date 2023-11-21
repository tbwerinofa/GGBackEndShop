using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ProductWebAPI.Services
{
        public static class DbUpdateExceptionExtensions
        {
            public static Dictionary<bool, string> GetValidateEntityResults(this Dictionary<bool, string> dictionary, string message)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    dictionary.Add(false, message);
                }
                return dictionary;
            }

            public static int GetSqlerrorNo(this DbUpdateException dbUpdateEx) //It returns sqlException Number
            {

                if (dbUpdateEx != null)
                {
                    if (dbUpdateEx != null
                            && dbUpdateEx.InnerException != null)
                    {
                        SqlException sqlException = dbUpdateEx.InnerException as SqlException;
                        if (sqlException != null)
                        {
                            return sqlException.Number;
                        }

                        return 0;
                    }
                }
                return 0;
            }
        }
    public enum SqlErrNo
    {
        UQ = 2627,//Unique Key
        FK = 547,// Foreign Key
        DK = 2601  // Duplicated key row error
    };

    public static class ConstEntity
    {
        public const string UniqueKeyMsg = "Record  details already exist.";
        public const string ForeignKeyDelMsg = "This Record  can not be deleted because it is mapped to another record !!";
        public const string MissingValueMsg = "This Record can not be saved because there is a missing value !!";

        public const string ArgumentNullException = "Entity  does not exist";

    }
}
