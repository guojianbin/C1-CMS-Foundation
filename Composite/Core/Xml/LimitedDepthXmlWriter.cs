﻿using System;
using System.Xml;

namespace Composite.Core.Xml
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
	public class LimitedDepthXmlWriter : XmlWriter
	{
        private readonly XmlWriter _innerWriter;
        private readonly int _maxDepth;
        private int _depth;

        /// <exclude />
        public LimitedDepthXmlWriter(XmlWriter innerWriter): this(innerWriter, 100)
        {
        }


        /// <exclude />
        public LimitedDepthXmlWriter(XmlWriter innerWriter, int maxDepth)
        {
            _maxDepth = maxDepth;
            _innerWriter = innerWriter;
        }


        /// <exclude />
        public override void Close()
        {
            _innerWriter.Close();
        }


        /// <exclude />
        public override void Flush()
        {
            _innerWriter.Flush();
        }


        /// <exclude />
        public override string LookupPrefix(string ns)
        {
            return _innerWriter.LookupPrefix(ns);
        }


        /// <exclude />
        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            _innerWriter.WriteBase64(buffer, index, count);
        }


        /// <exclude />
        public override void WriteCData(string text)
        {
            _innerWriter.WriteCData(text);
        }


        /// <exclude />
        public override void WriteCharEntity(char ch)
        {
            _innerWriter.WriteCharEntity(ch);
        }


        /// <exclude />
        public override void WriteChars(char[] buffer, int index, int count)
        {
            _innerWriter.WriteChars(buffer, index, count);
        }


        /// <exclude />
        public override void WriteComment(string text)
        {
            _innerWriter.WriteComment(text);
        }


        /// <exclude />
        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
            _innerWriter.WriteDocType(name, pubid, sysid, subset);
        }


        /// <exclude />
        public override void WriteEndAttribute()
        {
            _innerWriter.WriteEndAttribute();
        }


        /// <exclude />
        public override void WriteEndDocument()
        {
            _innerWriter.WriteEndDocument();
        }


        /// <exclude />
        public override void WriteEndElement()
        {
            _depth--;

            _innerWriter.WriteEndElement();
        }


        /// <exclude />
        public override void WriteEntityRef(string name)
        {
            _innerWriter.WriteEntityRef(name);
        }


        /// <exclude />
        public override void WriteFullEndElement()
        {
            _innerWriter.WriteFullEndElement();
        }


        /// <exclude />
        public override void WriteProcessingInstruction(string name, string text)
        {
            _innerWriter.WriteProcessingInstruction(name, text);
        }


        /// <exclude />
        public override void WriteRaw(string data)
        {
            _innerWriter.WriteRaw(data);
        }


        /// <exclude />
        public override void WriteRaw(char[] buffer, int index, int count)
        {
            _innerWriter.WriteRaw(buffer, index, count);
        }


        /// <exclude />
        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            _innerWriter.WriteStartAttribute(prefix, localName, ns);
        }


        /// <exclude />
        public override void WriteStartDocument(bool standalone)
        {
            _innerWriter.WriteStartDocument(standalone);
        }


        /// <exclude />
        public override void WriteStartDocument()
        {
            _innerWriter.WriteStartDocument();
        }


        /// <exclude />
        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            if (_depth++ > _maxDepth) ThrowException();

            _innerWriter.WriteStartElement(prefix, localName, ns);
        }


        /// <exclude />
        public override WriteState WriteState
        {
            get { return _innerWriter.WriteState; }
        }


        /// <exclude />
        public override void WriteString(string text)
        {
            _innerWriter.WriteString(text);
        }


        /// <exclude />
        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            _innerWriter.WriteSurrogateCharEntity(lowChar, highChar);
        }


        /// <exclude />
        public override void WriteWhitespace(string ws)
        {
            _innerWriter.WriteWhitespace(ws);
        }


        private void ThrowException()
        {
            throw new InvalidOperationException(string.Format("Result xml has more the {0} nested tags. It is possible that xslt transformation contains an endless recursive call.", _maxDepth));
        }
    }
}
