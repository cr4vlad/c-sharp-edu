using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            //boxing/unboxing
            object box = (int)42;
            Console.WriteLine(box.GetType());
            long unbox = (int)box;
            Console.WriteLine(box);
            Console.WriteLine(unbox);
            
            //generics
            Console.WriteLine("Generics:");

            Account<int> account1 = new Account<int> { Sum = 5000 };
            Account<int>.session = 5436;

            Account<string> account2 = new Account<string> { Sum = 4000 };
            Account<string>.session = "45245";

            Console.WriteLine(Account<int>.session);      // 5436
            Console.WriteLine(Account<string>.session);   // 45245

            //static
            Console.WriteLine("Static. Запуск создания обычных пользователей");
            User user1 = new User(); // здесь сработает статический конструктор
            User user2 = new User();

            Console.WriteLine("Запуск создания супер-пользователей");
            //SuperUser superUser1 = new SuperUser(); // здесь сработает статический конструктор
            //SuperUser superUser2 = new SuperUser();
            Console.WriteLine(SuperUser.SuperKey); // или здесь сработает статический конструктор

            Console.WriteLine("Запуск создания админа");
            Admin.DisplayCounter(); // нельзя создать объект для static class, внутри все static only

            //delegates
            Console.WriteLine("Delegates");
            Message mes1 = Hello;
            mes1 += HowAreYou;  // теперь mes1 указывает на два метода
            mes1(); // вызываются оба метода - Hello и HowAreYou

            if (DateTime.Now.Hour < 12)
            {
                Show_Message(GoodMorning);
            }
            else
            {
                Show_Message(GoodEvening);
            }

            //matrix
            Console.WriteLine("Matrix");

            int[,] mas = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 10, 11, 12 } };

            int rows = mas.GetUpperBound(0) + 1; // GetUpperBound(dimension) возвращает индекс последнего элемента в определенной размерности
            int columns = mas.Length / rows;
 
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(mas[i, j] + "\t");
                }
                Console.WriteLine();
            }

            // enum
            Console.WriteLine("Enum");

            Console.WriteLine((int)Times.Night); // не очевидное поведение

            // extension methods
            Console.WriteLine("Extension methods");

            string s = "Привет мир";
            char c = 'и';
            int n = s.WordCount(c);
            Console.WriteLine(n);

            List<int> numbers = new List<int> { 1, 2, 3, 1, 4, 5, 1, 6, 7, 8, 9 };
            Console.WriteLine("Amount of \"1\" in list: " + numbers.CountOf<int>(1));

            Console.ReadKey();
        }

        class Account<T>
        {
            public static T session;

            public T Id { get; set; }
            public int Sum { get; set; }
        }

        class User
        {
            static User()
            {
                Console.WriteLine("Идет процесс создания пользователей...");
            }

            public User()
            {
                Console.WriteLine("Создан пользователь");
            }
        }

        class SuperUser
        {
            public static string SuperKey = "e3Gf7E";
            
            static SuperUser()
            {
                Console.WriteLine("Идет процесс создания пользователей...");
            }

            public SuperUser()
            {
                Console.WriteLine("Создан пользователь");
            }
        }

        static class Admin
        {
            private static int counter = 1;

            static Admin()
            {
                Console.WriteLine("Сработал конструктор админа");
            }
 
            public static void DisplayCounter()
            {
                Console.WriteLine("Создано {0} объектов Admin", counter);
            }
        }

        delegate void Message();
        private static void Hello()
        {
            Console.WriteLine("Hello");
        }
        private static void HowAreYou()
        {
            Console.WriteLine("How are you?");
        }

        delegate void GetMessage();

        private static void Show_Message(GetMessage _del)
        {
            if (_del != null) // ?. in newer versions
                _del.Invoke();
        }
        private static void GoodMorning()
        {
            Console.WriteLine("Good Morning");
        }
        private static void GoodEvening()
        {
            Console.WriteLine("Good Evening");
        }

        enum Times
        {
            Morning,
            Noon,
            Afternoon,
            Evening = 1,
            Night
        }
    }

    public static class StringExtension
    {
        public static int WordCount(this string str, char c)
        {
            int counter = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                    counter++;
            }
            return counter;
        }
    }

    public static class ListEx
    {
        public static int CountOf<T>(this List<T> list, T item)
        {
            int result = 0;
            foreach (T elem in list)
            {
                if (elem.Equals(item))
                    result++;
            }
            return result;
        }
    }
}
