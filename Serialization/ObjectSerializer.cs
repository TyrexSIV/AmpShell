/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2020 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using Avalonia.Logging;

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

    public static class ObjectSerializer
    {
        public static async Task<object> DeserializeFromFileAsync<T>(string xmlPath, T targetObjectType) where T : Type
        {
            return await Task.Run(() =>
            {
                XmlSerializer deserializer = new XmlSerializer(targetObjectType);
                var reader = new StreamReader(xmlPath, Encoding.Unicode);
                var targetObjectInstance = (T)deserializer.Deserialize(reader);
                reader.Close();
                return targetObjectInstance;
            });
        }

        public static async Task<bool> SerializeToDiskFileAsync<T>(string xmlPath, object objectToSerialize, T typeOfObjectToSerialize) where T : Type
        {
            try
            {
                await Task.Run(() =>
                {
                    XmlSerializer serializer = new XmlSerializer(typeOfObjectToSerialize);
                    var writer = new StreamWriter(xmlPath, false, Encoding.Unicode);
                    serializer.Serialize(writer, objectToSerialize);
                    writer.Close();
                });
            }
            catch (Exception e)
            {
                Logger.Fatal("Serialization", e, "Serialization to file failed", new object[] { e });
                return false;
            }
            return true;
        }

        private static T DeserializeXML<T>(string xmlData) where T : new()
        {
            if (string.IsNullOrEmpty(xmlData))
            {
                return default;
            }

            TextReader tr = new StringReader(xmlData);
            T DocItms = new T();
            XmlSerializer xms = new XmlSerializer(DocItms.GetType());
            DocItms = (T)xms.Deserialize(tr);

            return DocItms == null ? default : DocItms;
        }

        private static string SeralizeObjectToXML<T>(T xmlObject)
        {
            StringBuilder sbTR = new StringBuilder();
            XmlSerializer xmsTR = new XmlSerializer(xmlObject.GetType());
            XmlWriterSettings xwsTR = new XmlWriterSettings();

            XmlWriter xmwTR = XmlWriter.Create(sbTR, xwsTR);
            xmsTR.Serialize(xmwTR, xmlObject);

            return sbTR.ToString();
        }

        public static T CloneObject<T>(T objClone) where T : new()
        {
            string objectToString = SeralizeObjectToXML<T>(objClone);
            return DeserializeXML<T>(objectToString);
        }
    }
}