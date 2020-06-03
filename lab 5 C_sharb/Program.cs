using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5_C_sharb
{
    //Многоадресный и одноадресный делегат;
    delegate void Multicast();
    delegate void Unicast();

    class Program
    {
        //Свойства класса, принимающие изначальные значения;
        private int Flat_1 { get; set; } = 12;
        private int House_1 { get; set; } = 10;
        private string Street_1 { get; set; } = "Shevchenko";
        private string City_1 { get; set; } = "Odesa";

        //Конструктор с параметрами для переопределения значений если пользователь захочет их изменить;
        public Program(int New_Flat, int New_House, string New_Street, string New_City)
        {
            Flat_1 = New_Flat;
            House_1 = New_House;
            Street_1 = New_Street;
            City_1 = New_City;
        }
        public Program()
        { }

        //Делегат принимает адрес функции, вызываем ее;
        static void Main(string[] args)
        {
            Program program = new Program();
            Unicast one_for_all = program.Input;
            one_for_all();
        }
        //Конструктора класса Inform принимают значения полей и записывают их в свойства;
        public void Input()
        {
            Inform inform = new Inform(Flat_1, House_1);
            Inform inform_1 = new Inform(Street_1, City_1);
            More(inform, inform_1);
        }
        //Inform является дочерним классом от двух интерфейсов - вызываем функции этих интерфейсов при помощи многоадресного делегата;
        static void More(IFlat flat, IStreet street)
        {
            Multicast all_in_one = flat.Write_Flat;
            all_in_one += street.Write_Street;
            all_in_one();
        }
    }
    class Inform : IFlat, IStreet
    {
        private int Flat { get; set; }
        private int House { get; set; }
        private string Street { get; set; }
        private string City { get; set; }

        public Inform(int flat, int house)
        {
            Flat = flat;
            House = house;
        }
        public Inform(string street, string city)
        {
            Street = street;
            City = city;
        }
        public Inform()
        { }

        //Функции интерфейсов выводят данные о месте проживания;
        void IFlat.Write_Flat()
        {
            Console.WriteLine($"\nДанные о месте проживания:\nКвартира: " + Flat);
            Console.WriteLine($"Дом: " + House);
        }
        void IStreet.Write_Street()
        {
            Console.WriteLine($"Улица: " + Street);
            Console.WriteLine($"Город: " + City + "\n");
            Console.Write("Хотите изменить данные? Введите 1 - да, другое число - нет: ");

            //Если пользователь хочет изменить данные - вызываем функцию More_1, которая при помощи делегата вызовет функцию Re_Write класса Re_Write;
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                ReWrite write = new ReWrite();
                More_1(write);
            }
            else
            {
                Console.WriteLine("Вы решили не изменять\n");
                Console.ReadLine();
            }
        }
        static void More_1(IReWrite write)
        {
            Unicast one_for_all = write.Re_Write;
            one_for_all();
        }
    }

    interface IFlat
    {
        void Write_Flat();
    }
    interface IStreet
    {
        void Write_Street();
    }

    class ReWrite : IReWrite
    {
        private int New_Flat { get; set; }
        private int New_House { get; set; }
        private string New_Street { get; set; }
        private string New_City { get; set; }

        public ReWrite()
        { }

        //Вводим новые данные и при помощи конструктора класса Program записываем их в свойства, вызываем функцию Input класса Program;
        void IReWrite.Re_Write()
        {
            Console.Write("\nВведите новый номер квартиры: ");
            New_Flat = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите номер дома: ");
            New_House = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите улицу: ");
            New_Street = Console.ReadLine();
            Console.Write("Введите город: ");
            New_City = Console.ReadLine();
            Program program = new Program(New_Flat, New_House, New_Street, New_City);
            Unicast one_for_all = program.Input;
            one_for_all();
        }
    }

    interface IReWrite
    {
        void Re_Write();
    }
}
