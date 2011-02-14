﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Composite.Core.IO;
using Composite.Core.Xml;


namespace Composite.Core.WebClient
{
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public enum CompositeScriptMode
    {
        /// <exclude />
        OPERATE = 0,

        /// <exclude />
        DEVELOP = 1,

        /// <exclude />
        COMPILE = 2,
    };


    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public static class ScriptHandler
    {
        private static string _compileScriptsFilename = "CompileScripts.xml";



        /// <exclude />
        public static string MergeScripts(string type, IEnumerable<string> scriptFilenames, string folderPath, string targetPath)
        {
            string sourcesFilename = targetPath + "\\" + type + "-uncompressed.js";

            StringBuilder newLine = new StringBuilder();
            newLine.AppendLine();
            newLine.AppendLine();

            FileUtils.RemoveReadOnly(sourcesFilename);

            C1File.WriteAllText(sourcesFilename, string.Empty /* GetTimestampString() */);

            foreach (string scriptFilename in scriptFilenames)
            {
                string scriptPath = scriptFilename.Replace("${root}", folderPath).Replace("/", "\\");

                string lines = C1File.ReadAllText(scriptPath);

                
                C1File.AppendAllText(sourcesFilename, lines);
                C1File.AppendAllText(sourcesFilename, newLine.ToString());
            }

            return sourcesFilename;
        }



        /// <exclude />
        public static string BuildTopLevelClassNames(IEnumerable<string> scriptFilenames, string folderPath, string targetPath)
        {
            StringBuilder classes = new StringBuilder();

            classes.AppendLine("var topLevelClassNames = [ // Don't edit! This file is automatically generated.");

            bool first = true;
            foreach (string scriptFilename in scriptFilenames)
            {
                string scriptPath = scriptFilename.Replace("${root}", folderPath);
                if (scriptPath.IndexOf("/scripts/source/page/") == -1)
                {
                    if (first == true)
                    {
                        first = false;
                    }
                    else
                    {
                        classes.AppendLine(",");
                    }

                    int _start = scriptPath.LastIndexOf("/") + 1;
                    int _length = scriptPath.LastIndexOf(".js") - _start;

                    string className = scriptPath.Substring(_start, _length);

                    classes.Append("\t\"" + className + "\"");
                }
            }

            classes.AppendLine("];");

            string classesFilename = targetPath + "\\" + "toplevelclassnames.js";

            FileUtils.RemoveReadOnly(classesFilename);

            C1File.WriteAllText(classesFilename, string.Empty /* GetTimestampString() */);
            C1File.AppendAllText(classesFilename, classes.ToString());

            return classesFilename;
        }



        /// <exclude />
        public static IEnumerable<string> GetTopScripts(CompositeScriptMode scriptMode, string folderPath)
        {
            IEnumerable<string> result = GetStrings("top", scriptMode.ToString().ToLower(), folderPath);

            return result;
        }



        /// <exclude />
        public static IEnumerable<string> GetSubScripts(CompositeScriptMode scriptMode, string folderPath)
        {
            IEnumerable<string> result = GetStrings("sub", scriptMode.ToString().ToLower(), folderPath);

            return result;
        }



        private static IEnumerable<string> GetStrings(string type, string mode, string folderPath)
        {
            string filename = Path.Combine(folderPath, _compileScriptsFilename);

            XDocument doc = XDocumentUtils.Load(filename);

            if (mode == "compile") mode = "develop";

            XElement topElement = doc.Root.Elements().Where(f => f.Attribute("name").Value == type).Single();
            XElement modeElement = topElement.Elements().Where(f => f.Attribute("name").Value == mode).Single();

            return
                from e in modeElement.Elements()
                select e.Attribute("filename").Value;
        }



        private static string GetTimestampString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("/*");
            sb.AppendLine(" * Created: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            sb.AppendLine(" */");
            sb.AppendLine();
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
