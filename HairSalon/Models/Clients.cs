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
      string AllHash = this.GetClientName() + this.GetHashCode();
      return AllHash.GetHashCode();
    }

    public static void DeleteAll()
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

    public static List<Client> GetAll()
    {
      List<Client> allClient = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int clientid = rdr.GetInt32(0);
        string clientname = rdr.GetString(1);
        Client newClient = new Client(clientname, clientid);
        allClient.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClient;
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

    public static Client Find(int employeeId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM employees WHERE client_Id = (@clientId);";

      MySqlParameter searchflightId = new MySqlParameter();
       searchflightId.ParameterName = "@searchflightId";
       searchflightId.Value = employeeId;
       cmd.Parameters.Add(searchflightId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientid = 0;
      string clientname = "";

      while (rdr.Read())
      {
        clientid = rdr.GetInt32(0);
        clientname = rdr.GetString(1);
      }
      Client foundClient = new Client(clientname, clientid);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }


  }
}
