using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;
using System.Collections;

namespace HairSalon.Models
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _employeeId;
    public Client(string name, int employeeId, int Id = 0)
    {
      _id = Id;
      _name = name;
      _employeeId = employeeId;
    }
    public string GetClientName()
    {
      return _name;
    }

    public int GetClientId()
    {
      return _id;
    }
    public int GetEmployeeId()
    {
      return _employeeId;
    }

    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = (this.GetClientId() == newClient.GetClientId());
        bool nameEquality = (this.GetClientName() == newClient.GetClientName());
        bool employeeEquality = this.GetEmployeeId() == newClient.GetEmployeeId();
        return (idEquality && nameEquality && employeeEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetClientName().GetHashCode();
    }


    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;"; //select from clients
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientEmployeeId = rdr.GetInt32(2);

        Client newClient = new Client(clientName, clientEmployeeId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, employeeId) VALUES (@ClientName, @EmployeeId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@ClientName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter employeeId = new MySqlParameter();
      employeeId.ParameterName = "@EmployeeId";
      employeeId.Value = this._employeeId;
      cmd.Parameters.Add(employeeId);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `clients` WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int clientId = 0;
      string clientName = "";
      int clientEmployeeId = 0;

      while (rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientEmployeeId = rdr.GetInt32(2);
      }
      Client foundClient = new Client(clientName, clientEmployeeId, clientId);

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE clients";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = new MySqlCommand("DELETE FROM clients WHERE id = @ClientId;", conn);
      MySqlParameter clientIdParameter = new MySqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetClientId();

      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = newName;
      cmd.Parameters.Add(name);


      cmd.ExecuteNonQuery();
      _name = newName;


      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }

  }
}
