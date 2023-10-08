using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefLectionListOfList
{
    public class Topics
    {
        public string Title {  get; set; }

        public List<Topic> TopicList {  get; set; }

        public Topics(string title , List<Topic> topic) 
        {
            Title = title;
            TopicList = topic;
        }
    }
}
