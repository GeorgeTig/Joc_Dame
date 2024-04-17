using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Joc_Dame.Model
{
    public enum EPiece
    {
        [XmlEnum(Name = "Empty")]
        Empty,
        [XmlEnum(Name = "RedSoldier")]
        RedSoldier,
        [XmlEnum(Name = "WhiteSoldier")]
        WhiteSoldier,
        [XmlEnum(Name = "RedKing")]
        RedKing,
        [XmlEnum(Name = "WhiteKing")]
        WhiteKing
    }
}
