using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nielsen_PDF_Creator
{
    public class QueueItem
    {
        private String itemName;
        private String command;

        public QueueItem(String i, String c)
        {
            itemName = i;
            command = c;
        }

        public String getName() { return itemName; }
        public String getCommand() { return command; }
        public void setCommand(String c) { command = c; }

        public override string ToString()
        {
            return itemName;
        }

    }
}
