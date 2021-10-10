using System.Data.Common;
using System.Data.SqlClient;

namespace VeraDemoNet.Commands
{
    public class IgnoreCommand : BlabberCommandBase,IBlabberCommand
    {
        private readonly string username;

        public IgnoreCommand(DbConnection connect, string username)
        {
            this.connect = connect;
            this.username = username;
        }

        public void Execute(string blabberUsername) {
            var sqlQuery = $"DELETE FROM listeners WHERE blabber={blabberUsername} AND listener={username}";


            logger.Info(sqlQuery);

            var action = connect.CreateCommand();
            action.CommandText = "DELETE FROM listeners WHERE blabber=@blabber AND listener=@username";
		    action.Parameters.Add(new SqlParameter{ParameterName = "@blabber", Value = blabberUsername});
            action.Parameters.Add(new SqlParameter{ParameterName = "@username", Value = username});
            action.ExecuteNonQuery();
					
            sqlQuery = "SELECT blab_name FROM users WHERE username = '" + blabberUsername + "'";
            
            var sqlStatement = connect.CreateCommand();
            sqlStatement.CommandText = "SELECT blab_name FROM users WHERE username = @blabberUsername";
            logger.Info(sqlQuery);
            sqlStatement.Parameters.Add(new SqlParameter("@blabberUsername", blabberUsername));

            var blabName = sqlStatement.ExecuteScalar();
		
            /* START BAD CODE */
            var ignoringEvent = username + " is now ignoring " + blabberUsername + "(" + blabName + ")";
            sqlQuery = "INSERT INTO users_history (blabber, event) VALUES (\"" + username + "\", \"" + ignoringEvent + "\")";

            sqlStatement.CommandText = "INSERT INTO users_history (blabber, event) VALUES (@username, @ignoringEvent)";
            sqlStatement.Parameters.Add(new SqlParameter("@username", username));
            sqlStatement.Parameters.Add(new SqlParameter("@ignoringEvent", ignoringEvent));

            /* END BAD CODE */

            logger.Info(sqlQuery);
            sqlStatement.ExecuteNonQuery();
        }
    }
}