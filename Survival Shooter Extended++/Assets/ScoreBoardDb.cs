using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class ScoreBoardDb : MonoBehaviour
{
    private string dbName = "URI=file:Scoreboard.db";

    // Start is called before the first frame update
    void Start()
    {
        // DeleteTable();
        CreateTable();
        InsertScore("Test", 20);
        ShowScoreBoards();
    }

    public void CreateTable()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS scoreboards (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255), score INT);";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void DeleteTable()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "DROP TABLE IF EXISTS scoreboards;";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void ShowScoreBoards()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM scoreboards;";

            using IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Debug.Log("Name: " + reader["name"] + "\t Score" + reader["score"]);
            }

            reader.Close();
        }

        connection.Close();
    }

    public void InsertScore(string name, int score)
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO scoreboards (name, score) VALUES ('" + name + "', '" + score + "');";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
