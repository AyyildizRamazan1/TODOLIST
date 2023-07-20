using System;
using System.Drawing.Printing;

namespace TODOLIST
{
    internal class printDocument1
    {
        public printDocument1()
        {
        }

        public System.Action<object, object> PrintPage { get; internal set; }

        public static implicit operator PrintDocument(printDocument1 v)
        {
            throw new NotImplementedException();
        }
    }
}