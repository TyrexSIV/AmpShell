﻿/*AmpShell : .NET front-end for DOSBox
 * Copyright (C) 2009, 2019 Maximilien Noal
 *This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
 * without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.
 * If not, see <http://www.gnu.org/licenses/>.*/

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace AmpShell.Model.Serialization
{
    public static class ObjectSerializer
    {
        public static object Deserialize<T>(string xmlPath, T targetObjectType) where T : Type
        {
            XmlSerializer deserializer = new XmlSerializer(targetObjectType);
            var reader = new StreamReader(xmlPath, Encoding.Unicode);
            var targetObjectInstance = deserializer.Deserialize(reader);
            reader.Close();
            return targetObjectInstance;
        }

        public static void Serialize<T>(string xmlPath, object objectToSerialize, T typeOfObjectToSerialize) where T : Type
        {
            XmlSerializer serializer = new XmlSerializer(typeOfObjectToSerialize);
            var writer = new StreamWriter(xmlPath, false, Encoding.Unicode);
            serializer.Serialize(writer, objectToSerialize);
            writer.Close();
        }
    }
}