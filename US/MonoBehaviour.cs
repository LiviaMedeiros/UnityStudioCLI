﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Unity_Studio
{
    class MonoBehaviour
    {
        public string serializedText;

        public MonoBehaviour(AssetPreloadData preloadData, bool readSwitch)
        {
            var sourceFile = preloadData.sourceFile;
            var a_Stream = preloadData.sourceFile.a_Stream;
            a_Stream.Position = preloadData.Offset;

            var m_GameObject = sourceFile.ReadPPtr();
            var m_Enabled = a_Stream.ReadByte();
            a_Stream.AlignStream(4);
            var m_Script = sourceFile.ReadPPtr();
            var m_Name = a_Stream.ReadAlignedString(a_Stream.ReadInt32());
            if (readSwitch)
            {
                if ((serializedText = preloadData.ViewStruct()) == null)
                {
                    var str = "PPtr<GameObject> m_GameObject\n";
                    str += "\tint m_FileID = " + m_GameObject.m_FileID + "\n";
                    str += "\tint64 m_PathID = " + m_GameObject.m_PathID + "\n";
                    str += "UInt8 m_Enabled = " + m_Enabled + "\n";
                    str += "PPtr<MonoScript> m_Script\n";
                    str += "\tint m_FileID = " + m_Script.m_FileID + "\n";
                    str += "\tint64 m_PathID = " + m_Script.m_PathID + "\n";
                    str += "string m_Name = \"" + m_Name + "\"\n";
                    serializedText = str;
                }
            }
            else
            {
                preloadData.extension = ".snaa";
                preloadData.Text = m_Name;
            }
        }
    }
}
