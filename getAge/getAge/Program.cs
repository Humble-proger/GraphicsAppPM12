//тут же напишу про наследование классов - класс наследник имеет доступ КО ВСЕМ методам\полям\свойствам класса родителя
//выглядит примерно так: Dog: Animal
// конструктор производн класса - вызывает род класс,виртульные методы(если класс public или protected) может изменить
// таким образом например  public override void MakeSound()!!! через overridehububh
using System;// для DateTime
namespace PersonAgeCalculator// чтобы можно было использовать тот же класс и интерфейсjijjjj
{
    class Program
    {
        static void Main(string[] args)
        {
            // Запрашиваем у пользователя ввод даты рождения
            Console.WriteLine("Введите дату рождения в формате ГГГГ-ММ-ДД:");

            DateTime birthDate;// если не получается распарсить строку в дату
            while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
            {
                Console.WriteLine("Неверный формат даты. Попробуйте еще раз:");
            }

            // Создаем экземпляр класса Person
            Person person = new Person
            {
                BirthDate = birthDate // Устанавливаем введенную дату рождения
            };

            // Вычисляем возраст
            int age = person.GetAge();

            // Выводим результат на экран
            Console.WriteLine($"Возраст: {age} лет");
        }
    }
}