using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace UCGS.RichChat
{
    public class Group
    {
        public Group()
        {
        }
        public Group(string name, string tag, string colorname, string colortag, string colortext)
        {
            Name = name;
            Tag = tag;
            ColorName = colorname;
            ColorTag = colortag;
            ColorText = colortext;
        }
        [XmlAttribute("PeermissionName")]
        public string Name;
        [XmlAttribute()]
        public string Tag;
        [XmlAttribute()]
        public string ColorName;
        [XmlAttribute()]
        public string ColorTag;
        [XmlAttribute()]
        public string ColorText;
    }
}
