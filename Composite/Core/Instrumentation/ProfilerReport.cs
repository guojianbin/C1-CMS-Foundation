﻿using System;
using System.Linq;
using System.Xml.Linq;


namespace Composite.Core.Instrumentation
{
    /// <summary>    
    /// </summary>
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public static class ProfilerReport
    {
        /// <exclude />
        public static XElement BuildReportXml(Measurement measurement)
        {
            int index = 0;

            var result = new XElement("Measurements");
            foreach (var node in measurement.Nodes)
            {
                result.Add(BuildReportXmlRec(node, node.TotalTime, node.TotalTime, false, index.ToString()));
                index++;
            }

            foreach (var node in measurement.ParallelNodes)
            {
                result.Add(BuildReportXmlRec(node, node.TotalTime, node.TotalTime, true, index.ToString()));
                index++;
            }

            return result;
        }

        private static XElement BuildReportXmlRec(Measurement measurement, /* int level,*/ long totalTime, long parentTime, bool parallel, string id)
        {
            long persentTotal = (measurement.TotalTime * 100) / totalTime;

            long ownTime = measurement.TotalTime - measurement.Nodes.Select(childNode => childNode.TotalTime).Sum();

            var result = new XElement("Measurement",
                                      new XAttribute("_id", id),
                                      new XAttribute("title", measurement.Name),
                                      new XAttribute("totalTime", measurement.TotalTime),
                                      new XAttribute("ownTime", ownTime),
                                      new XAttribute("persentFromTotal", persentTotal),
                                      new XAttribute("parallel", parallel.ToString().ToLowerInvariant()));

            int index = 0;
            foreach (var childNode in measurement.Nodes.OrderByDescending(c => c.TotalTime))
            {
                result.Add(BuildReportXmlRec(childNode, totalTime, measurement.TotalTime, false, (id + "|" + index)));
                index++;
            }

            foreach (var childNode in measurement.ParallelNodes.OrderByDescending(c => c.TotalTime))
            {
                result.Add(BuildReportXmlRec(childNode, totalTime, measurement.TotalTime, true, (id + "|" + index)));
                index++;
            }  

            return result;
        }
    }
}
