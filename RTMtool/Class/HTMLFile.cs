using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RTMtool.Class
{
    class HTMLFile
    {
        private String[] listPlaceHolder;

        public String[] ListPlaceHolder
        {
            get { return listPlaceHolder; }
            set { listPlaceHolder = value; }
        }
        private String path;

        public String Path
        {
            get { return path; }
            set { path = value; }
        }
        private HtmlAgilityPack.HtmlDocument htmldoc;

        public HtmlAgilityPack.HtmlDocument Htmldoc
        {
            get { return htmldoc; }
            set { htmldoc = value; }
        }
        private List<HtmlNode> nodeList;

        public List<HtmlNode> NodeList
        {
            get { return nodeList; }
            set { nodeList = value; }
        }

        public HTMLFile(String path)
        {
            this.path = path;
            this.loadFile(path);
            this.NodeList= new List<HtmlNode>();
            /*var element = this.htmldoc.GetElementbyId("image-1");
            int i = 0;
            System.Console.Write("image-1 attributes: ");
            while(i<element.Attributes.Count)
            {
                System.Console.Write(element.Attributes.ElementAt(i).Name + "=" + element.Attributes.ElementAt(i).Value);
                i++;
            }*/
        }
        public void loadFile(String path)
        {
            this.htmldoc = new HtmlAgilityPack.HtmlDocument();
            this.htmldoc.Load(this.path);
        }
        public Boolean checkID(String id)
        {
            var test = this.htmldoc.GetElementbyId(id);
            if (test != null)
                return true;
            return false;
        }
        public void findPlaceHolder()
        {
            try
            {
                this.htmldoc.OptionFixNestedTags = true;
                this.htmldoc.Load(this.path);
                var test = this.htmldoc.GetElementbyId("header-1");
                test.InnerHtml = "changed text";
                this.htmldoc.Save("C:/Users/####/#####/GeneratedFile.html");
            }
            catch(Exception exception)
            {
                System.Console.Write(exception.StackTrace);
            }
        }
        public void updateNode(int nodeID, String PlaceID, String value, String separator)
        {
            HtmlNode node = this.NodeList.ElementAt(nodeID);
            String[] nodeTagTab = node.XPath.Split('/');
            String nodeTag = nodeTagTab[nodeTagTab.Length - 1];
            Regex linkRegex = new Regex(@"^a[[0-9]+]$");
            Regex imgRegex = new Regex(@"^img[[0-9]+]$");
            Regex spanRegex = new Regex(@"^span[[0-9]+]$");
            Regex bRegex = new Regex(@"^b[[0-9]+]$");
            if(spanRegex.Match(nodeTag).ToString().Contains("span") || bRegex.Match(nodeTag).ToString().Contains("b["))
            {
                String[] splitBR = value.Split('\n');
                int i = 0;
                int j = 0;
                String newValue = "";
                String linkTagsBegining = "<a href=\"";
                String linkTagsHref = "\" target=\"_blank\"><b><span style=\"color:#989897;text-decoration:none;text-underline:none\">";
                String linkTagsEnd = "</span></b></a>";
                String[] splitLink = null;
                String[] splitText = null;
                String line = "";
                String text="";
                while (i < splitBR.Length)
                {
                    line = "";
                    j = 0;
                    if(splitBR[i].Contains("^"))
                    {
                        splitLink = splitBR[i].Split('^');
                        while(j<splitLink.Length)
                        {
                            text = "";
                            if(splitLink[j].Contains("%"))
                            {
                                splitText = splitLink[j].Split('%');
                                text = linkTagsBegining + splitText[0] + linkTagsHref + splitText[1] + linkTagsEnd;
                            }
                            else
                            {
                                text = splitLink[j];
                            }
                            line += text;
                            j++;
                        }
                    }
                    else
                    {
                        line = splitBR[i];
                    }
                    if(i>0)
                    {
                        line = "<br>" + line;
                    }
                    newValue += line;
                    i++;
                }
                node.InnerHtml = newValue;
            }
            else if (linkRegex.Match(nodeTag)!=null)
            {
                int i = 0;
                while (i < node.Attributes.Count)
                {
                    if (node.Attributes.ElementAt(i).Name == "href")
                    {
                        String[] splitStr = value.Split('%');
                        //System.Console.Write("\n split1: " + splitStr[0] + "split2: " + splitStr[1]);
                        //this.NodeList.ElementAt(nodeID).InnerHtml = value.Split(separator.ToCharArray())[0];
                        if (node.ChildNodes == null)
                        {
                            node.InnerHtml = splitStr[0];
                        }
                        else if (node.ChildNodes != null && node.ChildNodes.Count == 1)
                        {
                            if (node.ChildNodes.ElementAt(0).ChildNodes.ElementAt(0).NodeType.ToString().Equals("Text") && splitStr[0]!="")
                                node.ChildNodes.ElementAt(0).InnerHtml = splitStr[0];
                        }
                        if(splitStr.Length>1)
                            node.Attributes.ElementAt(i).Value = value.Split(separator.ToCharArray())[1];
                        break;
                    }
                    i++;
                }
            }
            else if (imgRegex.Match(nodeTag)!=null)
            {
                int i = 0;
                while(i<node.Attributes.Count)
                {
                    if(node.Attributes[i].Name=="src")
                    {
                        node.Attributes[i].Value = value;
                        break;
                    }
                    i++;
                }
            }
        }
    }
}
