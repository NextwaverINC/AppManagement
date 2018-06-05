using System;
using System.Collections.Generic;
using System.Text;

namespace Document
{
    public class Config
    {
        public static string DConfig()
        {
            string XmlData = @"<Configs>
                                  <Config ID='1' Name='Document Type'>
                                    <Item ID='1' Name='String' Value='STR'/>
                                    <Item ID='2' Name='Integer' Value='INT'/>
                                    <Item ID='3' Name='Double' Value='DOB'/>   
                                  </Config>
                                  <Config ID='2' Name='Item Type'>
                                    <Item ID='1' Name='Fix' Value='FIX'/>
                                    <Item ID='2' Name='Sequence' Value='SEQ'/>
                                  </Config>
                                  <Config ID='3' Name='Means Table'>   
                                    <Item ID='Column2' Name='Name'/>
                                    <Item ID='Column3' Name='Type'/>
                                  </Config>
                                </Configs>";
            return XmlData;
        }

        public static string DefaultDocument()
        {
            string XmlData =@"<Document ID='' Name=''>
                                  <Profile>
                                    <Section ID='1' Name='Who'></Section>
                                    <Section ID='2' Name='What'></Section>
                                    <Section ID='3' Name='Where'></Section>
                                    <Section ID='4' Name='When'></Section>
                                    <Section ID='5' Name='Why'></Section>
                                  </Profile>
                                  <Data>    
                                  </Data>
                                </Document>";
            return XmlData;
        }
    }
}
