using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Notes
{
    public class Saving
    {
        public void CreateXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("notes");
            xmlDoc.AppendChild(rootNode);

            xmlDoc.Save("SavedNotes.xml");
        }


        public void SaveNote(string header, string note)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("SavedNotes.xml");
            XmlNode rootNode = xmlDoc.SelectSingleNode("notes");

            XmlNode noteNode = xmlDoc.CreateElement("note");
            XmlAttribute attribute = xmlDoc.CreateAttribute("header");
            attribute.Value = header;
            noteNode.Attributes.Append(attribute);
            noteNode.InnerText = note;
            rootNode.AppendChild(noteNode);
            xmlDoc.Save("SavedNotes.xml");
        }
    }
}
