using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            Scene chair = new Scene("chair");
            Scene table = new Scene("table");
            Scene scene = new Scene("kitchen");

            scene += table - chair;

            Console.WriteLine(scene.Description);

            Console.ReadKey();
        }
    }

    class Scene
    {
        private string description;

        //public string Description => description; // doesn't work in my VS2013
        public string Description
        {
            get { return description; }
            private set { description = value; }
        }

        public Scene(string description)
        {
            Description = description;
        }

        public static Scene operator +(Scene s1, Scene s2)
        {
            return new Scene(s1.Description + " with " + s2.Description);
        }

        public static Scene operator -(Scene s1, Scene s2)
        {
            return new Scene(s1.Description + " without " + s2.Description);
        }
    }
}
