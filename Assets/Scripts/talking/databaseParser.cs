using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Database parser is the translator part.
/// This is where rows and columns are translated into sections.
/// Reply are replies.
/// Conversation are conversation segments.
/// Data is stored in convo list as a chat class
/// </summary>
public class databaseParser
{
    public  ButtonStart buttonstart;
    public List<chat> convo = new List<chat>();

    public void ParseDataFromDB()
    {
        //Takes in a csv file and splits it up by newline for rows and comma for columns.
        // Parse that data into different fields and save that to the chat class.
        TextAsset ConversationData = Resources.Load<TextAsset>("Porper Organization field");
        string[] row = ConversationData.text.Split(new char[] {'\n'});

        // Skip first two rows, used as headers
        for (int i = 2; i < row.Length - 1; i++)
        {
            string[] column = row[i].Split(new char[ ]{','});
            if (row[i] != "")
            {
                chat qChat = new chat();
                int.TryParse(column[0], out qChat.ID);
                qChat.Conversation = column[1];
                qChat.Reply1 = column[2];
                int.TryParse(column[3], out qChat.Replylink1);
                qChat.Reply2 = column[4];
                int.TryParse(column[5], out qChat.Replylink2);
                qChat.Reply3 = column[6];
                int.TryParse(column[7], out qChat.Replylink3);
                qChat.Reply4 = column[8];
                int.TryParse(column[9], out qChat.Replylink4);
                
                convo.Add(qChat);
            }
        }
    }

}
