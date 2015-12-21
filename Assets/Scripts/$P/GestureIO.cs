using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using PDollarGestureRecognizer;

namespace PDollarGestureRecognizer
{
    public class GestureIO
    {
        /// <summary>
        /// Reads a multistroke gesture from an XML file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Gesture ReadGesture(string fileName)
        {
			//List<Gesture> gestures = new List<Gesture> ();
            List<Point> points = new List<Point>();
			//List<TextAsset> gestures = new List<TextAsset> ();

            //XmlTextReader xmlReader = null;
            int currentStrokeIndex = -1;
            string gestureName = "";
            try
            {
				//Debug.Log (fileName);

				//xmlReader = new XmlTextReader(File.OpenText(fileName));
				TextAsset xmlFile = (TextAsset)Resources.Load ("Gestures/"+fileName) as TextAsset;
				XmlDocument xml = new XmlDocument();
				xml.LoadXml (xmlFile.text);
				XmlNodeList gestureNodes = xml.GetElementsByTagName("Gesture");
				//var num = 0;
				//Document trial
				foreach(XmlNode gestureInfo in gestureNodes)
				{
					//currentStrokeIndex = -1;
					//Debug.Log (gestureInfo.Attributes["Name"].Value);
					gestureName = gestureInfo.Attributes["Name"].Value;
					if (gestureName.Contains("~")) // '~' character is specific to the naming convention of the MMG set
						gestureName = gestureName.Substring(0, gestureName.LastIndexOf('~'));
					if (gestureName.Contains("_")) // '_' character is specific to the naming convention of the MMG set
						gestureName = gestureName.Replace('_', ' ');

					XmlNodeList strokeNodes = gestureInfo.ChildNodes;
					foreach (XmlNode strokeInfo in strokeNodes){
						//Debug.Log (currentStrokeIndex);
						currentStrokeIndex++;

						XmlNodeList pointNodes = strokeInfo.ChildNodes;
						foreach(XmlNode pointInfo in pointNodes){
							//Debug.Log (pointInfo.Attributes["T"].Value);
							points.Add(new Point(
								float.Parse (pointInfo.Attributes["X"].Value),
								float.Parse (pointInfo.Attributes["Y"].Value),
								currentStrokeIndex
								));
							//num+=1;
						}
						//Debug.Log (gestureInfo.Attributes["Name"].Value+": "+num+": "+points.Count);
					}
					//gestures.Add (new Gesture(points.ToArray (), gestureName));
				}
				//Reader, not working
                /*while (xmlReader.Read())
                {
                    if (xmlReader.NodeType != XmlNodeType.Element) continue;
                    switch (xmlReader.Name)
                    {
                        case "Gesture":
                            gestureName = xmlReader["Name"];
                            if (gestureName.Contains("~")) // '~' character is specific to the naming convention of the MMG set
                                gestureName = gestureName.Substring(0, gestureName.LastIndexOf('~'));
                            if (gestureName.Contains("_")) // '_' character is specific to the naming convention of the MMG set
                                gestureName = gestureName.Replace('_', ' ');
                            break;
                        case "Stroke":
                            currentStrokeIndex++;
                            break;
                        case "Point":
                            points.Add(new Point(
                                float.Parse(xmlReader["X"]),
                                float.Parse(xmlReader["Y"]),
                                currentStrokeIndex
                            ));
                            break;
                    }
                }*/
            }
            finally
            {
            //    if (xmlReader != null) //XmlReader
            //        xmlReader.Close(); //XmlReader
            }
			//Debug.Log ("GestureIO End: " + points.Count);
			//var returner = new Gesture(points.ToArray(), gestureName);
			var pointArray = points.ToArray ();
			//Debug.Log ("Returner " + returner.Points.Length+"  pA: "+pointArray.Length);
            return new Gesture(pointArray, gestureName);
			//return gestures;
        }

        /// <summary>
        /// Writes a multistroke gesture to an XML file
        /// </summary>
        public static void WriteGesture(Point[] points, string gestureName, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                sw.WriteLine("<Gesture Name = \"{0}\">", gestureName);
                int currentStroke = -1;
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].StrokeID != currentStroke)
                    {
                        if (i > 0)
                            sw.WriteLine("\t</Stroke>");
                        sw.WriteLine("\t<Stroke>");
                        currentStroke = points[i].StrokeID;
                    }

                    sw.WriteLine("\t\t<Point X = \"{0}\" Y = \"{1}\" T = \"0\" Pressure = \"0\" />",
                        points[i].X, points[i].Y
                    );
                }
                sw.WriteLine("\t</Stroke>");
                sw.WriteLine("</Gesture>");
            }
        }
    }
}