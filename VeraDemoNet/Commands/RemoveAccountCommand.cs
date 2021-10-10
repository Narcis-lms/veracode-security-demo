using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace VeraDemoNet.Commands
{
    public class RemoveAccountCommand : BlabberCommandBase,IBlabberCommand
    {
        public RemoveAccountCommand(DbConnection connect, string username) {
            this.connect = connect;
        }

        public void Execute(string blabberUsername)
        {
            var sqlQuery = "DELETE FROM listeners WHERE blabber=@Username OR listener=@Username;";
            logger.Info(sqlQuery);
            DbCommand action;
            action = connect.CreateCommand();
            action.CommandText = sqlQuery;

            var parameter = action.CreateParameter();
            parameter.ParameterName = "@Username";
            parameter.Value = blabberUsername;
            action.Parameters.Add(parameter);

            action.ExecuteNonQuery();

            sqlQuery = "SELECT blab_name FROM users WHERE username = '" + blabberUsername +"'";
            var sqlStatement = connect.CreateCommand();
            sqlStatement.CommandText = sqlQuery;
            logger.Info(sqlQuery);
            var result = sqlStatement.ExecuteScalar();
		
            /* START BAD CODE */
            var removeEvent = "Removed account for blabber " + result;
            sqlQuery = "INSERT INTO users_history (blabber, event) VALUES ('" + blabberUsername + "', '" + removeEvent + "')";
            logger.Info(sqlQuery);
            sqlStatement.CommandText = "INSERT INTO users_history (blabber, event) VALUES (@blabberUsername ,@removeEvent)";
            sqlStatement.Parameters.Add(new SqlParameter("@blabberUsername", blabberUsername));
            sqlStatement.Parameters.Add(new SqlParameter("@removeEvent", removeEvent));
            sqlStatement.ExecuteNonQuery();
		
            sqlQuery = "DELETE FROM users WHERE username = '" + blabberUsername + "'";
            logger.Info(sqlQuery);
            sqlStatement.CommandText = "DELETE FROM users WHERE username = @blabberUsername";
            sqlStatement.Parameters.Add(new SqlParameter("@blabberUsername", blabberUsername));
            sqlStatement.ExecuteNonQuery();
            /* END BAD CODE */
        }
    }
}