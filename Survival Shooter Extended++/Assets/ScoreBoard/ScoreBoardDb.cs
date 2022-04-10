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
    public string type;


    // Start is called before the first frame update
    void Start()
    {

        if (type == "Zen")
            ShowScoreBoards();
        else
            ShowScoreBoards2();
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

                i++;
            }

            // scrolling to top
            scroll.normalizedPosition = new Vector2(0, 1);

            reader.Close();
        }

        connection.Close();
    }

    public void ShowScoreBoards2()
    {
        using var connection = new SqliteConnection(dbName);
        connection.Open();

        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM wave_scoreboards;";

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
                addWaveObject(rect, i, reader);
                addScoreObject(rect, i, reader);

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

        name.GetComponent<Text>().transform.position = new Vector3(-37, y-14, 60);

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

        name.GetComponent<Text>().transform.position = new Vector3(100, y-14, 0);

        name.GetComponent<Text>().text = reader["time"].ToString();
        
        name.GetComponent<Text>().font = font;

        name.GetComponent<Text>().fontSize = 30;
    }

    public void addWaveObject(GameObject rect, int i, IDataReader reader)
    {
        GameObject name = new GameObject("num_wave-" + i);

        name.AddComponent<Text>();

        name.transform.SetParent(rect.transform);

        name.GetComponent<Text>().rectTransform.sizeDelta = new Vector2(120, 53);

        name.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);

        float y = name.GetComponent<Text>().transform.position.y;

        name.GetComponent<Text>().transform.position = new Vector3(70, y-14, 0);

        name.GetComponent<Text>().text = reader["num_wave"].ToString();
        
        name.GetComponent<Text>().font = font;

        name.GetComponent<Text>().fontSize = 30;
    }

    public void addScoreObject(GameObject rect, int i, IDataReader reader)
    {
        GameObject name = new GameObject("score-" + i);

        name.AddComponent<Text>();

        name.transform.SetParent(rect.transform);

        name.GetComponent<Text>().rectTransform.sizeDelta = new Vector2(120, 53);

        name.GetComponent<Text>().rectTransform.localScale = new Vector3(1, 1, 1);

        float y = name.GetComponent<Text>().transform.position.y;

        name.GetComponent<Text>().transform.position = new Vector3(100, y-14, 0);

        name.GetComponent<Text>().text = reader["score"].ToString();
        
        name.GetComponent<Text>().font = font;

        name.GetComponent<Text>().fontSize = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
