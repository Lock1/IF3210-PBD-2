using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class ScoreBoardDb : MonoBehaviour
{
    private string dbName = "URI=file:Scoreboard.db";
    public GameObject panel;
    public Sprite image;
    public ScrollRect scroll;
    public Font font;


    // Start is called before the first frame update
    void Start()
    {
        // DeleteTable();
        CreateZenTable();
        CreateWaveTable();
        /*InsertZenScore("Test", "20:00");*/
        ShowScoreBoards();
    }

    public void CreateZenTable()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS zen_scoreboards (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255), time VARCHAR(255));";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void CreateWaveTable()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS wave_scoreboards (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(255), num_wave INT, score INT);";
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
            command.CommandText = "SELECT * FROM zen_scoreboards;";

            using IDataReader reader = command.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                // add gameobject
                GameObject rect = new GameObject("scoreboard" + i);

                rect.transform.SetParent(panel.transform);

                rect.AddComponent<Image>();

                rect.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(500, 100);

                rect.GetComponent<Image>().sprite = image;

                rect.GetComponent<Image>().rectTransform.localScale = new Vector3(1, 1, 1);

                addNameObject(rect, i, reader);

                addTimeObject(rect, i, reader);

                // TODO: add text and score

                i++;
            }

            // scrolling to top
            scroll.normalizedPosition = new Vector2(0, 1);

            reader.Close();
        }

        connection.Close();
    }

    public void InsertZenScore(string name, string time)
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO zen_scoreboards (name, time) VALUES ('" + name + "', '" + time + "');";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    public void addNameObject(GameObject rect, int i, IDataReader reader)
    {
        GameObject name = new GameObject("name-" + i);

        name.AddComponent<Text>();

        name.transform.SetParent(rect.transform);

        name.GetComponent<Text>().rectTransform.sizeDelta = new Vector2(286, 53);

        name.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);

        float y = name.GetComponent<Text>().transform.position.y;

        name.GetComponent<Text>().transform.position = new Vector3(-113, y-14, 0);

        name.GetComponent<Text>().text = reader["name"].ToString();
        
        name.GetComponent<Text>().font = font;

        name.GetComponent<Text>().fontSize = 30;
    }

    public void addTimeObject(GameObject rect, int i, IDataReader reader)
    {
        GameObject name = new GameObject("time-" + i);

        name.AddComponent<Text>();

        name.transform.SetParent(rect.transform);

        name.GetComponent<Text>().rectTransform.sizeDelta = new Vector2(120, 53);

        name.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);

        float y = name.GetComponent<Text>().transform.position.y;

        name.GetComponent<Text>().transform.position = new Vector3(218, y-14, 0);

        name.GetComponent<Text>().text = reader["time"].ToString();
        
        name.GetComponent<Text>().font = font;

        name.GetComponent<Text>().fontSize = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
