using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;

namespace HairSalon.Models
{
  public class Client
  {
    private int _clientId;
    private string _clientName;
    private int _employeeId;

    public Client(string clientName, int employeeId, int clientId = 0)
    {
      _clientId = clientId;
      _clientName = clientName;
      _employeeId = employeeId;
    }

    public int GetClientId()
    {
      return _clientId;
    }

    public string GetClientName()
    {
      return _clientName;
    }

    public int GetEmployeeId()
    {
      return _employeeId;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool areNamesEqual = this.GetClientName().Equals(newClient.GetClientName());
        bool areIdsEqual = this.GetClientId().Equals(newClient.GetClientId());
        return (areNamesEqual && areIdsEqual);
      }
    }

    public override int GetHashCode()
    {
      return this.GetClientName().GetHashCode();
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE from clients;";

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (client_name, employee_id) VALUES (@clientName, @employeeId);";

      cmd.Parameters.Add(new MySqlParameter("@clientName", _clientName));
      cmd.Parameters.Add(new MySqlParameter("@employeeId", _employeeId));

      cmd.ExecuteNonQuery();
      _clientId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
